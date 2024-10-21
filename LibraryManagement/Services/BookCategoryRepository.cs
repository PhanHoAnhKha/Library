using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Services
{
    public class BookCategoryRepository : IBookCategoryRepository
    {
        private readonly LibraryDbContext _dbContext;

        // Inject DbContext để BookCategoryRepository có thể thao tác với cơ sở dữ liệu
        public BookCategoryRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Lấy tất cả thể loại từ cơ sở dữ liệu
        public IEnumerable<BookCategory> GetAllCategories()
        {
            return _dbContext.BookCategories.ToList(); // Trả về danh sách thể loại
        }

        // Lấy chi tiết của một thể loại sách từ cơ sở dữ liệu dựa trên ID
        public BookCategory? GetCategoryDetail(int id)
        {
            return _dbContext.BookCategories.FirstOrDefault(c => c.CategoryID == id);
        }

        // Thêm thể loại mới vào cơ sở dữ liệu
        public void AddCategory(BookCategory category)
        {
            _dbContext.BookCategories.Add(category);
            _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Cập nhật thông tin thể loại trong cơ sở dữ liệu
        public void UpdateCategory(BookCategory category)
        {
            var existingCategory = _dbContext.BookCategories.FirstOrDefault(c => c.CategoryID == category.CategoryID);
            if (existingCategory != null)
            {
                existingCategory.CategoryName = category.CategoryName;
                existingCategory.CategoryDescription = category.CategoryDescription;

                _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Xóa thể loại khỏi cơ sở dữ liệu
        public void DeleteCategory(int id)
        {
            var category = _dbContext.BookCategories.FirstOrDefault(c => c.CategoryID == id);
            if (category != null)
            {
                _dbContext.BookCategories.Remove(category);
                _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            }
        }
    }
}
