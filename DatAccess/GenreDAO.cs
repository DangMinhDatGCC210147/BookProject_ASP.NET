using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class GenreDAO
    {
        public static List<Genre> GetGenres()
        {
            var listGenres = new List<Genre>();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    listGenres = context.Genres.ToList();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listGenres;
        }

        public static Genre FindGenreById(int id)
        {
            var genre = new Genre();
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    genre = context.Genres.Find(id);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return genre;
        }
        public static void SaveGenre(Genre genre)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Genres.Add(genre);
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void UpdateGenre(Genre genre)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Entry<Genre>(genre).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void DeleteGenre(Genre genre)
        {
            try
            {
                using (var context = new ApplicationDBContext())
                {
                    context.Genres.Remove(FindGenreById(genre.Id));
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
