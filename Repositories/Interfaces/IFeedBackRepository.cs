using BookStore.Models;
using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface IFeedBackRepository
    {
        FeedBack SaveFeedback(FeedBack feedback);
        List<FeedBack> GetFeedbacks();
        FeedBack GetFeedbackById(int id);
        void DeleteFeedbackById(FeedBack feedback);
        FeedBack UpdateFeedback(FeedBack feedback);
    }
}
