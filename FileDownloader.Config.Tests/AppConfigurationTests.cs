using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FileDownloader.Config.Tests
{
    [TestClass]
    public class AppConfigurationTests
    {
        [TestMethod]
        public void CheckDbConnectionString()
        {
            var config = new AppConfiguration();
            var connStr = config.GetDbConnectionString();

            Assert.AreEqual("Data Source=.;Initial Catalog=test;User ID=test;Password=test;", connStr);
        }

        [TestMethod]
        public void CheckSlowSpeedThresholdInKBps()
        {
            var config = new AppConfiguration();
            var speedThreshold = config.GetSlowSpeedThresholdInKBps();

            Assert.AreEqual(1, speedThreshold);
        }

        [TestMethod]
        public void CheckBigFileSizeThresholdInKb()
        {
            var config = new AppConfiguration();
            var fileSizeThreshold = config.GetBigFileSizeThresholdInKb();

            Assert.AreEqual(10, fileSizeThreshold);
        }

        [TestMethod]
        public void CheckDestinationPath()
        {
            var config = new AppConfiguration();
            var destPath = config.GetDestinationPath();

            Assert.AreEqual("D:\\test", destPath);
        }

        [TestMethod]
        public void CheckLogInDbFlag()
        {
            var config = new AppConfiguration();
            var shouldLog = config.ShouldLogInDb();

            Assert.AreEqual(false, shouldLog);
        }
    }
}
