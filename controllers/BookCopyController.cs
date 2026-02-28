using System.Text.Json;
using LibraryManagement.services;
using LibraryManagement.models;
using LibraryManagement.Routing;

namespace LibraryManagement.controllers
{
    /// <summary>
    /// Contrôleur responsable de la gestion des exemplaires de livres.
    /// Reçoit les requêtes HTTP, appelle le BookCopyService
    /// et retourne les réponses au format JSON.
    /// </summary>
    class BookCopyController
    {
        // Injection du service BookCopy pour accéder à la logique métier
        private readonly IBookCopyService _bookCopyService;

        public BookCopyController(IBookCopyService bookCopyService)
        {
            _bookCopyService = bookCopyService;
        }

        /// <summary>
        /// GET /api/bookcopies
        /// Retourne la liste complète des exemplaires au format JSON.
        /// </summary>
        [HttpGet("/api/bookcopies")]
        public string GetAll()
        {
            var copies = _bookCopyService.GetAll();
            return JsonSerializer.Serialize(copies);
        }

        /// <summary>
        /// GET /api/bookcopies/{id}
        /// Retourne un exemplaire spécifique par son identifiant.
        /// </summary>
        [HttpGet("/api/bookcopies/{id}")]
        public string GetById(int id)
        {
            var copy = _bookCopyService.GetById(id);
            return JsonSerializer.Serialize(copy);
        }

        /// <summary>
        /// POST /api/bookcopies
        /// Crée un nouvel exemplaire à partir des données reçues.
        /// </summary>
        [HttpPost("/api/bookcopies")]
        public string Create(BookCopy bookCopy)
        {
            _bookCopyService.Add(bookCopy);
            return JsonSerializer.Serialize(new { message = "Exemplaire créé avec succès." });
        }

        /// <summary>
        /// PUT /api/bookcopies/{id}
        /// Met à jour les informations d'un exemplaire existant.
        /// </summary>
        [HttpPut("/api/bookcopies/{id}")]
        public string Update(int id, BookCopy bookCopy)
        {
            bookCopy.Id = id;
            _bookCopyService.Update(bookCopy);
            return JsonSerializer.Serialize(new { message = "Exemplaire mis à jour avec succès.", id });
        }

        /// <summary>
        /// DELETE /api/bookcopies/{id}
        /// Supprime un exemplaire par son identifiant.
        /// </summary>
        [HttpDelete("/api/bookcopies/{id}")]
        public string Delete(int id)
        {
            _bookCopyService.Delete(id);
            return JsonSerializer.Serialize(new { message = "Exemplaire supprimé avec succès.", id });
        }
    }
}