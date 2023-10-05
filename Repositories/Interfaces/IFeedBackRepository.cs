using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IFeedBackRepository
    {
        void SaveFeedBack(FeedBack feedBack);
        FeedBack GetFeedBackById(int id);
        void DeleteFeedBackById(FeedBack feedBack);
        void UpdateFeedBack(FeedBack feedBack);
        List<FeedBack> GetFeedBacks();
    }
}
