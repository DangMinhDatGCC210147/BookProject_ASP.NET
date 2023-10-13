﻿using BusinessObjects;

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

        public static Review SaveReview(Review review)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Reviews.Add(review);
                    context.SaveChanges();
                    return review;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return null;
            }
        }

        public static Review UpdateReview(Review review)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Review>(review).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return review;
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
