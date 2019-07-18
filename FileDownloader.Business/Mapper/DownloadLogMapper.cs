using System;
using System.Collections.Generic;
using System.Text;

namespace FileDownloader.Business.Mapper
{
    using FileDownloader.Config;
    using FileDownloader.DataAccess.Model;

    public static class DownloadLogMapper
    {
        public static DownloadLog ConvertToDownloadLog(DownloadFileCompletedArgs e, IAppConfiguration configuration)
        {
            DownloadLog log = new DownloadLog();
            log.DownloadState = e.State.ToString();
            log.BytesTotal = e.BytesTotal;
            log.DownloadSpeedInKiloBytesPerSecond = e.DownloadSpeedInKiloBytesPerSecond;
            log.DownloadTime = e.DownloadTime;
            log.FileSource = e.FileSource.ToString();
            log.LocalFilePath = e.FileName;

            if (e.State == DownloadState.Succeeded)
            {
                log.IsFileBig = (e.BytesTotal / 1024) > configuration.GetBigFileSizeThresholdInKb();
                log.IsSpeedSlow = e.DownloadSpeedInKiloBytesPerSecond < configuration.GetSlowSpeedThresholdInKBps();
            }

            return log;
        }
    }
}
