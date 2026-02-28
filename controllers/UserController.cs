using System.Text.Json;
using LibraryManagement.services;
using LibraryManagement.models;
using LibraryManagement.Routing;

namespace LibraryManagement.controllers
{
    /// <summary>
    /// Contrôleur responsable de la gestion des utilisateurs.
    /// Reçoit les requêtes HTTP, appelle le UsesrService
    /// et retourne les réponses au format JSON.
    /// </summary>
    internal class UserController
    {
        // Injection du service User pour accéder à la logique métier
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// GET /api/users
        /// Retourne la liste complète des emprunts au format JSON.
        /// </summary>
        [HttpGet("/api/users")]
        public string GetAll()
        {
            var users = _userService.GetAll();
            return JsonSerializer.Serialize(users);
        }

        /// <summary>
        /// GET /api/users/{id}
        /// Retourne un utilisateur spécifique par son identifiant.
        /// </summary>
        [HttpGet("/api/users/{id}")]
        public string GetById(int id)
        {
            var user = _userService.GetById(id);
            return JsonSerializer.Serialize(user);
        }

        /// <summary>
        /// POST /api/users
        /// Crée un nouvel utilisateur.
        /// </summary>
        [HttpPost("/api/users")]
        public string Create(User user)
        {
            _userService.Add(user);
            return JsonSerializer.Serialize(new { message = "Utilisateur créé avec succès." });
        }
        /// <summary>
        /// PUT /api/users/{id}
        /// Met à jour les informations d'un utilisateur existant.
        /// </summary>
        [HttpPut("/api/users/{id}")]
        public string Update(int id, User user)
        {
            user.Id = id;
            _userService.Update(user);
            return JsonSerializer.Serialize(new { message = "Utilisateur mis à jour avec succès.", id });
        }

        /// <summary>
        /// DELETE /api/users/{id}
        /// Supprime un emprunt par son identifiant.
        /// </summary>
        [HttpDelete("/api/users/{id}")]
        public string Delete(int id)
        {
            _userService.Delete(id);
            return JsonSerializer.Serialize(new { message = "Utilisateur supprimé avec succès.", id });
        }
    }
}