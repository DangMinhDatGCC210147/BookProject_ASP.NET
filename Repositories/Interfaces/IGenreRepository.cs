using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface IGenreRepository
    {
        Genre SaveGenre(Genre genre);
        Genre GetGenreById(int id);
        void DeleteGenreById(Genre genre);
        Genre UpdateGenre(Genre genre);
        List<Genre> GetGenres();
    }
}
