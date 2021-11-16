using System.Collections.Generic;

namespace SSUAchievementComparer.Core.Entities
{
    public class GameDetails
    {
        public GameDetails()
        {
            Achievements = new List<Achievement>();
            GameExists = true;
        }

        public int GameId { get; set; }
        public string GameName { get; set; }
        public List<Achievement> Achievements { get; set; }
        public bool GameExists { get; set; }
    }
}
