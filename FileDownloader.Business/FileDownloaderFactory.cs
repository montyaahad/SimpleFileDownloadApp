using System;

namespace FileDownloader.Business
{
    using FileDownloader.Model;

    using Microsoft.Extensions.Logging;

    public class FileDownloaderFactory : IFileDownloaderFactory
    {
        private readonly ILogger<DownloadService> _logger;

        public FileDownloaderFactory(ILoggerFactory loggerFactory)
        {
            this._logger = loggerFactory.CreateLogger<DownloadService>();
        }

        public IFileDownloader GetFileDownloader(FileModel fileModel)
        {
            IFileDownloader fileDownloader;

            var protocol = fileModel.FileUrl.Split(new string[] { "://" }, StringSplitOptions.None)[0];

            if (protocol.ToLower() == "http" || protocol.ToLower() == "https")
            {
                fileDownloader = new HttpFileDownloader();
            }
            else if (protocol.ToLower() == "ftp" || protocol.ToLower() == "sftp")
            {
                fileDownloader = new FtpFileDownloader();
            }
            else
            {
                this._logger.LogWarning("source : " + fileModel.FileUrl + "; Protocol not supported!");
                throw new Exception("Protocol not supported!");
            }

            return fileDownloader;
        }
    }
}
