namespace MicroNet.User.Application.Helpers
{
    public static class HelperFunctions
    {
        public static string SaveImages(string base64, string FilePath, string id)
        {
            //Get the file type to save in
            var FilePathWithExtension = string.Empty;
            string localBase64 = string.Empty;

            if (base64.Contains("data:image/jpeg;base64,"))
            {
                //FilePathWithExtension = FilePath + fileName + ".jpg";
                FilePathWithExtension = FilePath + id + ".jpg";
                localBase64 = base64.Replace("data:image/jpeg;base64,", string.Empty);
            }
            else if (base64.Contains("data:image/png;base64,"))
            {
                FilePathWithExtension = FilePath + id + ".png";
                localBase64 = base64.Replace("data:image/png;base64,", string.Empty);
            }
            else if (base64.Contains("data:image/bmp;base64"))
            {
                FilePathWithExtension = FilePath + id + ".bmp";
                localBase64 = base64.Replace("data:image/bmp;base64", string.Empty);
            }
            else if (base64.Contains("data:application/msword;base64,"))
            {
                FilePathWithExtension = FilePath + id + ".doc";
                localBase64 = base64.Replace("data:application/msword;base64,", string.Empty);
            }
            else if (base64.Contains("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,"))
            {
                FilePathWithExtension = FilePath + id + ".docx";
                localBase64 = base64.Replace("data:application/vnd.openxmlformats-officedocument.wordprocessingml.document;base64,", string.Empty);
            }
            else if (base64.Contains("data:application/pdf;base64,"))
            {
                FilePathWithExtension = FilePath + id + ".pdf";
                localBase64 = base64.Replace("data:application/pdf;base64,", string.Empty);
            }

            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(localBase64)))
            {
                using (FileStream fs = new FileStream(FilePathWithExtension, FileMode.Create, FileAccess.Write))
                {
                    //Create the specified directory if it does not exist
                    var photofolder = System.IO.Path.GetDirectoryName(FilePathWithExtension);
                    if (!Directory.Exists(photofolder))
                    {
                        Directory.CreateDirectory(photofolder!);
                    }

                    ms.WriteTo(fs);
                    fs.Close();
                    ms.Close();
                }
            }
            return FilePathWithExtension;
        }
    }
}
