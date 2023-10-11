using BusinessObjects;
using DatAccess;
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

        public Author SaveAuthor(Author author) => AuthorDAO.SaveAuthor(author);

        public Author UpdateAuthor(Author author) => AuthorDAO.UpdateAuthor(author);
    }
}
