using BusinessObjects;
using DataAccess;
using DatAccess;
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
        public void DeleteFeedbackById(FeedBack feedBack) => FeedbackDAO.DeleteFeedbackById(feedBack);

        public FeedBack GetFeedbackById(int id) => FeedbackDAO.FindFeedbackById(id);

        public List<FeedBack> GetFeedbacks() => FeedbackDAO.GetFeedbacks();

        public FeedBack SaveFeedback(FeedBack feedBack) => FeedbackDAO.SaveFeedback(feedBack);

        public FeedBack UpdateFeedback(FeedBack feedBack) => FeedbackDAO.UpdateFeedback(feedBack);
    }
}


