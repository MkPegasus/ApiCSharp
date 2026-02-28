namespace LibraryManagement.models
{
    /// <summary>
    /// Définition de la class bookcopy
    /// </summary>
    public class BookCopy
    {
        /* Définition de attributs ainsi que leurs getters et settes*/
       public int Id { get; set; }
        public string Barcode { get; set; } = null!;

        public string Status { get; set; }

        public Book Book { get; set; }

        /* Définition du constructeur avec paramètres */

        public BookCopy(int id, string barcode, string status, Book book)
        {
            Id = id;
            Barcode = barcode;
            Status = status;
            Book = book;
        }
    }
}