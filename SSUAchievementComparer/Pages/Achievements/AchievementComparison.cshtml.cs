using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSUAchievementComparer.Core;
using SSUAchievementComparer.Core.Entities;

namespace SSUAchievementComparer.Pages.Achievements
{
    public class AchievementComparisonModel : PageModel
    {
        public GameDetails GameDetails = new GameDetails();
        public PlayerDetails PlayerDetails1 = new PlayerDetails();
        public PlayerDetails PlayerDetails2 = new PlayerDetails();

        //public SearchDetails SearchDetails { get; set; }

        public AchievementComparisonModel()
        {

        }

        public IActionResult OnGet(int gameId, long idPlayer1, long idPlayer2)
        {
            // Fetches achievements of the game and players
            string link = $"https://steamcommunity.com/stats/{gameId}/achievements/";
            string link1 = $"https://steamcommunity.com/profiles/{idPlayer1}/stats/{gameId}/achievements?&l=en";
            string link2 = $"https://steamcommunity.com/profiles/{idPlayer2}/stats/{gameId}/achievements?&l=en";

            Thread threadGame, threadPlayer1, threadPlayer2;

            threadGame = new Thread(() =>
            {
                GameDetails = AchievementMethods.GetGameAchievements(link, "Halo");
            });
            threadPlayer1 = new Thread(() =>
            {
                PlayerDetails1 = AchievementMethods.GetPlayerAchievements(link1, "Halo");
            });
            threadPlayer2 = new Thread(() =>
            {
                PlayerDetails2 = AchievementMethods.GetPlayerAchievements(link2, "Halo");
            });

            threadGame.Start();
            threadPlayer1.Start();
            threadPlayer2.Start();

            threadGame.Join();
            threadPlayer1.Join();
            threadPlayer2.Join();

            var achievementTampon = new List<Achievement>(PlayerDetails1.Achievements);

            // Removes achievements that both players have
            foreach (var achievement in achievementTampon)
            {
                if (PlayerDetails2.Achievements.Any(a => a.Title == achievement.Title))
                {
                    PlayerDetails1.Achievements.RemoveAll(a => a.Title == achievement.Title);
                    PlayerDetails2.Achievements.RemoveAll(a => a.Title == achievement.Title);
                }

                GameDetails.Achievements.RemoveAll(a => a.Title == achievement.Title);
            }

            // Removes player2 achievement in the global list to unlock
            foreach (var achievement in PlayerDetails2.Achievements)
            {
                GameDetails.Achievements.RemoveAll(a => a.Title == achievement.Title);
            }

            return Page();
        }
    }
}
