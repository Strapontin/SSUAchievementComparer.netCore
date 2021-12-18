using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SSUAchievementComparer.Core;
using SSUAchievementComparer.Core.Entities;
using SSUAchievementComparer.Core.Entities.DB;
using SSUAchievementComparer.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SSUAchievementComparer.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly string game_halo = "976730";
        //private readonly string player_strapontin = "76561198086634382";
        //private readonly string player_sevenup = "76561198193278659";

        [BindProperty]
        public SearchDetails SearchDetails { get; set; }

        private readonly IGameDetailsData gameDetailsData;
        public List<GameDetailsDb> GamesList;
        public List<PlayerDetailsDb> PlayersList;

        public IndexModel(IGameDetailsData gameDetailsData, IPlayerDetailsData playerDetailsData)
        {
            SearchDetails = new SearchDetails();
            this.gameDetailsData = gameDetailsData;

            if (this.gameDetailsData.GetCountOfGames() == 0)
            {
                gameDetailsData.FetchAllGames();
            }

            GamesList = gameDetailsData.GetGamesByName(null).ToList();
            PlayersList = playerDetailsData.GetPlayersByName(null).ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            return RedirectToPage("./Achievements/AchievementComparison", new { gameId = SearchDetails.GameId, idPlayer1 = SearchDetails.IdPlayer1, idPlayer2 = SearchDetails.IdPlayer2 });
        }
    }
}
