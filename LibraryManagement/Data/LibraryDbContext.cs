using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace LibraryManagement.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<BookBorrowingHistory> BookBorrowingHistories { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Set up relationships
            builder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);
        
            builder.Entity<BookBorrowingHistory>()
                .HasOne(bbh => bbh.Student)
                .WithMany()
                .HasForeignKey(bbh => bbh.StudentID)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookBorrowingHistory>()
                .HasOne(bbh => bbh.Book)
                .WithMany()
                .HasForeignKey(bbh => bbh.BookID)
                .OnDelete(DeleteBehavior.Restrict);

            // Seed sample data
            SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            // Sample data for Admin table
            var passwordHash = BCrypt.Net.BCrypt.HashPassword("admin_password"); // Hash password
            builder.Entity<Admin>().HasData(
                new Admin
                {
                    AdminID = 1,
                    Username = "admin",
                    PasswordHash = passwordHash
                }
            );

            // Sample data for BookCategory table
            builder.Entity<BookCategory>().HasData(
                new BookCategory { CategoryID = 1, CategoryName = "Văn học", CategoryDescription = "Sách thuộc lĩnh vực văn học, tiểu thuyết." },
                new BookCategory { CategoryID = 2, CategoryName = "Tiểu thuyết", CategoryDescription = "Sách về tiểu thuyết, tiểu thuyết tự truyện." },
                new BookCategory { CategoryID = 3, CategoryName = "Lịch sử", CategoryDescription = "Sách nghiên cứu về lịch sử." },
                new BookCategory { CategoryID = 4, CategoryName = "Triết học", CategoryDescription = "Sách về các học thuyết và triết lý." },
                new BookCategory { CategoryID = 5, CategoryName = "Tâm lý học", CategoryDescription = "Sách về tâm lý và phát triển cá nhân." }
            );

            // Sample data for Book table
            builder.Entity<Book>().HasData(
                new Book { BookID = 1, NameBook = "Chí Phèo", Detail = "Một tác phẩm kinh điển của Nam Cao.", ImageUrl = "https://product.hstatic.net/200000017360/product/chi-pheo_72e3f1370e484cf49b0fc94ee4ce0f80_master.jpg", Author = "Nam Cao", Quantity = 10, CategoryID = 1 },
                new Book { BookID = 2, NameBook = "Cây Cam Ngọt Của Tôi", Detail = "Cuốn sách chứa cả những cay đắng lẫn ngọt ngào.", ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSLT_2rjZ30psJYUZnAc-EKeQ7atzhCoaJpJA&s", Author = "José Mauro de Vasconcelos", Quantity = 5, CategoryID = 2 },
                new Book { BookID = 3, NameBook = "Việt Nam Sử Lược", Detail = "Cuốn sách tổng hợp lịch sử Việt Nam.", ImageUrl = "https://product.hstatic.net/200000343865/product/viet-nam-su-luoc_ban-dac-biet_9781e7c1612941059b695bdb77302e61_master.jpg", Author = "Trần Trọng Kim", Quantity = 7, CategoryID = 3 },
                new Book { BookID = 4, NameBook = "Phân tâm học nhập môn", Detail = "Cuốn sách về lý thuyết phân tâm học của Freud.", ImageUrl = "https://bizweb.dktcdn.net/thumb/1024x1024/100/363/455/products/phantamhocnhapmon02.jpg?v=1705552542217", Author = "Sigmund Freud", Quantity = 4, CategoryID = 4 },
                new Book { BookID = 5, NameBook = "Đắc Nhân Tâm", Detail = "Cuốn sách kinh điển về giao tiếp và quản lý con người.", ImageUrl = "https://nhasachphuongnam.com/images/detailed/217/dac-nhan-tam-bc.jpg", Author = "Dale Carnegie", Quantity = 15, CategoryID = 5 }
            );

            // Sample data for Student table
            builder.Entity<Student>().HasData(
                new Student { StudentID = 1, Mssv = 12201033, StudentName = "Phan Hồ Anh Kha", Class = "CNTT-K19", Phone = "0912345678" },
                new Student { StudentID = 2, Mssv = 20230002, StudentName = "Lê Thị Bích", Class = "KTL-K17", Phone = "0987654321" },
                new Student { StudentID = 3, Mssv = 20230003, StudentName = "Phạm Văn Cường", Class = "NNA-21", Phone = "0911223344" },
                new Student { StudentID = 4, Mssv = 20230004, StudentName = "Trần Thị Duyên", Class = "QHCC-K18", Phone = "0933445566" },
                new Student { StudentID = 5, Mssv = 12201015, StudentName = "Trần Phúc Huynh", Class = "CNTT-K19", Phone = "0977889900" }
            );

            // Sample data for BookBorrowingHistory table
            builder.Entity<BookBorrowingHistory>().HasData(
                new BookBorrowingHistory { ID = 1, StudentID = 1, BookID = 1, BorrowDate = new DateTime(2023, 11, 1), DueDate = new DateTime(2023, 11, 15), ReturnDate = new DateTime(2023, 11, 14) },
                new BookBorrowingHistory { ID = 2, StudentID = 2, BookID = 2, BorrowDate = new DateTime(2023, 10, 10), DueDate = new DateTime(2023, 10, 25), ReturnDate = new DateTime(2023, 10, 24) },
                new BookBorrowingHistory { ID = 3, StudentID = 3, BookID = 3, BorrowDate = new DateTime(2023, 9, 5), DueDate = new DateTime(2023, 9, 20), ReturnDate = new DateTime(2023, 9, 19) }
            );
        }
    }
}