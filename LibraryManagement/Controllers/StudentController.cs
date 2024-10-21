using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        // Inject StudentRepository thông qua constructor
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        // Hiển thị danh sách sinh viên từ cơ sở dữ liệu
        public IActionResult Student() // Đổi tên từ Student() thành Index()
        {
            var students = _studentRepository.GetAllStudents(); // Lấy tất cả sinh viên từ repository
            return View(students); // Truyền dữ liệu sinh viên vào view
        }

        // Trang để thêm sinh viên mới
        public IActionResult Create()
        {
            return View();
        }

        // Xử lý POST thêm sinh viên mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _studentRepository.AddStudent(student); // Thêm sinh viên mới
                return RedirectToAction(nameof(Index)); // Quay lại trang danh sách sinh viên
            }
            return View(student);
        }

        // Hiển thị chi tiết của một sinh viên
        public IActionResult Details(int id)
        {
            var student = _studentRepository.GetStudentDetail(id);
            if (student == null)
            {
                return NotFound(); // Nếu không tìm thấy sinh viên, trả về 404
            }
            return View(student); // Truyền dữ liệu chi tiết sinh viên vào view
        }

        // Trang để chỉnh sửa thông tin sinh viên
        public IActionResult Edit(int id)
        {
            var student = _studentRepository.GetStudentDetail(id);
            if (student == null)
            {
                return NotFound(); // Nếu không tìm thấy sinh viên, trả về 404
            }
            return View(student); // Truyền dữ liệu sinh viên vào form chỉnh sửa
        }

        // Xử lý POST cập nhật thông tin sinh viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            if (id != student.StudentID)
            {
                return NotFound(); // Nếu ID không khớp, trả về 404
            }

            if (ModelState.IsValid)
            {
                _studentRepository.UpdateStudent(student); // Cập nhật thông tin sinh viên
                return RedirectToAction(nameof(Index)); // Quay lại danh sách sinh viên
            }
            return View(student);
        }

        // Xác nhận việc xóa sinh viên
        public IActionResult Delete(int id)
        {
            var student = _studentRepository.GetStudentDetail(id);
            if (student == null)
            {
                return NotFound(); // Nếu không tìm thấy sinh viên, trả về 404
            }
            return View(student); // Truyền dữ liệu sinh viên vào form xác nhận xóa
        }

        // Xử lý POST xóa sinh viên
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _studentRepository.GetStudentDetail(id);
            if (student == null)
            {
                return NotFound(); // Nếu không tìm thấy sinh viên, trả về 404
            }

            _studentRepository.DeleteStudent(id); // Xóa sinh viên
            return RedirectToAction(nameof(Index)); // Quay lại danh sách sinh viên sau khi xóa
        }
    }
}
