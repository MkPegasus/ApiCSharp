using LibraryManagement.models;
using LibraryManagement.repositories;

namespace LibraryManagement.services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;

        public AuthorService(IAuthorRepository repository)
        {
            _repository = repository;
        }

        public List<Author> GetAll()
        {
            return _repository.GetAll();
        }

        public Author? GetById(int id)
        {
           return _repository.GetById(id);
           
        }

        public void Add(Author author)
        {
            _repository.Add(author);
        }

        public void Update(Author author)
        { 
            if (_repository.GetById(author.Id) == null)
                throw new Exception($"Auteur avec l'id {author.Id} introuvable.");

            _repository.Update(author);
        }

        public void Delete(int id)
        {
            Author? existing = _repository.GetById(id);
            if (existing == null)
                throw new Exception($"Auteur avec l'id {id} introuvable.");

            _repository.Delete(id);
        }
    }
}