using System;

namespace FileDownloader
{
    using System.Net;

    public interface IFileDownloader : IDisposable
    {
        event EventHandler<DownloadFileCompletedArgs> DownloadFileCompleted;
        
        event EventHandler<DownloadFileProgressChangedArgs> DownloadProgressChanged;
        
        void DownloadFileAsync(Uri source, string destinationDirectory, NetworkCredential credential = null);
    }
}
