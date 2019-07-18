using System;
using System.Collections.Generic;
using System.Text;

namespace FileDownloader.Business
{
    using FileDownloader.Model;

    public interface IFileDownloaderFactory
    {
        IFileDownloader GetFileDownloader(FileModel fileModel);
    }
}
