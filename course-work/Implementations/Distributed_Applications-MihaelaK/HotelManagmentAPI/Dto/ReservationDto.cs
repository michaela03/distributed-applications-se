namespace HotelManagmentAPI.Dto
{
    public class ReservationDto
    {
        public int ReservationID { get; set; }
        public int RoomID { get; set; }
        public int ClientID { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
       public double TotalPrice { get; set; }

       public string ReservationStatus { get; set; }
    }
}
