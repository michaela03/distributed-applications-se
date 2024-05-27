using System.ComponentModel.DataAnnotations;

namespace HotelManagmentMVC.Models
{
    public class ReviewViewModel
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int ClientID { get; set; }

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(500)]
        public string? Comment { get; set; }

        [Required]
        public DateTime Date { get; set; }

       
    }
}
