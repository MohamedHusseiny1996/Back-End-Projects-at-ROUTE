using Microsoft.AspNetCore.Http;
using System.IO;

namespace Department_prject_PL.Helper
{
    public class DocumentSetting
    {
        public static string UploadFile(IFormFile file, string FolderName)
        {
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", FolderName);
            string FileName = Path.GetFileName(file.FileName);
            string FilePath= Path.Combine(FolderPath, FileName);
            using(var FileStream =new FileStream(FilePath,FileMode.Create))
            {
                file.CopyTo(FileStream);
            }
            return FileName;
        }

        public static void DeleteFile(string FileName , string FolderName)
        {
            string path =Path.Combine("wwwroot",FolderName,FileName);
           File.Delete(path);
        }
    }
}
