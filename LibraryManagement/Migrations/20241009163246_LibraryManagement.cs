using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagement.Migrations
{
    /// <inheritdoc />
    public partial class LibraryManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminID);
                });

            migrationBuilder.CreateTable(
                name: "BookCategories",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCategories", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Mssv = table.Column<int>(type: "int", nullable: false),
                    StudentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Class = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentID);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameBook = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Detail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookID);
                    table.ForeignKey(
                        name: "FK_Books_BookCategories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "BookCategories",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowingHistories",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowingHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookBorrowingHistories_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BookBorrowingHistories_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BookBorrowingLists",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentID = table.Column<int>(type: "int", nullable: false),
                    BookID = table.Column<int>(type: "int", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookBorrowingLists", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BookBorrowingLists_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookBorrowingLists_Students_StudentID",
                        column: x => x.StudentID,
                        principalTable: "Students",
                        principalColumn: "StudentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "AdminID", "PasswordHash", "Username" },
                values: new object[] { 1, "$2a$11$mN4.xi2strH66seOKFw.WObhI5u7WkdsTN4GZlbu/rgIyvoi7q7Re", "admin" });

            migrationBuilder.InsertData(
                table: "BookCategories",
                columns: new[] { "CategoryID", "CategoryDescription", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Sách thuộc lĩnh vực văn học, tiểu thuyết.", "Văn học" },
                    { 2, "Sách về tiểu thuyết, tiểu thuyết tự truyện.", "Tiểu thuyết" },
                    { 3, "Sách nghiên cứu về lịch sử.", "Lịch sử" },
                    { 4, "Sách về các học thuyết và triết lý.", "Triết học" },
                    { 5, "Sách về tâm lý và phát triển cá nhân.", "Tâm lý học" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "StudentID", "Class", "Mssv", "Phone", "StudentName" },
                values: new object[,]
                {
                    { 1, "CNTT-K19", 12201033, "0912345678", "Phan Hồ Anh Kha" },
                    { 2, "KTL-K17", 20230002, "0987654321", "Lê Thị Bích" },
                    { 3, "NNA-21", 20230003, "0911223344", "Phạm Văn Cường" },
                    { 4, "QHCC-K18", 20230004, "0933445566", "Trần Thị Duyên" },
                    { 5, "CNTT-K19", 12201015, "0977889900", "Trần Phúc Huynh" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookID", "Author", "CategoryID", "Detail", "ImageUrl", "NameBook", "Quantity" },
                values: new object[,]
                {
                    { 1, "Nam Cao", 1, "Một tác phẩm kinh điển của Nam Cao.", "https://product.hstatic.net/200000017360/product/chi-pheo_72e3f1370e484cf49b0fc94ee4ce0f80_master.jpg", "Chí Phèo", 10 },
                    { 2, "José Mauro de Vasconcelos", 2, "Cuốn sách chứa cả những cay đắng lẫn ngọt ngào.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLT_2rjZ30psJYUZnAc-EKeQ7atzhCoaJpJA&s", "Cây Cam Ngọt Của Tôi", 5 },
                    { 3, "Trần Trọng Kim", 3, "Cuốn sách tổng hợp lịch sử Việt Nam.", "https://product.hstatic.net/200000343865/product/viet-nam-su-luoc_ban-dac-biet_9781e7c1612941059b695bdb77302e61_master.jpg", "Việt Nam Sử Lược", 7 },
                    { 4, "Sigmund Freud", 4, "Cuốn sách về lý thuyết phân tâm học của Freud.", "https://bizweb.dktcdn.net/thumb/1024x1024/100/363/455/products/phantamhocnhapmon02.jpg?v=1705552542217", "Phân tâm học nhập môn", 4 },
                    { 5, "Dale Carnegie", 5, "Cuốn sách kinh điển về giao tiếp và quản lý con người.", "https://nhasachphuongnam.com/images/detailed/217/dac-nhan-tam-bc.jpg", "Đắc Nhân Tâm", 15 }
                });

            migrationBuilder.InsertData(
                table: "BookBorrowingLists",
                columns: new[] { "ID", "BookID", "BorrowDate", "DueDate", "Status", "StudentID" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 1 },
                    { 2, 2, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 2 },
                    { 3, 3, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingHistories_BookID",
                table: "BookBorrowingHistories",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingHistories_StudentID",
                table: "BookBorrowingHistories",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingLists_BookID",
                table: "BookBorrowingLists",
                column: "BookID");

            migrationBuilder.CreateIndex(
                name: "IX_BookBorrowingLists_StudentID",
                table: "BookBorrowingLists",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Books_CategoryID",
                table: "Books",
                column: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "BookBorrowingHistories");

            migrationBuilder.DropTable(
                name: "BookBorrowingLists");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "BookCategories");
        }
    }
}
