using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;

namespace AspNetCore_Server.Models
{
    public class UploadFile : IDisposable
    {
        private bool disposed = false;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public UploadFile(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        ~UploadFile()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    this.Dispose();
                }
            }
        }
        public string uploadfile(IFormFile File)
        {
            if (File == null) return "";
            var path = _webHostEnvironment.WebRootPath + "\\Media\\" + File.FileName;
            using var file = System.IO.File.Create(path);
            File.CopyTo(file);

            return File.FileName;
        }
        public string Getfileurl(string FileName)
        {
            if (FileName == null) return "";
            return _webHostEnvironment.WebRootPath + "\\Media\\" + FileName;
        }
    }
}
