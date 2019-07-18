using System;

namespace FileDownloader.Client
{
    using System.Collections.Generic;
    using System.IO;

    using FileDownloader.Config;

    using global::FileDownloader.Business;
    using global::FileDownloader.Model;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    using Newtonsoft.Json;

    class Program
    {
        static void Main(string[] args)
        {
            #region Dependency Injection

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddScoped<IFileDownloaderFactory, FileDownloaderFactory>()
                .AddScoped<IDownloadService, DownloadService>()
                .AddScoped<IAppConfiguration, AppConfiguration>()
                .AddScoped<IDbLogService, DbLogService>()
                .BuildServiceProvider();

            //configure console logging
            serviceProvider?.GetService<ILoggerFactory>()?.AddConsole();

            var logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<Program>(); 
            #endregion

            logger.LogInformation("Starting application");


            SourceFiles sources;

            using (StreamReader r = new StreamReader("sources.json"))
            {
                string json = r.ReadToEnd();
                sources = JsonConvert.DeserializeObject<SourceFiles>(json);
            }

            var service = serviceProvider.GetService<IDownloadService>();
            service.DownloadServiceCompleted += ServiceOnDownloadServiceCompleted;
            service.DownloadFiles(sources.Files);

            Console.Read();
        }

        private static void ServiceOnDownloadServiceCompleted(object sender, DownloadServiceCompletedArgs e)
        {
            Console.WriteLine("Press any key to close!");
        }
    }

    
}
