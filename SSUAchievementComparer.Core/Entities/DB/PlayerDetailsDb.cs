using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSUAchievementComparer.Core.Entities.DB
{
    public class PlayerDetailsDb
    {
        public PlayerDetailsDb(long playerId, string name)
        {
            this.playerId = playerId;
            this.name = name;
        }

        public int Id { get; set; }
        [Required]
        public long playerId { get; set; }
        [Required]
        public string name { get; set; }
    }
}
