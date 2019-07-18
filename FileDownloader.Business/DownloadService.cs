using System;
using System.Collections.Generic;

namespace FileDownloader.Business
{
    using System.Net;

    using FileDownloader.Business.Mapper;
    using FileDownloader.Config;
    using FileDownloader.DataAccess;
    using FileDownloader.DataAccess.Model;

    using global::FileDownloader.Model;

    using Microsoft.Extensions.Logging;

    public class DownloadService : IDownloadService
    {
        private readonly IAppConfiguration _appConfiguration;

        private readonly IDbLogService _dbLogService;

        private readonly IFileDownloaderFactory _fileDownloaderFactory;

        private readonly ILogger<DownloadService> _logger;

        private int _totalFiles = 0;
        private int _inProcessFiles = 0;
        private int _failedFiles = 0;
        private int _succeededFiles = 0;


        public DownloadService(
            IAppConfiguration appConfiguration,
            IDbLogService dbLogService,
            ILoggerFactory loggerFactory,
            IFileDownloaderFactory fileDownloaderFactory)
        {
            this._appConfiguration = appConfiguration;
            this._dbLogService = dbLogService;
            this._logger = loggerFactory.CreateLogger<DownloadService>();
            this._fileDownloaderFactory = fileDownloaderFactory;
        }

        public event EventHandler<DownloadServiceCompletedArgs> DownloadServiceCompleted;

        public void DownloadFiles(List<FileModel> listSources)
        {
            this._totalFiles = listSources.Count;

            var destinationPath = this._appConfiguration.GetDestinationPath();
            foreach (var source in listSources)
            {
                NetworkCredential credential = null;
                if (source.Credentials != null)
                {
                    credential = new NetworkCredential(source.Credentials.Username, source.Credentials.Password);
                }

                using (var fileDownloader = this._fileDownloaderFactory.GetFileDownloader(source))
                {
                    fileDownloader.DownloadFileCompleted += this.DownloadFileCompleted;
                    fileDownloader.DownloadFileAsync(new Uri(source.FileUrl), destinationPath, credential);
                }
            }
        }

        private void DownloadFileCompleted(object sender, DownloadFileCompletedArgs e)
        {
            if (e.State == DownloadState.Succeeded)
            {
                this._logger.LogInformation("source :"+ e.FileName +"; download info : file size =" + e.BytesTotal + " Bytes; speed = " + e.DownloadSpeedInKiloBytesPerSecond + "kB/s");
                this._logger.LogInformation("source :" + e.FileName + "; download completed");
            }
            else if (e.State == DownloadState.Failed)
            {
                this._logger.LogWarning("source :" + e.FileName + "; download failed");
            }

            var dlLog = DownloadLogMapper.ConvertToDownloadLog(e, this._appConfiguration);

            this._dbLogService.DbLog(dlLog);

            this.InvokeCompletedEvent(e.State == DownloadState.Succeeded);
        }

        private void InvokeCompletedEvent(bool isSucceeded)
        {
            this._inProcessFiles++;
            if (isSucceeded)
            {
                this._succeededFiles++;
            }
            else
            {
                this._failedFiles++;
            }

            if (this._inProcessFiles >= this._totalFiles)
            {
                this.DownloadServiceCompleted?.Invoke(this, new DownloadServiceCompletedArgs(true, this._succeededFiles, this._failedFiles));
            }
        }
    }
}
