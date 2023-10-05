using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        void SaveAuthor(Author author);
        Author GetAuthorById(int id);
        void DeleteAuthorById(Author author);
        void UpdateAuthor(Author author);
        List<Author> GetAuthors();
    }
}
