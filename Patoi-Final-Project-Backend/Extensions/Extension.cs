using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using System;

namespace Patoi_Final_Project_Backend.Extensions
{
    public static class Extension
    {
        public static async Task<string> SaveImage(this IFormFile formfile, IWebHostEnvironment env, string folder)
        {
            string path = env.WebRootPath;
            string filename = Guid.NewGuid().ToString() + formfile.FileName;
            string result = Path.Combine(path, folder, filename);

            using (FileStream fileStream = new FileStream(result, FileMode.Create))
            {
                await formfile.CopyToAsync(fileStream);
            }
            return filename;

        }
    }
}
