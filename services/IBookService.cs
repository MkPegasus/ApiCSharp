using LibraryManagement.models;
namespace LibraryManagement.services
{
    /// <summary>
    ///definition de l'interface IBookService qui représente le contrat pour les opérations de gestion des livres dans l'application, elle définit les méthodes pour récupérer tous les livres,
    ///récupérer un livre spécifique par son id, ajouter un nouvel livre et mettre à jour les informations d'un livre existant. Cette interface est utilisée pour abstraire la logique métier de la gestion des livres et permettre une séparation claire entre la couche de service et la couche de données représentée par le UserRepository.
    /// </summary>
    interface IBookService
    {
        List<Book> GetAll();
        Book? GetById(int id);
        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}