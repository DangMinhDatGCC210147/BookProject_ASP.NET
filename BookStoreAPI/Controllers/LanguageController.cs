using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Repositories;
using Repositories.Interfaces;
using System.Collections.Generic;

namespace BookStoreAPI.Controllers
{
    [Route("api/Languages")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageRepository repository = new LanguageRepository();

        [HttpGet]
        public ActionResult<IEnumerable<Language>> GetLanguages() => repository.GetLanguages();

        [HttpGet("{id}")]
        public ActionResult<Language> GetLanguageById(int id)
        {
            var language = repository.GetLanguageById(id);
            if (language == null)
                return NotFound();

            return Ok(language);
        }

        [HttpPost]
        public IActionResult CreateLanguage([FromBody] Language language)
        {
            return Ok(repository.SaveLanguage(language));
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteLanguages(int id)
        {
            var language = repository.GetLanguageById(id);
            if (language == null)
                return NotFound();
            repository.DeleteLanguageById(language);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLanguage(int id, Language language)
        {
            var existingLanguage = repository.GetLanguageById(id);
            if (existingLanguage == null)
                return NotFound();

            language.Id = id; 
            return Ok(repository.UpdateLanguage(language));
        }

        /////////////////////////////////////////////////////////////////////////////

        [HttpGet("export")]
        public async Task<IActionResult> ExportV2(CancellationToken cancellationToken)
        {
            // query data from database  
            await Task.Yield();

            var list = repository.GetLanguages().ToList();
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
