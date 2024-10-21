using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public int Mssv { get; set; }
        public string StudentName { get; set; }
        public string Class { get; set; }
        public string Phone { get; set; }
        public ICollection<BookBorrowingHistory> BookBorrowingHistory { get; set; } // Đảm bảo thuộc tính này tồn tại
      
    }
}
