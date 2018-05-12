using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareTeamwork.Migrations
{
    public partial class WORKDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountInfos",
                columns: table => new
                {
                    AccountInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountInfos", x => x.AccountInfoId);
                });

            migrationBuilder.CreateTable(
                name: "DateTimes",
                columns: table => new
                {
                    DateTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Mon = table.Column<int>(nullable: false),
                    Day = table.Column<int>(nullable: false),
                    Hour = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DateTimes", x => x.DateTimeId);
                });

            migrationBuilder.CreateTable(
                name: "CourseInfos",
                columns: table => new
                {
                    CourseInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AccountInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseInfos", x => x.CourseInfoId);
                    table.ForeignKey(
                        name: "FK_CourseInfos_AccountInfos_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfos",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowInfos",
                columns: table => new
                {
                    FlowInfoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlowData = table.Column<double>(nullable: false),
                    InfoTimeDateTimeId = table.Column<int>(nullable: true),
                    AccountInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowInfos", x => x.FlowInfoId);
                    table.ForeignKey(
                        name: "FK_FlowInfos_AccountInfos_AccountInfoId",
                        column: x => x.AccountInfoId,
                        principalTable: "AccountInfos",
                        principalColumn: "AccountInfoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowInfos_DateTimes_InfoTimeDateTimeId",
                        column: x => x.InfoTimeDateTimeId,
                        principalTable: "DateTimes",
                        principalColumn: "DateTimeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CourseSets",
                columns: table => new
                {
                    CourseSetId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Term = table.Column<string>(nullable: true),
                    CourseInfoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSets", x => x.CourseSetId);
                    table.ForeignKey(
                        name: "FK_CourseSets_CourseInfos_CourseInfoId",
                        column: x => x.CourseInfoId,
                        principalTable: "CourseInfos",
                        principalColumn: "CourseInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseName = table.Column<string>(nullable: true),
                    CourseTeacher = table.Column<string>(nullable: true),
                    CourseLoc = table.Column<string>(nullable: true),
                    CourseTime = table.Column<int>(nullable: false),
                    CourseBegin = table.Column<int>(nullable: false),
                    CourseEnd = table.Column<int>(nullable: false),
                    CourseSetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_CourseSets_CourseSetId",
                        column: x => x.CourseSetId,
                        principalTable: "CourseSets",
                        principalColumn: "CourseSetId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseInfos_AccountInfoId",
                table: "CourseInfos",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseSetId",
                table: "Courses",
                column: "CourseSetId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSets_CourseInfoId",
                table: "CourseSets",
                column: "CourseInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowInfos_AccountInfoId",
                table: "FlowInfos",
                column: "AccountInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowInfos_InfoTimeDateTimeId",
                table: "FlowInfos",
                column: "InfoTimeDateTimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "FlowInfos");

            migrationBuilder.DropTable(
                name: "CourseSets");

            migrationBuilder.DropTable(
                name: "DateTimes");

            migrationBuilder.DropTable(
                name: "CourseInfos");

            migrationBuilder.DropTable(
                name: "AccountInfos");
        }
    }
}
