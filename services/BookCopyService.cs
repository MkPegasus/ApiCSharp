using LibraryManagement.models;
using LibraryManagement.repositories;

namespace LibraryManagement.services
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IBookCopyRepository _repository;

        public BookCopyService(IBookCopyRepository repository)
        {
            _repository = repository;
        }

        public List<BookCopy> GetAll()
        {
            return _repository.GetAll();
        }

        public BookCopy? GetById(int id)
        {
            BookCopy? copy = _repository.GetById(id);

            if (copy == null)
                throw new Exception($"Exemplaire avec l'id {id} introuvable.");

            return copy;
        }

        public void Add(BookCopy bookCopy)
        {
            if (string.IsNullOrWhiteSpace(bookCopy.Barcode))
                throw new Exception("Le code-barres est obligatoire.");

            if (bookCopy.Book == null)
                throw new Exception("Le livre associé est obligatoire.");

            _repository.Add(bookCopy);
        }

        public void Update(BookCopy bookCopy)
        {
            BookCopy? existing = _repository.GetById(bookCopy.Id);
            if (existing == null)
                throw new Exception($"Exemplaire avec l'id {bookCopy.Id} introuvable.");

            if (string.IsNullOrWhiteSpace(bookCopy.Barcode))
                throw new Exception("Le code-barres est obligatoire.");

            _repository.Update(bookCopy);
        }

        public void Delete(int id)
        {
            BookCopy? existing = _repository.GetById(id);
            if (existing == null)
                throw new Exception($"Exemplaire avec l'id {id} introuvable.");

            _repository.Delete(id);
        }
    }
}