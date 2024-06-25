using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace ClinicWebApp.Extensions.FileExtensions
{
    public static class FormFileExtensions
    {
        public static async Task<FormFileResult> SaveImage(this IFormFile file, string baseLocation, string name)
        {
            var result = new FormFileResult();

            if (file.Length < 0 && file.Length > 1000000)
            {
                result.Result = false;
                result.Error = "فایل دریافت نشد/حجم فایل بیش از 1 مگابایت است";
                return result;
            }

            string extension = Path.GetExtension(file.FileName);
            if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
            {
                result.Result = false;
                result.Error = "فرمت فایل باید png، jpg، jpeg باشد";
                return result;
            }

            var uniqueFileName = name + extension;
            result.SrcPath = $"/imgs/{uniqueFileName}";
            result.PhysicalPath = Path.Combine(baseLocation, "wwwroot", "imgs", uniqueFileName);
            using (var stream = new FileStream(result.PhysicalPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            result.Result = true;
            return result;
        }

        public static async Task<bool> DeleteImage(string physicalLocation)
        {
            if (File.Exists(physicalLocation))
            {
                File.Delete(physicalLocation);
            }

            return true;
        }
    }

    public class FormFileResult
    {
        public bool Result { get; set; }
        public string? Error { get; set; }
        public string? SrcPath { get; set; }
        public string? PhysicalPath { get; set; }
    }
}