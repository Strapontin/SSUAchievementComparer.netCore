using SSUAchievementComparer.Core.Entities;
using SSUAchievementComparer.Core.Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace SSUAchievementComparer.Core
{
    public static class AchievementMethods
    {
        /// <summary>
        /// Gets the webpage as Html object
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        private static HtmlAgilityPack.HtmlNodeCollection GetNodes(string content, string xpath)
        {
            var document = new HtmlAgilityPack.HtmlDocument();
            document.LoadHtml(content);

            // Select the nodes of the achievements
            var achievementNodecollection = document.DocumentNode.SelectNodes(xpath);

            return achievementNodecollection;
        }

        private static Achievement SetTitleNode(HtmlAgilityPack.HtmlNode MainNode, string gameName)
        {
            var titleNode = MainNode.Descendants("h3").First();
            string title = titleNode.InnerHtml;

            // Creates a new node so when the user clicks the link a new tab is opened to see helps on how to get the achievement
            var linkHelper = HtmlAgilityPack.HtmlNode.CreateNode($"<a href=\"http://www.google.com/search?q={gameName}+{title}+achievement\" target=\"_blank\">{title}</a>");
            titleNode.ParentNode.ReplaceChild(linkHelper, titleNode);

            Achievement achievement = new Achievement()
            {
                HtmlDiv = MainNode.OuterHtml,
                Title = title
            };

            return achievement;
        }

        /// <summary>
        /// Gets all the global achievements of the game
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public static GameDetails GetGameAchievements(string link)
        {
            var content = HtmlCommon.GetPageContent(link);
            var achievementNodecollection = GetNodes(content, "//div[contains(@class, 'achieveRow')]");
            var gameName = GetNodes(content, "//title").FirstOrDefault().InnerHtml
                .Replace("Steam Community :: ", "")
                .Replace(" :: Achievements", "");

            GameDetails gameDetails = new GameDetails();

            if (achievementNodecollection == null)
            {
                gameDetails.GameExists = false;
                return gameDetails;
            }

            foreach (var achievementNode in achievementNodecollection)
            {
                gameDetails.Achievements.Add(SetTitleNode(achievementNode, gameName));
            }

            return gameDetails;
        }

        /// <summary>
        /// Gets all achievements of a player
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public static PlayerDetails GetPlayerAchievements(string link, string gameName)
        {
            PlayerDetails playerDetails = new PlayerDetails();

            var content = HtmlCommon.GetPageContent(link);
            var achievementNodecollection = GetNodes(content, "//*[@class='achieveRow']");
            var playerNameNode = GetNodes(content, "//*[@class='whiteLink persona_name_text_content']");

            // If we don't find a node here, it's because the profile of the user is not accessible (set to private)
            if (playerNameNode == null)
            {
                playerDetails.PlayerName = GetNodes(content, "//*[@class='actual_persona_name']").First().InnerText.Trim();
                playerDetails.IsProfilePrivate = true;
                return playerDetails;
            }

            playerDetails.PlayerName = GetNodes(content, "//*[@class='whiteLink persona_name_text_content']").FirstOrDefault().InnerText.Trim();

            // Saves all achievements unlocked for player 
            foreach (var achievementNode in achievementNodecollection)
            {
                if (achievementNode.InnerHtml.Contains("achieveUnlockTime"))
                {
                    playerDetails.Achievements.Add(SetTitleNode(achievementNode, gameName));
                }
            }

            return playerDetails;
        }
    }
}
