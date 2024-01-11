using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MicroClassroom.Customer.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "课程id"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "用户id"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false, comment: "评论内容"),
                    Star = table.Column<int>(type: "int", nullable: false, comment: "评分"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "评论时间"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_comment_id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purchase",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "主键标识"),
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "课程id"),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "用户id"),
                    Price = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false, comment: "课程价格"),
                    IsPay = table.Column<bool>(type: "bit", nullable: true, comment: "实付付款"),
                    Discount = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: true, comment: "优惠金额"),
                    PayIn = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: true, comment: "实付金额"),
                    CreateAt = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "评论时间"),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true, comment: "租户id")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_purchase_id", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Purchase");
        }
    }
}
