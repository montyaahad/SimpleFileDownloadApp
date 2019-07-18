namespace FileDownloader
{
    using System.ComponentModel;

    public class DownloadFileProgressChangedArgs : ProgressChangedEventArgs
    {
        public DownloadFileProgressChangedArgs(
            int progressPercentage,
            long bytesReceived,
            long totalBytesToReceive)
            : base(progressPercentage, null)
        {
            this.BytesReceived = bytesReceived;
            this.TotalBytesToReceive = totalBytesToReceive;
        }

        public long BytesReceived { get; private set; }
        
        public long TotalBytesToReceive { get; private set; }
    }

}
