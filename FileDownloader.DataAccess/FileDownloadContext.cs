namespace FileDownloader.DataAccess
{
    using FileDownloader.Config;
    using FileDownloader.DataAccess.Model;

    using Microsoft.EntityFrameworkCore;

    public class FileDownloadContext : DbContext
    {
        public DbSet<DownloadLog> DownloadLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var appConfig = new AppConfiguration();
            optionsBuilder.UseSqlServer(appConfig.GetDbConnectionString());
        }
    }
}
