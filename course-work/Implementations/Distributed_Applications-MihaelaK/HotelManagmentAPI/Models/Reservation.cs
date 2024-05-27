using System;
using System.ComponentModel.DataAnnotations;

public class Reservation
{
    [Key]
    public int ReservationID { get; set; }

    [Required]
    public int ClientID { get; set; }

    [Required]
    public int RoomID { get; set; }

    [Required]
    public DateTime CheckInDate { get; set; }

    [Required]
    public DateTime CheckOutDate { get; set; }

    [Required]
    public double TotalPrice { get; set; }

    [Required]
    [MaxLength(20)]
    public string? ReservationStatus { get; set; }

    // Navigation property
    public Client? Client { get; set; }
    public Room? Room { get; set; }
}
