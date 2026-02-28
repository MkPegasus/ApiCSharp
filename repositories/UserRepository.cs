using LibraryManagement.data;
using LibraryManagement.models;
using MySql.Data.MySqlClient;

namespace LibraryManagement.repositories
{
    /// <summary>
    /// definition de la classe UserRepository qui implémente l'interface UserRepository,
    /// elle est responsable de fournir les implémentations concrètes des méthodes définies 
    /// dans l'interface pour gérer les utilisateurs dans l'application.
    /// </summary>
     class UserRepository : IUserRepository
    {
        //injection de dépendance du contexte de données pour permettre à la classe d'accéder à la base de données
        private readonly DbContext dbContext;

        //definition du constructeur de la classe UserRepositoryImpl qui prend en paramètre une instance de DbContext et l'affecte à la variable d'instance dbContext pour une utilisation ultérieure dans les méthodes de la classe
        public UserRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //implémentation de la méthode GetAllUsers qui retourne une liste de tous les utilisateurs présents dans la base de données en utilisant le contexte de données pour accéder à la table des utilisateurs et récupérer les données.
        public List<User> GetAllUsers()
        {

            //initialisation d'une liste d'utilisateurs vide pour stocker les résultats de la requête à la base de données.
            var users = new List<User>();

            //utilisation de la méthode CreerConnection du contexte de données pour créer une connexion à la base de données,

            using var conn = dbContext.CreerConnection();
            //puis ouverture de la connexion,
            conn.Open();
            //exécution d'une commande SQL pour sélectionner tous les utilisateurs,
            var command = new MySqlCommand("SELECT * FROM users", conn);
            //et lecture des résultats pour remplir la liste des utilisateurs avant de la retourner.
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(new User
                (
                    reader.GetInt32("id"),
                    reader.GetString("name"),
                    reader.GetString("email"),
                    reader.GetString("password_hash"),
                    reader.GetString("role"),
                    reader.GetString("status")
                ));
            }
            return users;

        }

        //implémentation de la méthode GetUserById qui prend un identifiant d'utilisateur en paramètre et retourne l'utilisateur correspondant à cet identifiant en utilisant le contexte de données pour exécuter une commande SQL de sélection avec une clause WHERE pour filtrer les résultats.
        public User? GetUserById(int id)
        {
            using var conn = dbContext.CreerConnection();
            conn.Open();
            var command = new MySqlCommand("SELECT * FROM users WHERE id = @id", conn);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                return new User
                (
                    reader.GetInt32("id"),
                    reader.GetString("name"),
                    reader.GetString("email"),
                    reader.GetString("password_hash"),
                    reader.GetString("role"),
                    reader.GetString("status")
                );
            }
            return null;
        }

        //implémentation de la méthode AddUser qui prend un objet User en paramètre et ajoute cet utilisateur à la base de données en utilisant le contexte de données pour exécuter une commande SQL d'insertion.
        public void AddUser(User user)
        {
            using var conn = dbContext.CreerConnection();
            conn.Open();
            var command = new MySqlCommand("INSERT INTO users(name, email, password_hash, role, status) VALUES (@name, @email, @password, @role, @status)", conn);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.PasswordHash);
            command.Parameters.AddWithValue("@role", user.Role);
            command.Parameters.AddWithValue("@status", user.Status);
            command.ExecuteNonQuery();
        }

        //implémentation de la méthode UpdateUser qui prend un objet User en paramètre et met à jour les informations de cet utilisateur dans la base de données en utilisant le contexte de données pour exécuter une commande SQL de mise à jour.
        public void UpdateUser(User user)
        {
            using var conn = dbContext.CreerConnection();
            conn.Open();
            var command = new MySqlCommand("UPDATE users SET name = @name, email = @email, password_hash = @password, role = @role, status = @status WHERE id = @id", conn);
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@password", user.PasswordHash);
            command.Parameters.AddWithValue("@role", user.Role);
            command.Parameters.AddWithValue("@status", user.Status);
            command.Parameters.AddWithValue("@id", user.Id);
            command.ExecuteNonQuery();
        }

        //implémentation de la méthode DeleteUser qui prend un identifiant d'utilisateur en paramètre et supprime cet utilisateur de la base de données en utilisant le contexte de données pour exécuter une commande SQL de suppression avec une clause WHERE pour filtrer les résultats.
        public void DeleteUser(int id)
        {
            using var conn = dbContext.CreerConnection();
            conn.Open();
            var command = new MySqlCommand("DELETE FROM users WHERE id = @id", conn);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

    }
}