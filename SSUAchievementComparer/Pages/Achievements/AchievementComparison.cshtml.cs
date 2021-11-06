using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSUAchievementComparer.Core;

namespace SSUAchievementComparer.Pages.Achievements
{
    public class AchievementComparisonModel : PageModel
    {
        public List<Achievement> AchievementsGame = new List<Achievement>();
        public PlayerDetails PlayerDetails1 = new PlayerDetails();
        public PlayerDetails PlayerDetails2 = new PlayerDetails();

        public AchievementComparisonModel()
        {

        }

        public IActionResult OnGet(int gameId, long player1, long player2)
        {
            if (gameId == 0 || player1 == 0 || player2 == 0)
            {
                return Page();
            }

            // Fetches achievements of the game and players
            string link = $"https://steamcommunity.com/stats/{gameId}/achievements/";
            string link1 = $"https://steamcommunity.com/profiles/{player1}/stats/{gameId}/achievements?&l=en";
            string link2 = $"https://steamcommunity.com/profiles/{player2}/stats/{gameId}/achievements?&l=en";

            Thread threadGame, threadPlayer1, threadPlayer2;

            threadGame = new Thread(() =>
            {
                AchievementsGame = AchievementMethods.GetGameAchievements(link, "Halo");
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

            var achievementTampon = new List<Achievement>(PlayerDetails1.Achievement);

            // Removes achievements that both players have
            foreach (var achievement in achievementTampon)
            {
                if (PlayerDetails2.Achievement.Any(a => a.Title == achievement.Title))
                {
                    PlayerDetails1.Achievement.RemoveAll(a => a.Title == achievement.Title);
                    PlayerDetails2.Achievement.RemoveAll(a => a.Title == achievement.Title);
                }

                AchievementsGame.RemoveAll(a => a.Title == achievement.Title);
            }

            // Removes player2 achievement in the global list to unlock
            foreach (var achievement in PlayerDetails2.Achievement)
            {
                AchievementsGame.RemoveAll(a => a.Title == achievement.Title);
            }

            return Page();
        }
    }
}
