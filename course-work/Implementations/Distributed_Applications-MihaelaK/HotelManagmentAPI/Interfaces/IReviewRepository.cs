namespace HotelManagmentAPI.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewID);
        ICollection<Review> GetReviewsByClients(int roomID);
        bool ReviewExists(int reviewID);

        bool UpdateReview(Review review);
        bool Save();
        bool DeleteReview(int reviewID);
        bool CreateReview(Review review);

    }
}
