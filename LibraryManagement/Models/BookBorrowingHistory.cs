using System;

namespace LibraryManagement.Models
{
    public class BookBorrowingHistory
    {
        public int ID { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
        public int BookID { get; set; }
        public Book Book { get; set; }
        public DateTime BorrowDate { get; set; } // Ngày mượn sách
        public DateTime DueDate { get; set; } // Ngày đến hạn trả sách
        public DateTime ReturnDate { get; set; } // Ngày trả sách
    }
}
