using System.Collections.Generic;

namespace LibraryManagement.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string NameBook { get; set; }
        public string Detail { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public int Quantity { get; set; }
        public int CategoryID { get; set; }
        public BookCategory Category { get; set; }
        public ICollection<BookBorrowingHistory> BookBorrowingHistory { get; set; } // Đảm bảo thuộc tính này tồn tại
    }
}
