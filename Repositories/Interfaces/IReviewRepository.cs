using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IReviewRepository
    {
        void SaveReview(Review review);
        Review GetReviewById(int id);
        void DeleteReviewById(Review review);
        void UpdateReview(Review review);
        List<Review> GetReviews();
    }
}
