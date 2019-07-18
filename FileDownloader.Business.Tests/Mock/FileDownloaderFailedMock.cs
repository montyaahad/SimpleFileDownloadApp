using System;
using System.Collections.Generic;
using System.Text;

namespace FileDownloader.Business.Tests.Mock
{
    using System.Net;

    public class FileDownloaderFailedMock : IFileDownloader
    {
        public event EventHandler<DownloadFileCompletedArgs> DownloadFileCompleted;

        public event EventHandler<DownloadFileProgressChangedArgs> DownloadProgressChanged;

        public void DownloadFileAsync(Uri source, string destinationDirectory, NetworkCredential credential = null)
        {
            this.DownloadFileCompleted?.Invoke(this, new DownloadFileCompletedArgs(DownloadState.Failed, "", source, TimeSpan.MinValue, 0, null));
        }

        public void Dispose()
        {
        }
    }
}
