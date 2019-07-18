using System;

namespace FileDownloader
{
    using System.ComponentModel;
    using System.IO;
    using System.Net;

    public class HttpFileDownloader : IFileDownloader
    {
        public event EventHandler<DownloadFileCompletedArgs> DownloadFileCompleted;
        
        public event EventHandler<DownloadFileProgressChangedArgs> DownloadProgressChanged;

        private DateTime DownloadStartTime { get; set; }


        private Uri fileSource;
        private string localFileName;
        private string destinationPath;

        private long TotalBytesToReceive { get; set; }

        public void DownloadFileAsync(Uri source, string destinationDirectory, NetworkCredential credential = null)
        {
            this.localFileName = FileUtil.GetFilename(source.ToString());
            this.DownloadStartTime = DateTime.Now;
            this.fileSource = source;
            this.destinationPath = destinationDirectory + "/" + this.localFileName;

            using (var downloadWebClient = this.CreateWebClient())
            {
                downloadWebClient.DownloadFileAsync(this.fileSource, this.destinationPath);
            }
        }

        private WebClient CreateWebClient()
        {
            var webClient = new WebClient();
            webClient.DownloadFileCompleted += this.OnDownloadFileCompleted;
            webClient.DownloadProgressChanged += this.OnDownloadProgressChanged;
            return webClient;
        }

        private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.TotalBytesToReceive = e.TotalBytesToReceive;
            var args = new DownloadFileProgressChangedArgs(e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive);

            this.InvokeDownloadProgressChanged(sender, args);
        }

        private void OnDownloadFileCompleted(object sender, AsyncCompletedEventArgs args)
        {
            var webClient = sender as WebClient;
            if (webClient == null)
            {
                this.InvokeDownloadCompleted(DownloadState.Failed, this.localFileName);
                return;
            }

            if (args.Cancelled)
            {
                FileUtil.DeleteDownloadedFile(this.destinationPath);

                this.InvokeDownloadCompleted(DownloadState.Canceled, this.localFileName);
            }
            else if (args.Error != null)
            {
                FileUtil.DeleteDownloadedFile(this.destinationPath);

                this.InvokeDownloadCompleted(DownloadState.Failed, this.localFileName, args.Error);
            }
            else
            {
                this.InvokeDownloadCompleted(DownloadState.Succeeded, this.localFileName, null);
            }
        }

        private void InvokeDownloadCompleted(DownloadState downloadDownloadState, string fileName, Exception error = null, bool fromCache = false)
        {
            var downloadTime = DateTime.Now.Subtract(this.DownloadStartTime);

            this.DownloadFileCompleted?.Invoke(this, new DownloadFileCompletedArgs(downloadDownloadState, fileName, fileSource, downloadTime, TotalBytesToReceive, error));
        }

        private void InvokeDownloadProgressChanged(object sender, DownloadFileProgressChangedArgs args)
        {
            this.DownloadProgressChanged?.Invoke(sender, args);
        }

        

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    //this.CleanupWebClient();
                }

                this.disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            this.Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
