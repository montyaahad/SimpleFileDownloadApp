using System;

namespace FileDownloader.Business
{
    using FileDownloader.Config;
    using FileDownloader.DataAccess;
    using FileDownloader.DataAccess.Model;

    using Microsoft.Extensions.Logging;

    public class DbLogService : IDbLogService
    {
        private readonly IAppConfiguration _appConfiguration;
        private readonly ILogger<DbLogService> _logger;

        public DbLogService(
            IAppConfiguration appConfiguration,
            ILoggerFactory loggerFactory)
        {
            this._appConfiguration = appConfiguration;
            this._logger = loggerFactory.CreateLogger<DbLogService>();
        }

        public bool DbLog(DownloadLog downloadLog)
        {
            try
            {
                if (this._appConfiguration.ShouldLogInDb())
                {
                    using (var dbContext = new FileDownloadContext())
                    {
                        dbContext.DownloadLogs.Add(downloadLog);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {
                this._logger.LogWarning("Error occured during database entry : " + e.Message);
                return false;
            }

            return true;
        }
    }
}
