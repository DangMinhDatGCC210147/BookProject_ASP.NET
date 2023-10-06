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
    public class PublisherRepository : IPublisherRepository
    {
        public void DeletePublisherById(Publisher publisher) => PublisherDAO.DeletePublisher(publisher);

        public Publisher GetPublisherById(int id) => PublisherDAO.FindPublisherById(id);

        public List<Publisher> GetPublishers() => PublisherDAO.GetPublishers();

        public void SavePublisher(Publisher publisher) => PublisherDAO.SavePublisher(publisher);

        public void UpdatePublisher(Publisher publisher) => PublisherDAO.UpdatePublisher(publisher);
    }
}
