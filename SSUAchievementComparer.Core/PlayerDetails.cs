using System.Collections.Generic;

namespace SSUAchievementComparer.Core
{
    public class PlayerDetails
    {
        public PlayerDetails()
        {
            Achievement = new List<Achievement>();
            IsProfilePrivate = false;
        }

        public string PlayerName { get; set; }
        public List<Achievement> Achievement { get; set; }
        public bool IsProfilePrivate { get; set; }
        public string PrivateProfileText { get; set; }
    }
}
