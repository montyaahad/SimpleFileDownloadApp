using System;

namespace FileDownloader
{
    public class DownloadFileCompletedArgs : EventArgs
    {
        public DownloadFileCompletedArgs(
            DownloadState state,
            string fileName,
            Uri fileSource,
            TimeSpan downloadTime,
            long bytesTotal,
            Exception error)
        {
            this.State = state;
            this.FileName = fileName;
            this.FileSource = fileSource;
            this.Error = error;
            this.DownloadTime = downloadTime;
            this.BytesTotal = bytesTotal;
        }

        public DownloadState State { get; private set; }
        
        public string FileName { get; private set; }
        
        public Uri FileSource { get; private set; }
        
        public Exception Error { get; private set; }
        
        public TimeSpan DownloadTime { get; private set; }
        
        public long BytesTotal { get; private set; }

        public int DownloadSpeedInKiloBytesPerSecond
        {
            get
            {
                if (this.DownloadTime == TimeSpan.Zero || this.BytesTotal == 0)
                {
                    return 0;
                }

                var kiloBytesReceived = this.BytesTotal / 1024.0;

                return Convert.ToInt32(kiloBytesReceived / this.DownloadTime.TotalSeconds);
            }
        }
    }
}
