using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        void SavePublisher(Publisher publisher);
        Publisher GetPublisherById(int id);
        void DeletePublisherById(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        List<Publisher> GetPublishers();
    }
}
