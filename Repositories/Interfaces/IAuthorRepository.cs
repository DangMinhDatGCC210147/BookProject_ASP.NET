using BusinessObjects;
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        Author SaveAuthor(Author author);
        List<Author> GetAuthors();
        Author GetAuthorById(int id);
        void DeleteAuthorById(Author author);
        Author UpdateAuthor(Author author);
    }
}
