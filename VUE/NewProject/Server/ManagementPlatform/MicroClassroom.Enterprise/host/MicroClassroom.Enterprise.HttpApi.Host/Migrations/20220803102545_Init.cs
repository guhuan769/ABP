using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroClassroom.Enterprise.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseCategory",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "分类名"),
                    Status = table.Column<int>(type: "int", nullable: true, comment: "分类状态"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_category_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mechanism",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    Pinyin = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "拼音或租户名"),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "图像"),
                    Slogo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "Slogo"),
                    Introduce = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "介绍"),
                    Grade = table.Column<int>(type: "int", nullable: true, comment: "等级"),
                    About = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true, comment: "关于我们"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_mechanism_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    MechanismId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "机构"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "标题"),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "图像"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_banner_id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_mechanism_banner_id",
                        column: x => x.MechanismId,
                        principalTable: "Mechanism",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    MechanismId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "课程分类"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, comment: "名称"),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "图像"),
                    Price = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false, comment: "课程价格"),
                    HasPay = table.Column<bool>(type: "bit", nullable: true, comment: "是否付费"),
                    Introduce = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "介绍"),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "开始时间"),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "结束时间"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id"),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_category_course_id",
                        column: x => x.CategoryId,
                        principalTable: "CourseCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_mechanism_course_id",
                        column: x => x.MechanismId,
                        principalTable: "Mechanism",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    MechanismId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "机构"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "名称"),
                    Image = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "图像"),
                    Introduce = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "介绍"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_teacher_id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_mechanism_teacher_id",
                        column: x => x.MechanismId,
                        principalTable: "Mechanism",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseItem",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "课程id"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, comment: "目录名"),
                    Order = table.Column<int>(type: "int", nullable: false, comment: "排序"),
                    Duration = table.Column<float>(type: "real", nullable: false, comment: "视频时长"),
                    Video = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "视频地址"),
                    StartAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "开始时间"),
                    EndAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "结束时间"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_item_id", x => x.Id);
                    table.ForeignKey(
                        name: "fk_course_item_id",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CourseTeacher",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "课程id"),
                    TeacherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "教师id"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_teacher_id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseTeacher_Course_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Course",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banner_MechanismId",
                table: "Banner",
                column: "MechanismId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CategoryId",
                table: "Course",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Course_MechanismId",
                table: "Course",
                column: "MechanismId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseItem_CourseId",
                table: "CourseItem",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseTeacher_CourseId",
                table: "CourseTeacher",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Teacher_MechanismId",
                table: "Teacher",
                column: "MechanismId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "CourseItem");

            migrationBuilder.DropTable(
                name: "CourseTeacher");

            migrationBuilder.DropTable(
                name: "Teacher");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "CourseCategory");

            migrationBuilder.DropTable(
                name: "Mechanism");
        }
    }
}
