using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace VirtualClinic.Services.Upload
{
    public class UploadFile
    {
        private readonly IWebHostEnvironment _environment;
        public UploadFile(IWebHostEnvironment Environment)
        {
            _environment = Environment;
        }

        // this method convert file to byte[]
        public Byte[] Upload(IFormFile Iformimage)
        {
            Byte[] Image = null;
            if (Iformimage != null)
            {
                if (Iformimage.Length > 0)
                {
                    using (var target = new MemoryStream())
                    {
                        Iformimage.CopyTo(target);
                        Image = target.ToArray();
                    }

                }
            }       
            return Image;
        }
    }
}
