using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Repositories;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace BookStoreAPI.Controllers
{
    [Route("api/Authors")]
    [ApiController]
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository repository = new AuthorRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors() => repository.GetAuthors();

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthorById(int id)
        {
            var author = repository.GetAuthorById(id);
            if (author == null)
                return NotFound();

            return Ok(author);
        }

        [HttpPost]
        public IActionResult CreateAuthor([FromBody] Author author)
        {
            return Ok(repository.SaveAuthor(author));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthors(int id)
        {
            var author = repository.GetAuthorById(id);
            if (author == null)
                return NotFound();
            repository.DeleteAuthorById(author);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            var existingAuthor = repository.GetAuthorById(id);
            if (existingAuthor == null)
                return NotFound();

            author.Id = id; // Make sure the ID is set to the correct value
            repository.UpdateAuthor(author);
            return Ok();
        }

        
      
        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();
           
            var list = repository.GetAuthors().ToList();
            var stream = new MemoryStream();

            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");
                workSheet.Cells.LoadFromCollection(list, true);
                package.Save();
            }
            stream.Position = 0;
            string excelName = $"UserList-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            //return File(stream, "application/octet-stream", excelName);  
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", excelName);
        }
    }
}



