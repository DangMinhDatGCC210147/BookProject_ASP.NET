using BusinessObjects;

namespace Repositories.Interfaces
{
    public interface IGenreRepository
    {
        void SaveGenre(Genre genre);
        Genre GetGenreById(int id);
        void DeleteGenreById(Genre genre);
        void UpdateGenre(Genre genre);
        List<Genre> GetGenres();
    }
}
