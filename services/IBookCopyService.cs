using LibraryManagement.models;

namespace LibraryManagement.services
{
    public interface IBookCopyService
    {
        List<BookCopy> GetAll();
        BookCopy? GetById(int id);
        void Add(BookCopy bookCopy);
        void Update(BookCopy bookCopy);
        void Delete(int id);
    }
}