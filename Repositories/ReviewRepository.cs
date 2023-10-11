using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        public void DeleteReviewById(Review review) => ReviewDAO.DeleteReview(review);

        public Review GetReviewById(int id) => ReviewDAO.FindReviewById(id);

        public List<Review> GetReviews() => ReviewDAO.GetReviews();

        public Review SaveReview(Review review) => ReviewDAO.SaveReview(review);

        public Review UpdateReview(Review review) => ReviewDAO.UpdateReview(review);
    }
}
