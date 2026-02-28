using LibraryManagement.models;
using LibraryManagement.repositories;


namespace LibraryManagement.services
{
    /// <summary>
    /// definition de la classe UserServiceImpl qui implémente l'interface UserService,
    /// elle est responsable de fournir les implémentations concrètes des méthodes définies dans l'interface pour gérer les utilisateurs dans l'application. Cette classe utilise une instance de UserRepository pour accéder aux données des utilisateurs et effectuer les opérations nécessaires pour répondre aux demandes de la couche de présentation ou d'autres parties de l'application qui interagissent avec les services de gestion des utilisateurs.
    /// </summary>
    internal class UserService : IUserService
    {
        //injection de dépendance du UserRepository pour permettre à la classe d'accéder aux données des utilisateurs
        private readonly UserRepository userRepository;

        //definition du constructeur de la classe UserServiceImpl qui prend en paramètre une instance de UserRepository
        //et l'affecte à la variable d'instance userRepository pour une utilisation ultérieure dans les méthodes de la classe
        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        //implementation de la méthode GetAllUsers qui utilise le UserRepository pour récupérer et retourner une liste de tous les utilisateurs disponibles dans la base de données ou la source de données configurée
        public List<User> GetAll()
        {
            return userRepository.GetAllUsers();
        }
        //implementation de la méthode GetUserById qui prend un identifiant d'utilisateur en paramètre, utilise le UserRepository pour récupérer et retourner les détails de l'utilisateur correspondant à cet identifiant. Si l'utilisateur n'existe pas, la méthode peut retourner null ou gérer l'exception selon la logique de l'application.
        public User? GetById(int id)
        {
            return userRepository.GetUserById(id);
        }

        //implementation de la méthode AddUser qui prend un objet User en paramètre et utilise le UserRepository pour ajouter ce nouvel utilisateur à la base de données ou à la source de données configurée. Cette méthode peut également inclure des validations ou des vérifications avant d'ajouter l'utilisateur, selon les besoins de l'application.
        public void Add(User user)
        {
            userRepository.AddUser(user);
        }

        public void Update(User user)
        {
            userRepository.UpdateUser(user);
        }

        public void Delete(int id)
        {
            userRepository.DeleteUser(id);
        }
    }
}