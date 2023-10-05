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
    public class GenreRepository : IGenreRepository
    {
        public void DeleteGenreById(Genre genre) => GenreDAO.DeleteGenre(genre);

        public Genre GetGenreById(int id) => GenreDAO.FindGenreById(id);

        public List<Genre> GetGenres() => GenreDAO.GetGenres();

        public void SaveGenre(Genre genre) => GenreDAO.SaveGenre(genre);

        public void UpdateGenre(Genre genre) => GenreDAO.UpdateGenre(genre);
    }
}
