namespace FileDownloader.Business.Tests.Mock
{
    using FileDownloader.Model;

    public class FileDownloaderFactoryMock : IFileDownloaderFactory
    {
        public IFileDownloader GetFileDownloader(FileModel fileModel)
        {
            if (fileModel.FileUrl == "https://www.asd.org/f1")
            {
                return new FileDownloaderFailedMock();
            }

            return new FileDownloaderSucceededMock();
        }
    }
}
