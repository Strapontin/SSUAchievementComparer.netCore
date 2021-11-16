using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSUAchievementComparer.Core.Entities.DB
{
    public class GameDetailsDb
    {
        public GameDetailsDb()
        {
        }

        public int Id { get; set; }
        [Required]
        public int appid { get; set; }
        [Required]
        public string name { get; set; }
    }
}
