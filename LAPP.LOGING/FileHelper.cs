using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAPP.LOGING
{
    public class FileHelper
    {


        public static void FileToBase64()
        {
            string base64String = null;
            string path = "D:\\Sample.pdf";

            byte[] imageBytes = File.ReadAllBytes(path);
            base64String = Convert.ToBase64String(imageBytes);
            File.WriteAllText("D:\\image\\Sample.txt", base64String);

        }
        public static string Base64ToFile(string base64String, string FilePathWithFileName)
        {
            try
            {
                byte[] fileBytes = Convert.FromBase64String(base64String);

                File.WriteAllBytes(FilePathWithFileName, fileBytes);
                //MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
                //ms.Write(imageBytes, 0, imageBytes.Length);
                //System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                return FilePathWithFileName;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


    }
}
