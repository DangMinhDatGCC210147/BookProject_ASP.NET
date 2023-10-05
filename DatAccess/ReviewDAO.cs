using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ReviewDAO
    {
        public static List<Review> GetReviews()
        {
            var listReviews = new List<Review>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listReviews = context.Reviews.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listReviews;
        }

        public static Review FindReviewById(int id)
        {
            var review = new Review();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    review = context.Reviews.Find(id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return review;
        }
        public static void SaveReview(Review review)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Reviews.Add(review);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateReview(Review review)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Review>(review).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteReview(Review review)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Reviews.Remove(FindReviewById(review.Id));
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
