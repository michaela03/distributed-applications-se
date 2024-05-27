using System;
using System.ComponentModel.DataAnnotations;

public class Room
{
    [Key]
    public int RoomID { get; set; }

    [Required]
    [MaxLength(50)]
    public string? RoomType { get; set; }

    [Required]
    public int Capacity { get; set; }

    [Required]
    public double PricePerNight { get; set; }

    [Required]
    [MaxLength(200)]
    public string? Description { get; set; }

    public string ImageUrl { get; set; }
}
