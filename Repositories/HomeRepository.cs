using BusinessObjects;
using BusinessObjects.DTO;
using DataAccess;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class HomeRepository : IHomeRepository
    {
        public Task<List<TopAuthor>> GetBookAuthors() => HomeDAO.TopSixAuthors();
        public Task<List<TopGenre>> GetBookGenres() => HomeDAO.TopGenres();
    }
}
