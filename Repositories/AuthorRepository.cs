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
    public class AuthorRepository : IAuthorRepository
    {
        public void DeleteAuthorById(Author author) => AuthorDAO.DeleteAuthor(author);

        public Author GetAuthorById(int id) => AuthorDAO.FindAuthorById(id);

        public List<Author> GetAuthors() => AuthorDAO.GetAuthors();

        public void SaveAuthor(Author author) => AuthorDAO.SaveAuthor(author);

        public void UpdateAuthor(Author author) => AuthorDAO.UpdateAuthor(author);
    }
}
