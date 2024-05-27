using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelManagmentMVC.Models;

namespace HotelManagmentMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<HotelManagmentMVC.Models.ClientViewModel> ClientViewModel { get; set; } = default!;

        public DbSet<HotelManagmentMVC.Models.ReservationViewModel> ReservationViewModel { get; set; } = default!;

        public DbSet<HotelManagmentMVC.Models.ReviewViewModel> ReviewViewModel { get; set; } = default!;

        public DbSet<HotelManagmentMVC.Models.RoomViewModel> RoomViewModel { get; set; } = default!;
    }
}
