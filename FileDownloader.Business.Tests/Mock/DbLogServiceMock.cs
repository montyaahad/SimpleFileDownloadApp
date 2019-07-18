using System;
using System.Collections.Generic;
using System.Text;

namespace FileDownloader.Business.Tests.Mock
{
    using FileDownloader.DataAccess.Model;

    public class DbLogServiceMock : IDbLogService
    {
        public bool DbLog(DownloadLog downloadLog)
        {
            return true;
        }
    }
}
