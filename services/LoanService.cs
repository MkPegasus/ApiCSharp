using LibraryManagement.models;
using LibraryManagement.repositories;

namespace LibraryManagement.services
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IBookCopyRepository _copyRepository;

        public LoanService(ILoanRepository loanRepository, IBookCopyRepository copyRepository)
        {
            _loanRepository = loanRepository;
            _copyRepository = copyRepository;
        }

        public List<Loan> GetAll()
        {
            return _loanRepository.GetAll();
        }

        public Loan? GetById(int id)
        {
            Loan? loan = _loanRepository.GetById(id);

            if (loan == null)
                throw new Exception($"Emprunt avec l'id {id} introuvable.");

            return loan;
        }

        public void Add(Loan loan)
        {
            // Vérifier que l'exemplaire existe et est disponible
            BookCopy? copy = _copyRepository.GetById(loan.CopyId);
            if (copy == null)
                throw new Exception("L'exemplaire n'existe pas.");

            if (copy.Status != "AVAILABLE")
                throw new Exception("L'exemplaire n'est pas disponible.");

            if (loan.DueDate <= loan.CheckoutDate)
                throw new Exception("La date de retour doit être après la date d'emprunt.");

            // Mettre à jour le statut de l'exemplaire
            copy.Status = "LOANED";
            _copyRepository.Update(copy);

            _loanRepository.Add(loan);
        }

        public void Return(int loanId)
        {
            Loan? loan = _loanRepository.GetById(loanId);
            if (loan == null)
                throw new Exception($"Emprunt avec l'id {loanId} introuvable.");

            if (loan.ReturnDate != null)
                throw new Exception("Cet emprunt a déjà été retourné.");

            // Marquer la date de retour
            loan.ReturnDate = DateTime.Now;
            _loanRepository.Update(loan);

            // Remettre l'exemplaire disponible
            BookCopy? copy = _copyRepository.GetById(loan.CopyId);
            if (copy != null)
            {
                copy.Status = "AVAILABLE";
                _copyRepository.Update(copy);
            }
        }

        public void Delete(int id)
        {
            Loan? loan = _loanRepository.GetById(id);
            if (loan == null)
                throw new Exception($"Emprunt avec l'id {id} introuvable.");

            _loanRepository.Delete(id);
        }
    }
}