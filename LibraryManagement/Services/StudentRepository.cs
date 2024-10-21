using LibraryManagement.Data;
using LibraryManagement.Interfaces;
using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LibraryManagement.Services
{
    public class StudentRepository : IStudentRepository
    {
        private readonly LibraryDbContext _dbContext;

        // Inject DbContext để StudentRepository có thể thao tác với cơ sở dữ liệu
        public StudentRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Lấy tất cả sinh viên từ cơ sở dữ liệu
        public IEnumerable<Student> GetAllStudents()
        {
            return _dbContext.Students.ToList();
        }

        // Lấy chi tiết của một sinh viên từ cơ sở dữ liệu dựa trên ID
        public Student GetStudentDetail(int id)
        {
            return _dbContext.Students.FirstOrDefault(s => s.StudentID == id);
        }

        // Thêm sinh viên mới vào cơ sở dữ liệu
        public void AddStudent(Student student)
        {
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
        }

        // Cập nhật thông tin sinh viên trong cơ sở dữ liệu
        public void UpdateStudent(Student student)
        {
            var existingStudent = _dbContext.Students.FirstOrDefault(s => s.StudentID == student.StudentID);
            if (existingStudent != null)
            {
                existingStudent.StudentName = student.StudentName;
                existingStudent.Class = student.Class;
                existingStudent.Phone = student.Phone;

                _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            }
        }

        // Xóa sinh viên khỏi cơ sở dữ liệu
        public void DeleteStudent(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.StudentID == id);
            if (student != null)
            {
                _dbContext.Students.Remove(student);
                _dbContext.SaveChanges();  // Lưu thay đổi vào cơ sở dữ liệu
            }
        }
    }
}
