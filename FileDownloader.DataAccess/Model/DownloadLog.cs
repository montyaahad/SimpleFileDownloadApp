using System;

namespace FileDownloader.DataAccess.Model
{
    public class DownloadLog
    {
        public int DownloadLogId { get; set; }

        public string DownloadState { get; set; }

        public string LocalFilePath { get; set; }

        public string FileSource { get; set; }

        public string Protocol { get; set; }

        public string Error { get; set; }

        public TimeSpan DownloadTime { get; set; }

        public long BytesTotal { get; set; }

        public int DownloadSpeedInKiloBytesPerSecond { get; set; }

        public bool IsSpeedSlow { get; set; }

        public bool IsFileBig { get; set; }
    }
}
