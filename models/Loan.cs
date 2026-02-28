namespace LibraryManagement.models
{
    /// <summary>
    /// Définition de la classe loan
    /// </summary>
public class Loan
{
    /* Définition des attributs */
    public int Id { get; set; }

    public int UserId { get; set; }

    public int CopyId { get; set; }

    public DateTime CheckoutDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public User User { get; set; } = null!;

    public BookCopy Copy { get; set; } = null!;

    /* Définition du constructeur personnalisé */
    public Loan(int userId, int copyId, DateTime checkedOutDate, DateTime dueDate)
        {
            UserId = userId;
            CopyId = copyId;
            CheckoutDate = checkedOutDate;
            DueDate = dueDate;
           
           
        }
}
}