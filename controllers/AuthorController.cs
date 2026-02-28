using System.Text.Json;
using LibraryManagement.services;
using LibraryManagement.models;
using LibraryManagement.routing;

namespace LibraryManagement.controllers
{
    /// <summary>
    /// Contrôleur responsable de la gestion des auteurs.
    /// Reçoit les requêtes HTTP, appelle le AuthorService
    /// et retourne les réponses au format JSON.
    /// </summary>
    internal class AuthorController
    {
        // Injection du service Author pour accéder à la logique métier
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// GET /api/authors
        /// Retourne la liste complète des auteurs au format JSON.
        /// </summary>
        [HttpGet("/api/authors")]
        public string GetAll()
        {
            var authors = _authorService.GetAll();
            return JsonSerializer.Serialize(authors);
        }

        /// <summary>
        /// GET /api/authors/{id}
        /// Retourne un auteur spécifique par son identifiant.
        /// </summary>
        [HttpGet("/api/authors/{id}")]
        public string GetById(int id)
        {
            var author = _authorService.GetById(id);
            return JsonSerializer.Serialize(author);
        }

        /// <summary>
        /// POST /api/authors
        /// Crée un nouvel auteur à partir des données reçues.
        /// </summary>
        [HttpPost("/api/authors")]
        public string Create(Author author)
        {
            _authorService.Add(author);
            return JsonSerializer.Serialize(new { message = "Auteur créé avec succès." });
        }

        /// <summary>
        /// PUT /api/authors/{id}
        /// Met à jour les informations d'un auteur existant.
        /// </summary>
        [HttpPut("/api/authors/{id}")]
        public string Update(int id, Author author)
        {
            author.Id = id;
            _authorService.Update(author);
            return JsonSerializer.Serialize(new { message = "Auteur mis à jour avec succès.", id });
        }

        /// <summary>
        /// DELETE /api/authors/{id}
        /// Supprime un auteur par son identifiant.
        /// </summary>
        [HttpDelete("/api/authors/{id}")]
        public string Delete(int id)
        {
            _authorService.Delete(id);
            return JsonSerializer.Serialize(new { message = "Auteur supprimé avec succès.", id });
        }
    }
}