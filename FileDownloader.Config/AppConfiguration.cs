using System;
using System.Collections.Generic;
using System.Text;

namespace FileDownloader.Config
{
    using System.IO;

    using Microsoft.Extensions.Configuration;

    public class AppConfiguration : IAppConfiguration
    {

        private readonly IConfigurationRoot _configRoot;
        public AppConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            this._configRoot = builder.Build();
        }

        public string GetDbConnectionString()
        {
            try
            {
                var connectionString = this._configRoot.GetConnectionString("FileDownloadContext");
                return connectionString;
            }
            catch
            {
                throw new Exception("Invalid ConnectionString in appsettings.json");
            }
        }

        public int GetSlowSpeedThresholdInKBps()
        {
            try
            {
                var speedkbps = this._configRoot?.GetSection("AppConfig")?["SlowSpeedKBps"];

                return Int32.Parse(speedkbps);
            }
            catch
            {
                throw new Exception("Invalid SlowSpeedKBps in appsettings.json");
            }
        }

        public long GetBigFileSizeThresholdInKb()
        {
            try
            {
                var bigFileSize = this._configRoot?.GetSection("AppConfig")?["BigFileSizeKB"];
                return long.Parse(bigFileSize);
            }
            catch (Exception)
            {
                throw new Exception("Invalid BigFileSizeKB in appsettings.json");
            }
        }

        public string GetDestinationPath()
        {
            try
            {
                var destPath = this._configRoot?.GetSection("AppConfig")?["DestinationPath"];
                return destPath;
            }
            catch (Exception)
            {
                throw new Exception("Invalid DestinationPath in appsettings.json");
            }
        }

        public bool ShouldLogInDb()
        {
            try
            {
                var shouldLog = this._configRoot?.GetSection("AppConfig")?["LogInDb"];
                return bool.Parse(shouldLog);
            }
            catch (Exception)
            {
                throw new Exception("Invalid LogInDb flag in appsettings.json");
            }
        }
    }
}
