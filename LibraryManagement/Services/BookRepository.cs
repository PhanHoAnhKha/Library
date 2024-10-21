using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Services
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _dbContext;

        // Inject DbContext để BookRepository có thể thao tác với cơ sở dữ liệu
        public BookRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Lấy tất cả sách từ cơ sở dữ liệu, bao gồm thể loại
        public IEnumerable<Book> GetAllBooks()
        {
            return _dbContext.Books.Include(b => b.Category).ToList(); // Sử dụng Include để lấy thể loại
        }

        // Lấy chi tiết của một cuốn sách từ cơ sở dữ liệu dựa trên ID, bao gồm thể loại
        public Book? GetBookDetail(int id)
        {
            return _dbContext.Books
                .Include(b => b.Category) // Kèm theo thể loại
                .FirstOrDefault(b => b.BookID == id);
        }

        // Thêm sách mới vào cơ sở dữ liệu
        public void AddBook(Book book)
        {
            _dbContext.Books.Add(book);
            _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Cập nhật thông tin sách trong cơ sở dữ liệu
        public void UpdateBook(Book book)
        {
            var existingBook = _dbContext.Books.FirstOrDefault(b => b.BookID == book.BookID);
            if (existingBook != null)
            {
                existingBook.NameBook = book.NameBook;
                existingBook.Detail = book.Detail;
                existingBook.Author = book.Author;
                existingBook.Quantity = book.Quantity;
                existingBook.ImageUrl = book.ImageUrl;
                existingBook.CategoryID = book.CategoryID;

                _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Xóa sách khỏi cơ sở dữ liệu
        public void DeleteBook(int id)
        {
            var book = _dbContext.Books.FirstOrDefault(b => b.BookID == id);
            if (book != null)
            {
                _dbContext.Books.Remove(book);
                _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            }
        }
    }
}
