using HotelManagmentAPI.Data;
using HotelManagmentAPI.Interfaces;

namespace HotelManagmentAPI.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        public ReviewRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.OrderBy(r => r.ReviewID).ToList();
        }
        public Review GetReview(int reviewID)
        {
            return _context.Reviews.FirstOrDefault(r => r.ReviewID == reviewID);
        }
        public ICollection<Review> GetReviewsByClients(int clientID)
        {
            return _context.Reviews.Where(r => r.ClientID == clientID).ToList();
        }
        public bool ReviewExists(int reviewID)
        {
            return _context.Reviews.Any(r => r.ReviewID == reviewID);
        }

        public bool UpdateReview(Review review)
        {
            _context.Reviews.Update(review);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved >= 0 ? true : false;
        }

        public bool DeleteReview(int reviewID)
        {
            var review = GetReview(reviewID);
            _context.Reviews.Remove(review);
            return Save();
        }

        public bool CreateReview(Review review)
        {
            _context.Reviews.Add(review);
            return Save();
        }


    }
}
