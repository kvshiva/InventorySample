using Microsoft.AspNetCore.Http;
using Common.Enum;

namespace Common
{
    public static class FileServices
    {
        private static string Webroot { get { return "wwwroot/"; } }
        private static string UploadPath { get { return "Uploads/"; } }
        private static string ProfilePath { get { return "Profile/"; } }
        private static string ProjectPath { get { return "Project/"; } }
        private static string LogoPath { get { return "Logo/"; } }
        public static string ProfileDefaultImage { get { return "Assets/Images/Profile.png"; } }
        public static string CurrentDirectory { get { return Directory.GetCurrentDirectory(); } }

        private static string GetUploadPath(UploadTypeEnum UploadType)
        {
            #region Get Upload Path
            var FilePath = string.Empty;
            if (UploadType == UploadTypeEnum.Profile)
                FilePath = UploadPath + ProfilePath;
            else if (UploadType == UploadTypeEnum.Project)
                FilePath = UploadPath + ProjectPath;
            else if (UploadType == UploadTypeEnum.Logo)
                FilePath = UploadPath + LogoPath;

            return FilePath;
            #endregion
        }

        public static string UniqueName(string Name)
        {
            #region Unique Name
            return Guid.NewGuid().ToString() + "-R" + Name;
            #endregion
        }

        public static string UploadFile(IFormFile File, UploadTypeEnum UploadType)
        {
            #region  Upload File
            try
            {
                var FileName = UniqueName(File.Name);
                var UploadPath = GetUploadPath(UploadType);
                var FullUploadPath = Webroot + UploadPath;

                var UploadedFilePath = FullUploadPath + FileName;

                if (!Directory.Exists(FullUploadPath))
                    Directory.CreateDirectory(FullUploadPath);

                using var FS = new FileStream(UploadedFilePath, FileMode.Create);
                File.CopyTo(FS);

                return UploadPath + FileName;
            }
            catch { throw; }
            #endregion
        }

        public static FileStream Download(string Url)
        {
            #region Download
            try
            {
                //Url = "http://./" + Url;

                var file = File.OpenRead(Url);

                return file;
            }
            catch { throw; }
            #endregion
        }

        public static bool FileDelete(string Url)
        {
            #region File Delete
            try
            {
                if (File.Exists(Url))
                {
                    File.Delete(Url);
                    return true;
                }
                return false;
            }
            catch { return false; }
            #endregion
        }
    }
}
