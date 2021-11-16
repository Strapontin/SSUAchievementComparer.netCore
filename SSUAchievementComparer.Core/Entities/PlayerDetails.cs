using System.Collections.Generic;

namespace SSUAchievementComparer.Core.Entities
{
    public class PlayerDetails
    {
        public PlayerDetails()
        {
            Achievements = new List<Achievement>();
            IsProfilePrivate = false;
            PrivateProfileText = "Cannot see this user's achievements";
        }

        public string PlayerName { get; set; }
        public List<Achievement> Achievements { get; set; }
        public bool IsProfilePrivate { get; set; }
        public string PrivateProfileText { get; set; }
    }
}
