using LibraryManagement.repositories;
using LibraryManagement.models;
namespace LibraryManagement.services
{
    /// <summary>
    /// definition de la classe BookService qui implémente l'interface BookService,
    /// elle est responsable de fournir les implémentations concrètes des méthodes définies dans l'interface pour gérer les livres dans l'application. Cette classe utilise une instance de BookRepository pour accéder aux données des livres et effectuer les opérations nécessaires pour répondre aux demandes de la couche de présentation ou d'autres parties de l'application qui interagissent avec les services de gestion des livres.
    /// </summary>
    /// 

    class BookService : IBookService
    {
        public readonly IBookRepository _repository;

        public BookService(IBookRepository repository)
        {
            _repository = repository;
        }
        public void Add(Book book)
        {
            _repository.Add(book);
        }

        public List<Book> GetAll()
        {
            return _repository.GetAll();
        }

        public Book? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public void Update(Book book)
        {
            _repository.Update(book);
        }
        public void Delete(int id)
        {
            _repository.Delete(id);
        }
    }
}