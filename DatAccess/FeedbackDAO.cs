using BusinessObjects;

namespace DatAccess
{
    public class FeedbackDAO
    {
        public static List<FeedBack> GetFeedbacks()
        {
            var listFeedbacks = new List<FeedBack>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listFeedbacks = context.FeedBacks.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listFeedbacks;
        }

        public static FeedBack FindFeedbackById(int id)
        {
            var feedback = new FeedBack();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    feedback = context.FeedBacks.Find(id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return feedback;
        }

        public static FeedBack SaveFeedback(FeedBack feedback)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.FeedBacks.Add(feedback);
                    context.SaveChanges();
                    return feedback;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException?.Message);
                return null;
            }
        }

        public static FeedBack UpdateFeedback(FeedBack feedback)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<FeedBack>(feedback).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                    return feedback;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteFeedbackById(FeedBack feedback)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.FeedBacks.Remove(FindFeedbackById(feedback.Id));
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
