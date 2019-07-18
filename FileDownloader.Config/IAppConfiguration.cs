namespace FileDownloader.Config
{
    public interface IAppConfiguration
    {
        string GetDbConnectionString();

        int GetSlowSpeedThresholdInKBps();

        long GetBigFileSizeThresholdInKb();

        string GetDestinationPath();

        bool ShouldLogInDb();
    }
}
