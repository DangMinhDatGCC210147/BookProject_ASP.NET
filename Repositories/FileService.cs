using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FileService : IFileService
    {
        private IWebHostEnvironment environment;
        public FileService(IWebHostEnvironment env)
        {
            this.environment = env;
        }
        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                var connectPath = this.environment.ContentRootPath;

                var path = Path.Combine(connectPath, "wwwroot\\Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var ext = Path.GetExtension(imageFile.FileName);
                var allowedExtentions = new string[] { ".jpg", ".png", ".jpeg", ".pdf" };
                if (!allowedExtentions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extentions are allowed", string.Join(",", allowedExtentions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();

                var newFileName = uniqueString + ext;
                var fileWithPath = Path.Combine(path, newFileName);
                var stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception e)
            {
                return new Tuple<int, string>(0, "Error has accured");
            }
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                var wwPath = this.environment.WebRootPath;
                var path = Path.Combine(wwPath, "wwwroot\\Uploads\\", imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
