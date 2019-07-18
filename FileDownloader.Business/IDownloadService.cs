using FileDownloader.Model;

using System.Collections.Generic;

namespace FileDownloader.Business
{
    using System;

    public interface IDownloadService
    {
        event EventHandler<DownloadServiceCompletedArgs> DownloadServiceCompleted;

        void DownloadFiles(List<FileModel> listSources);
    }
}
