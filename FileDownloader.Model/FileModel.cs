using System;
using System.Text;

namespace FileDownloader.Model
{
    public class FileModel
    {
        public string FileUrl { get; set; }

        public FileCredentials Credentials { get; set; }
    }
}
