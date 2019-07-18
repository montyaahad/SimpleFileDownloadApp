using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileDownloader.Business.Tests
{
    using System.Collections.Generic;
    using System.Threading;

    using FileDownloader.Business.Tests.Mock;
    using FileDownloader.Config;
    using FileDownloader.Model;

    using Microsoft.Extensions.Logging;

    [TestClass]
    public class DownloadServiceTests
    {

        private  IAppConfiguration _appConfiguration;

        private  IDbLogService _dbLogService;

        private  IFileDownloaderFactory _fileDownloaderFactory;

        private  ILogger<DownloadService> _logger;

        private List<FileModel> _listSources;

        [TestInitialize]
        public void Setup()
        {
            this._appConfiguration = new AppConfiguration();
            this._dbLogService = new DbLogServiceMock();
            this._fileDownloaderFactory = new FileDownloaderFactoryMock();
            this._logger = null;
            this._listSources = new List<FileModel>()
                                    {
                                        new FileModel() { FileUrl = "https://www.asd.org/f1" },
                                        new FileModel() { FileUrl = "https://www.asd.org/f2" },
                                        new FileModel() { FileUrl = "https://www.asd.org/f3" }
                                    };
        }

        [TestMethod]
        public void DownloadFilesBasic()
        {
            bool isCompleted = false;
            var downloadService = new DownloadService(this._appConfiguration, this._dbLogService, new LoggerFactory(), this._fileDownloaderFactory);
            downloadService.DownloadServiceCompleted += (sender, args) => { isCompleted = args.IsCompleted; };
            downloadService.DownloadFiles(this._listSources);

            Thread.Sleep(100);
            Assert.AreEqual(true, isCompleted);
        }

        [TestMethod]
        public void DownloadFilesSucceedCount()
        {
            int succeedCount = 0;
            var downloadService = new DownloadService(this._appConfiguration, this._dbLogService, new LoggerFactory(), this._fileDownloaderFactory);
            downloadService.DownloadServiceCompleted += (sender, args) => { succeedCount = args.SucceededCount; };
            downloadService.DownloadFiles(this._listSources);

            Thread.Sleep(100);
            Assert.AreEqual(2, succeedCount);
        }

        [TestMethod]
        public void DownloadFilesFailedCountCount()
        {
            int failedCount = 0;
            var downloadService = new DownloadService(this._appConfiguration, this._dbLogService, new LoggerFactory(), this._fileDownloaderFactory);
            downloadService.DownloadServiceCompleted += (sender, args) => { failedCount = args.FailedCount; };
            downloadService.DownloadFiles(this._listSources);

            Thread.Sleep(100);
            Assert.AreEqual(1, failedCount);
        }
    }
}
