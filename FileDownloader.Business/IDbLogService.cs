using System;
using System.Collections.Generic;
using System.Text;

namespace FileDownloader.Business
{
    using FileDownloader.DataAccess.Model;

    public interface IDbLogService
    {
        bool DbLog(DownloadLog downloadLog);
    }
}
