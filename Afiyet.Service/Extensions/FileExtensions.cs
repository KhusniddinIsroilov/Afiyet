using Afiyet.Service.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Afiyet.Service.Extensions
{
    public static class FileExtensions
    {
        public static async Task<string> SaveFileAsync(this Stream file, string fileName, string url, IWebHostEnvironment env, IConfiguration config)
        {
            string hostUrl = HttpContextHelper.Context?.Request?.Scheme + "://" + HttpContextHelper.Context?.Request?.Host.Value;


            fileName = Guid.NewGuid().ToString("N") + "_" + fileName;

            string storagePath = config.GetSection($"Storage:{url}").Value;

            string filePath = Path.Combine(env.WebRootPath, $"{storagePath}/{fileName}");
            FileStream mainFile = File.Create(filePath);

            string webUrl = $@"{hostUrl}/{storagePath}/{fileName}";

            await file.CopyToAsync(mainFile);
            mainFile.Close();

            return webUrl;
        }

    }
}
