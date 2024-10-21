using LibraryManagement.Models;
using System.Collections.Generic;

namespace LibraryManagement.Interfaces
{
    public interface IBookCategoryRepository
    {
        IEnumerable<BookCategory> GetAllCategories();
        BookCategory? GetCategoryDetail(int id);
        void AddCategory(BookCategory category);
        void UpdateCategory(BookCategory category);
        void DeleteCategory(int id);
    }
}
