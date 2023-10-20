using BusinessObjects;
using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IHomeRepository
    {
        Task<List<TopAuthor>> GetBookAuthors(string userId);
        Task<List<TopGenre>> GetBookGenres(string userId);
	}
}
