using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookCategoryController : Controller
    {
        private readonly IBookCategoryRepository _bookCategoryRepository;

        // Inject BookCategoryRepository thông qua constructor
        public BookCategoryController(IBookCategoryRepository bookCategoryRepository)
        {
            _bookCategoryRepository = bookCategoryRepository;
        }

        // Hiển thị danh sách thể loại từ cơ sở dữ liệu
        public IActionResult BookCategory()
        {
            var categories = _bookCategoryRepository.GetAllCategories(); // Lấy tất cả thể loại từ repository
            return View(categories); // Truyền dữ liệu thể loại vào view
        }

        // Trang để thêm thể loại sách mới
        public IActionResult Create()
        {
            return View();
        }

        // Xử lý POST thêm thể loại sách mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookCategory category)
        {
            if (ModelState.IsValid)
            {
                _bookCategoryRepository.AddCategory(category); // Thêm thể loại sách mới
                return RedirectToAction(nameof(Index)); // Quay lại trang danh sách thể loại
            }
            return View(category);
        }

        // Hiển thị chi tiết của một thể loại
        public IActionResult Details(int id)
        {
            var category = _bookCategoryRepository.GetCategoryDetail(id);
            if (category == null)
            {
                return NotFound(); // Nếu không tìm thấy thể loại, trả về 404
            }
            return View(category); // Truyền dữ liệu chi tiết thể loại vào view
        }

        // Trang để chỉnh sửa thông tin thể loại
        public IActionResult Edit(int id)
        {
            var category = _bookCategoryRepository.GetCategoryDetail(id);
            if (category == null)
            {
                return NotFound(); // Nếu không tìm thấy thể loại, trả về 404
            }
            return View(category); // Truyền dữ liệu thể loại vào form chỉnh sửa
        }

        // Xử lý POST cập nhật thông tin thể loại
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BookCategory category)
        {
            if (id != category.CategoryID)
            {
                return BadRequest(); // Nếu ID không khớp, trả về lỗi yêu cầu không hợp lệ
            }

            if (ModelState.IsValid)
            {
                _bookCategoryRepository.UpdateCategory(category); // Cập nhật thông tin thể loại
                return RedirectToAction(nameof(Index)); // Quay lại danh sách thể loại
            }
            return View(category);
        }

        // Xác nhận việc xóa thể loại
        public IActionResult Delete(int id)
        {
            var category = _bookCategoryRepository.GetCategoryDetail(id);
            if (category == null)
            {
                return NotFound(); // Nếu không tìm thấy thể loại, trả về 404
            }
            return View(category); // Truyền dữ liệu thể loại vào form xác nhận xóa
        }

        // Xử lý POST xóa thể loại
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = _bookCategoryRepository.GetCategoryDetail(id);
            if (category == null)
            {
                return NotFound(); // Nếu không tìm thấy thể loại, trả về 404
            }

            _bookCategoryRepository.DeleteCategory(id); // Xóa thể loại
            return RedirectToAction(nameof(Index)); // Quay lại danh sách thể loại sau khi xóa
        }
    }
}
