Prerequisites -
- .net core 2.2
- MS SQL Server

Build & Run instructions -
1. Set "FileDownloader.Client" as Startup project
2. Inside "FileDownloader.Client" update the "sources.json" file.
	2.1. There is a array of files.
	2.2. Insert valid file url in "FileUrl"
	2.3. If there is credential needed to access the file insert it in "Credentials"
3. Inside "FileDownloader.Client" update the "appsettings.json" file as required
	3.1. Update the Connection string "FileDownloadContext" as required
	3.2. Update "AppConfig" section
		3.2.1. SlowSpeedKBps = threshold speed, anything below this will be considered slow
		3.2.2. BigFileSizeKB = threshold size, anything avove this will be considered big file
		3.2.3. DestinationPath = local directory where file will be downloaded
		3.2.4. LogInDb : set true if need to log in DB, otherwise set false
4. Now the solution is ready to Build and Run

Unit tests -
- There are two unit test projects to test some basic scenarios

Output -
- in console there will be some text logged, which is pretty staright forward