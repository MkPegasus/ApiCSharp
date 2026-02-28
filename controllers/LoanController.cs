using System.Text.Json;
using LibraryManagement.services;
using LibraryManagement.models;
using LibraryManagement.Routing;

namespace LibraryManagement.controllers
{
    /// <summary>
    /// Contrôleur responsable de la gestion des emprunts.
    /// Reçoit les requêtes HTTP, appelle le LoanService
    /// et retourne les réponses au format JSON.
    /// Gère également le retour des livres empruntés via un endpoint dédié.
    /// </summary>
    internal class LoanController
    {
        // Injection du service Loan pour accéder à la logique métier
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        /// <summary>
        /// GET /api/loans
        /// Retourne la liste complète des emprunts au format JSON.
        /// </summary>
        [HttpGet("/api/loans")]
        public string GetAll()
        {
            var loans = _loanService.GetAll();
            return JsonSerializer.Serialize(loans);
        }

        /// <summary>
        /// GET /api/loans/{id}
        /// Retourne un emprunt spécifique par son identifiant.
        /// </summary>
        [HttpGet("/api/loans/{id}")]
        public string GetById(int id)
        {
            var loan = _loanService.GetById(id);
            return JsonSerializer.Serialize(loan);
        }

        /// <summary>
        /// POST /api/loans
        /// Crée un nouvel emprunt.
        /// La date d'emprunt est automatiquement définie à la date actuelle.
        /// </summary>
        [HttpPost("/api/loans")]
        public string Create(Loan loan)
        {
            // La date d'emprunt est définie au moment de la création
            loan.CheckoutDate = DateTime.Now;
            _loanService.Add(loan);
            return JsonSerializer.Serialize(new { message = "Emprunt créé avec succès." });
        }

        /// <summary>
        /// PUT /api/loans/{id}/return
        /// Enregistre le retour d'un livre emprunté.
        /// Met à jour la date de retour et remet l'exemplaire en statut AVAILABLE.
        /// </summary>
        [HttpPut("/api/loans/{id}/return")]
        public string Return(int id)
        {
            _loanService.Return(id);
            return JsonSerializer.Serialize(new { message = "Retour enregistré avec succès.", id });
        }

        /// <summary>
        /// DELETE /api/loans/{id}
        /// Supprime un emprunt par son identifiant.
        /// </summary>
        [HttpDelete("/api/loans/{id}")]
        public string Delete(int id)
        {
            _loanService.Delete(id);
            return JsonSerializer.Serialize(new { message = "Emprunt supprimé avec succès.", id });
        }
    }
}