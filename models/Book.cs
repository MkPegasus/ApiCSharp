namespace LibraryManagement.models
{
    /// <summary>
    /// Définition de la class Book
    /// </summary>
    public class Book
    {
        /* Définition des attribut ainsi que leur getter et setter */
        public int Id{get; set;}
        public string Title{get; set;}
        public string Isbn{get; set;}
        public int Year{get; set;}
        public string? Description{get; set;}

        /* Liste des auteurs */
        public List<Author> Authors{get; set;}

        /* Liste des exemplaires */
        public List<BookCopy> BookCopies{get; set;}

        /* Définition du constructeur */
        public Book(string title, string isbn, int year, string description)
        {
            Title = title;
            Isbn = isbn;
            Year = year;
            Description = description;
            Authors = new List<Author>();
            BookCopies = new List<BookCopy>();

        }
    }
}