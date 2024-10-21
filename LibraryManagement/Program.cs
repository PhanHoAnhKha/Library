using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IBookRepository, BookRepository>(); // Đăng ký dịch vụ Book
builder.Services.AddScoped<IStudentRepository, StudentRepository>(); // Đăng ký dịch vụ Student
builder.Services.AddScoped<IBookCategoryRepository, BookCategoryRepository>(); // Đăng ký dịch vụ Thể Loại Sách.
//builder.Services.AddScoped<IBookBorrowingHistoryRepository, BookBorrowingHistoryRepository>(); // Đăng ký dịch vụ Lịch sử mượn sách.





builder.Services.AddDbContext<LibraryDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("LibraryDbContextConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
