namespace HotelManagmentAPI.Dto
{
    public class ReviewDto
    {
        public int ReviewID { get; set; }
        public int ClientID { get; set; }
        public string? Comment { get; set; }
        public int Rating { get; set; }
        public DateTime Date { get; set; }
    }
}
