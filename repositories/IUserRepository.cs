using LibraryManagement.models;
namespace LibraryManagement.repositories
{
     /// <summary>
    /// definition de l'interface UserRepository qui représente le contrat pour les opérations de gestion des utilisateurs dans l'application.
    /// </summary>
     interface IUserRepository
    {
        //definition de la méthode pour récupérer tous les utilisateurs de la base de données, elle retourne une liste d'objets User
        List<User> GetAllUsers();
        //definition de la méthode pour récupérer un utilisateur spécifique en fonction de son id, elle retourne un objet User
        User? GetUserById(int id);
        //definition de la méthode pour ajouter un nouvel utilisateur à la base de données, elle prend en paramètre un objet User et ne retourne rien
        void AddUser(User user);
        //definition de la méthode pour mettre à jour les informations d'un utilisateur existant dans la base de données, elle prend en paramètre un objet User et ne retourne rien
        void UpdateUser(User user);
        //definition de la méthode pour supprimer un utilisateur de la base de données en fonction de son id, elle prend en paramètre l'id de l'utilisateur à supprimer et ne retourne rien
        void DeleteUser(int id);
    }
}