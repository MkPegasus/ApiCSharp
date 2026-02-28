using LibraryManagement.models;

namespace LibraryManagement.services
{
    public interface ILoanService
    {
        List<Loan> GetAll();
        Loan? GetById(int id);
        void Add(Loan loan);
        /* Retourner un livre lorsqu'il a été emprunté */
        void Return(int loanId);
        void Delete(int id);
    }
}