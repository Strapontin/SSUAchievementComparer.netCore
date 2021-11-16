using System.ComponentModel.DataAnnotations;

namespace SSUAchievementComparer.Core.Entities
{
    public class SearchDetails
    {
        [Required]
        public int? GameId { get; set; }
        [Required]
        public long? IdPlayer1 { get; set; }
        [Required]
        public long? IdPlayer2 { get; set; }
    }
}
