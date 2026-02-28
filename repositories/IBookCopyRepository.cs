using LibraryManagement.models;

namespace LibraryManagement.repositories
{
    public interface IBookCopyRepository
    {
        List<BookCopy> GetAll();
        BookCopy? GetById(int id);
        void Add(BookCopy bookCopy);
        void Update(BookCopy bookCopy);
        void Delete(int id);
    }
}