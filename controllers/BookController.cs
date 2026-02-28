using System.Text.Json;
using LibraryManagement.services;
using LibraryManagement.models;
using LibraryManagement.Routing;

namespace LibraryManagement.controllers
{
    /// <summary>
    /// Contrôleur responsable de la gestion des livres.
    /// Reçoit les requêtes HTTP, appelle le BookService 
    /// et retourne les réponses au format JSON.
    /// </summary>
     class BookController
    {
        // Injection du service Book pour accéder à la logique métier
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        /// GET /api/books
        /// Retourne la liste complète des livres au format JSON.
        /// </summary>
        [HttpGet("/api/books")]
        public string GetAll()
        {
            var books = _bookService.GetAll();
            return JsonSerializer.Serialize(books);
        }

        /// <summary>
        /// GET /api/books/{id}
        /// Retourne un livre spécifique par son identifiant.
        /// </summary>
        [HttpGet("/api/books/{id}")]
        public string GetById(int id)
        {
            var book = _bookService.GetById(id);
            return JsonSerializer.Serialize(book);
        }

        /// <summary>
        /// POST /api/books
        /// Crée un nouveau livre à partir des données reçues.
        /// </summary>
        [HttpPost("/api/books")]
        public string Create(Book book)
        {
            _bookService.Add(book);
            return JsonSerializer.Serialize(new { message = "Livre créé avec succès." });
        }

        /// <summary>
        /// PUT /api/books/{id}
        /// Met à jour les informations d'un livre existant.
        /// </summary>
        [HttpPut("/api/books/{id}")]
        public string Update(int id, Book book)
        {
            book.Id = id;
            _bookService.Update(book);
            return JsonSerializer.Serialize(new { message = "Livre mis à jour avec succès.", id });
        }

        /// <summary>
        /// DELETE /api/books/{id}
        /// Supprime un livre par son identifiant.
        /// </summary>
        [HttpDelete("/api/books/{id}")]
        public string Delete(int id)
        {
            _bookService.Delete(id);
            return JsonSerializer.Serialize(new { message = "Livre supprimé avec succès.", id });
        }
    }
}