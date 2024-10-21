using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookCategoryRepository _bookCategoryRepository;

        // Inject BookRepository và BookCategoryRepository thông qua constructor
        public BookController(IBookRepository bookRepository, IBookCategoryRepository bookCategoryRepository)
        {
            _bookRepository = bookRepository;
            _bookCategoryRepository = bookCategoryRepository;
        }

        // Hiển thị danh sách sách từ cơ sở dữ liệu
        public IActionResult BooK() // Đổi tên từ Book() thành Index() theo chuẩn MVC
        {
            // Lấy tất cả sách từ repository
            var books = _bookRepository.GetAllBooks();

            // Truyền dữ liệu sách vào view
            return View(books);
        }

        // Trang để thêm sách mới
        public IActionResult Create()
        {
            // Lấy danh sách thể loại và truyền vào ViewBag để sử dụng trong dropdown list
            ViewBag.Categories = new SelectList(_bookCategoryRepository.GetAllCategories(), "CategoryID", "CategoryName");
            return View();
        }

        // Xử lý POST thêm sách mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookRepository.AddBook(book); // Thêm sách mới
                return RedirectToAction(nameof(Index)); // Quay lại trang danh sách sách
            }

            // Nếu có lỗi, load lại danh sách thể loại
            ViewBag.Categories = new SelectList(_bookCategoryRepository.GetAllCategories(), "CategoryID", "CategoryName");
            return View(book);
        }

        // Hiển thị chi tiết của một cuốn sách
        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookDetail(id);
            if (book == null)
            {
                return NotFound(); // Nếu không tìm thấy sách, trả về 404
            }
            return View(book); // Truyền dữ liệu chi tiết sách vào view
        }

        // Trang để chỉnh sửa thông tin sách
        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookDetail(id);
            if (book == null)
            {
                return NotFound(); // Nếu không tìm thấy sách, trả về 404
            }

            // Load lại danh sách thể loại
            ViewBag.Categories = new SelectList(_bookCategoryRepository.GetAllCategories(), "CategoryID", "CategoryName");
            return View(book); // Truyền dữ liệu sách vào form chỉnh sửa
        }

        // Xử lý POST cập nhật thông tin sách
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Book book)
        {
            if (id != book.BookID)
            {
                return NotFound(); // Nếu ID không khớp, trả về 404
            }

            if (ModelState.IsValid)
            {
                _bookRepository.UpdateBook(book); // Cập nhật thông tin sách
                return RedirectToAction(nameof(Index)); // Quay lại danh sách sách
            }

            // Nếu có lỗi, load lại danh sách thể loại
            ViewBag.Categories = new SelectList(_bookCategoryRepository.GetAllCategories(), "CategoryID", "CategoryName");
            return View(book);
        }

        // Xác nhận việc xóa sách
        public IActionResult Delete(int id)
        {
            var book = _bookRepository.GetBookDetail(id);
            if (book == null)
            {
                return NotFound(); // Nếu không tìm thấy sách, trả về 404
            }

            return View(book); // Truyền dữ liệu sách vào form xác nhận xóa
        }

        // Xử lý POST xóa sách
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var book = _bookRepository.GetBookDetail(id);
            if (book == null)
            {
                return NotFound();
            }

            // Kiểm tra xem sách có đang được mượn không
            if (book.BookBorrowingHistory != null && book.BookBorrowingHistory.Any())
            {
                ModelState.AddModelError("", "Không thể xóa sách vì sách này đang được mượn.");
                return View(book);
            }

            _bookRepository.DeleteBook(id); // Xóa sách
            return RedirectToAction(nameof(Index)); // Quay lại danh sách sách sau khi xóa
        }
    }
}
