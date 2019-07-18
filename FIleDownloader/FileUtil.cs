using System;

namespace FileDownloader
{
    using System.IO;

    public static class FileUtil
    {
        public static void DeleteDownloadedFile(string destinationPath)
        {
            try
            {
                File.Delete(destinationPath);
            }
            catch
            {
                throw;
            }
        }

        public static string GetFilename(string hrefLink)
        {
            var uri = new Uri(hrefLink);

            var filename = System.IO.Path.GetFileName(uri.LocalPath);

            return filename;
        }
    }
}
