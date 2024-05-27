
namespace HotelManagmentAPI.Dto
{
    public class RoomDto
    {
        public int RoomID { get; set; }
        public string? RoomType { get; set; }
        public int Capacity { get; set; }
        public double PricePerNight { get; set; }
        public string? Description { get; set; }

        public string ImageUrl { get; set; }

    }
}
