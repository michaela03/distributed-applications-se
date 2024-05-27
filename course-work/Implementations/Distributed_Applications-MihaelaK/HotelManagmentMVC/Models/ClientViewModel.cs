using System.ComponentModel.DataAnnotations;

namespace HotelManagmentMVC.Models
{
    public class ClientViewModel
    {
        [Key]
        public int ClientID { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "Email Address")]
        public string? Email { get; set; }

        [Required]
        [MaxLength(15)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [Display(Name = "Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [MaxLength(200)]
        [Display(Name = "Address")]
        public string? Address { get; set; }
    }

}

