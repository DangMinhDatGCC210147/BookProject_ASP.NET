﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class PublisherDAO
    {
        public static List<Publisher> GetPublishers()
        {
            var listPublishers = new List<Publisher>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listPublishers = context.Publishers.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listPublishers;
        }

        public static Publisher FindPublisherById(int id)
        {
            var publisher = new Publisher();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    publisher = context.Publishers.Find(id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return publisher;
        }
        public static void SavePublisher(Publisher publisher)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Publishers.Add(publisher);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdatePublisher(Publisher publisher)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Publisher>(publisher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeletePublisher(Publisher publisher)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Publishers.Remove(FindPublisherById(publisher.Id));
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
