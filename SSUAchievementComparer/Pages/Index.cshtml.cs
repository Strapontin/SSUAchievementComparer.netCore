using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SSUAchievementComparer.Core;
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

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
