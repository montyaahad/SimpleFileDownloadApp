using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FileDownloader.DataAccess.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DownloadLogs",
                columns: table => new
                {
                    DownloadLogId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DownloadState = table.Column<string>(nullable: true),
                    LocalFilePath = table.Column<string>(nullable: true),
                    FileSource = table.Column<string>(nullable: true),
                    Protocol = table.Column<string>(nullable: true),
                    Error = table.Column<string>(nullable: true),
                    DownloadTime = table.Column<TimeSpan>(nullable: false),
                    BytesTotal = table.Column<long>(nullable: false),
                    DownloadSpeedInKiloBytesPerSecond = table.Column<int>(nullable: false),
                    IsSpeedSlow = table.Column<bool>(nullable: false),
                    IsFileBig = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadLogs", x => x.DownloadLogId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DownloadLogs");
        }
    }
}
