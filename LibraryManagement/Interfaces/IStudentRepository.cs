using LibraryManagement.Models;

namespace LibraryManagement.Interfaces
{
   public interface IStudentRepository
   {
            IEnumerable<Student> GetAllStudents();
            Student GetStudentDetail(int id);
            void AddStudent(Student student);
            void UpdateStudent(Student student);
            void DeleteStudent(int id);
        }
    }

