using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class AuthorDAO
    {
        public static List<Author> GetAuthors()
        {
            var listAuthors = new List<Author>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listAuthors = context.Authors.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listAuthors;
        }

        public static Author FindAuthorById(int id)
        {
            var author = new Author();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    author = context.Authors.Find(id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return author;
        }
        public static void SaveAuthor(Author author)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Authors.Add(author);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateAuthor(Author author)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Author>(author).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteAuthor(Author author)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Authors.Remove(FindAuthorById(author.Id));
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
