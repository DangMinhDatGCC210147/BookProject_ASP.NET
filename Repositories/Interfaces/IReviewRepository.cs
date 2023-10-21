using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Review SaveReview(Review review);
        List<Review> GetReviews();
        Review GetReviewById(int id);
		List<Review> GetReviewsByBookId(int id);
		void DeleteReviewById(Review review);
        Review UpdateReview(Review review);
    }
}
