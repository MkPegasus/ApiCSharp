namespace LibraryManagement.models
{
    /// <summary>
    /// Définition de la classe Author
    /// </summary>
    public class Author
    {
        /* Définition des attributs */
        public int Id{get; set;}
        public string Name{get; set;}
        public string? Biography{get; set;}

        /* Définition du constructeur personnalisé */
        public Author(string name, string? biography )
        {
            Name = name;
            Biography = biography;
        }

    }
}