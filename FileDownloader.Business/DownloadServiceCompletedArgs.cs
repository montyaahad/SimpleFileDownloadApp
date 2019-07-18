using System;
using System.Collections.Generic;
using System.Text;

namespace FileDownloader.Business
{
    public class DownloadServiceCompletedArgs : EventArgs
    {
        public DownloadServiceCompletedArgs(
            bool isCompleted,
            int succeededCount,
            int failedCount)
        {
            this.IsCompleted = isCompleted;
            this.SucceededCount = succeededCount;
            this.FailedCount = failedCount;
        }

        public bool IsCompleted { get; private set; }

        public int SucceededCount { get; private set; }

        public int FailedCount { get; private set; }
    }
}
