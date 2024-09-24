namespace Company.G03.PL.Helper
{
    public static class DocumentSettings
    {
        //Upload
        public static string UploadFile(IFormFile file , string folderName)
        {

            //string folderPath = $"C:\\Users\\Jimmybasha\\source\\repos\\Company.G03 Solution\\Company.G03.PL\\wwwroot\\files\\{folderName}";


            //var folderpath = Path.Combine(Directory.GetCurrentDirectory(),"\\wwwroot\\files\\", folderName);
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files",folderName);

            string fileName =$"{Guid.NewGuid()}{file.FileName}";

            string filePath = Path.Combine(folderPath,fileName);

           using var fileStream = new FileStream(filePath,FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;
        }

        //Delete
        public static void deleteFile(string fileName , string folderName)
        {



            string filePath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot\\files",folderName,fileName);


            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

        }

    }
}
