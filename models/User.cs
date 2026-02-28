namespace LibraryManagement.models
{
    /// <summary>
    /// Définition de la classe User
    /// </summary>
    public class User
    {

        /* Définition des attributs */
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; }

        public string Status { get; set; }

        public List<Loan> Loans { get; set; } = null!;

        /* Définition du constructeur personnalisé */
        public User(int id, string name, string email, string passwordHash, string role, string status)
        {
            Id = id;
            Name = name;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            Status = status;
            Loans = new List<Loan>();
        }    

    }
}