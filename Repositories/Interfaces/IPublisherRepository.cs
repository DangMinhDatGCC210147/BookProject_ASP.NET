using BusinessObjects;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IPublisherRepository
    {
        Publisher SavePublisher(Publisher p);
        List<Publisher> GetPublishers();
        Publisher GetPublisherById(int id);
        void DeletePublisherById(Publisher p);
        Publisher UpdatePublisher(Publisher p);
    }
}
