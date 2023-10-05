using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class FeedbackDAO
    {
        public static List<FeedBack> GetFeedBacks()
        {
            var listFeedBacks = new List<FeedBack>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listFeedBacks = context.FeedBacks.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listFeedBacks;
        }

        public static FeedBack FindFeedBackById(int id)
        {
            var feedBack = new FeedBack();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    feedBack = context.FeedBacks.Find(id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return feedBack;
        }
        public static void SaveFeedBack(FeedBack feedBack)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.FeedBacks.Add(feedBack);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateFeedBack(FeedBack feedBack)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<FeedBack>(feedBack).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteFeedBack(FeedBack feedBack)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.FeedBacks.Remove(FindFeedBackById(feedBack.Id));
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
