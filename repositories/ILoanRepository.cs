using LibraryManagement.models;

namespace LibraryManagement.repositories
{
    public interface ILoanRepository
    {
        List<Loan> GetAll();
        Loan? GetById(int id);
        void Add(Loan loan);
        void Update(Loan loan);
        void Delete(int id);
    }
}