using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class BookCategory
    {
        [Key] // Định nghĩa CategoryID là khóa chính
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
