using BusinessObjects;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FeedbackRepository : IFeedBackRepository
    {
        public void DeleteFeedBackById(FeedBack feedBack) => FeedbackDAO.DeleteFeedBack(feedBack);

        public FeedBack GetFeedBackById(int id) => FeedbackDAO.FindFeedBackById(id);

        public List<FeedBack> GetFeedBacks() => FeedbackDAO.GetFeedBacks();

        public void SaveFeedBack(FeedBack feedBack) => FeedbackDAO.SaveFeedBack(feedBack);

        public void UpdateFeedBack(FeedBack feedBack) => FeedbackDAO.UpdateFeedBack(feedBack);
    }
}
