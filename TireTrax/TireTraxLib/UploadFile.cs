using System;
using System.Configuration;
using System.IO;
using System.Web;

namespace TireTraxLib
{
    public class UploadFile
    {
        private string savePath;
        private string tempFileName;
        public UploadFile(string originalFileName)
        {
            //tempFileName = Guid.NewGuid().ToString("N") + "_" + originalFileName;
            //string saveLocation = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["FileUploadLocation"]);
            //savePath = Path.Combine(saveLocation, tempFileName);
            tempFileName = originalFileName;
            string saveLocation = HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["FileUploadLocation"]);
            savePath = Path.Combine(saveLocation, tempFileName);
        }

        public UploadFile(string originalFileName, string filePath)
        {
            //tempFileName = Guid.NewGuid().ToString("N") + "_" + originalFileName;
            //tempFileName = tempFileName.Replace(" ", "");
            //string saveLocation = HttpContext.Current.Server.MapPath(filePath );
            //savePath = Path.Combine(saveLocation, tempFileName);

            tempFileName = originalFileName;
            tempFileName = tempFileName.Replace(" ", "");
            string saveLocation = HttpContext.Current.Server.MapPath(filePath);
            savePath = Path.Combine(saveLocation, tempFileName);
        }

        public string FileName
        {
            get { return tempFileName; }
        }

        /// <summary>
        /// Temp path used to save the uploaded file
        /// </summary>
        public string SavePath
        {
            get
            {
                return savePath;
            }
        }

        /// <summary>
        /// Attempt to delete temp file
        /// </summary>
        public void DeleteFileNoException()
        {
            if (File.Exists(savePath))
            {
                try
                {
                    File.Delete(savePath);
                }
                catch (Exception e)
                {
                    new SqlLog().InsertSqlLog(0, "UploadFile.DeleteFileNoException", e);
                    //User does not need to see this exception.
                }
            }
        }

        /// <summary>
        /// Return connection strinng based on file extension
        /// </summary>
        public string GetOleDbConnectionString()
        {
            var finfo = new FileInfo(savePath);

            if (!finfo.Exists)
            {
                throw new FileNotFoundException(savePath);
            }

            var fileExtension = finfo.Extension.ToLower();
            switch (fileExtension)
            {
                case ".xls":
                    return string.Format(ConfigurationManager.AppSettings["Excel2003OleDBConnection"], savePath);
                case ".xlsx":
                    return string.Format(ConfigurationManager.AppSettings["Excel2007OleDBConnection"], savePath);
                default:
                    throw new NotSupportedException(String.Format("This file type {0} is not supported!", fileExtension));
            }
        }
    }
}
