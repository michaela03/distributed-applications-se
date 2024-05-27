using System;
using System.ComponentModel.DataAnnotations;

public class Client
{
    [Key]
    public int ClientID { get; set; }

    [Required]
    [MaxLength(50)]
    public string? FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string? LastName { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Required]
    [MaxLength(15)]
    public string? PhoneNumber { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }
}
