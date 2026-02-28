using LibraryManagement.models;
using LibraryManagement.data;
using MySql.Data.MySqlClient;

namespace LibraryManagement.repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly DbContext _context;

        public LoanRepository(DbContext context)
        {
            _context = context;
        }

        public List<Loan> GetAll()
        {
            var loans = new List<Loan>();
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("SELECT * FROM loan", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
                loans.Add(MapLoan(reader));

            return loans;
        }

        public Loan? GetById(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("SELECT * FROM loan WHERE id = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
                return MapLoan(reader);

            return null;
        }

        public void Add(Loan loan)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"INSERT INTO loan (user_id, idCopy, checkout_date, due_date) 
                  VALUES (@userId, @copyId, @checkoutDate, @dueDate)", connection);

            cmd.Parameters.AddWithValue("@userId", loan.UserId);
            cmd.Parameters.AddWithValue("@copyId", loan.CopyId);
            cmd.Parameters.AddWithValue("@checkoutDate", loan.CheckoutDate);
            cmd.Parameters.AddWithValue("@dueDate", loan.DueDate);

            cmd.ExecuteNonQuery();
        }

        public void Update(Loan loan)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"UPDATE loan SET 
                    user_id = @userId, 
                    idCopy = @copyId, 
                    checkout_date = @checkoutDate, 
                    due_date = @dueDate, 
                    return_date = @returnDate 
                  WHERE id = @id", connection);

            cmd.Parameters.AddWithValue("@id", loan.Id);
            cmd.Parameters.AddWithValue("@userId", loan.UserId);
            cmd.Parameters.AddWithValue("@copyId", loan.CopyId);
            cmd.Parameters.AddWithValue("@checkoutDate", loan.CheckoutDate);
            cmd.Parameters.AddWithValue("@dueDate", loan.DueDate);
            cmd.Parameters.AddWithValue("@returnDate", loan.ReturnDate ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("DELETE FROM loan WHERE id = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private Loan MapLoan(MySqlDataReader reader)
        {
            int returnDateIndex = reader.GetOrdinal("return_date");

            var loan = new Loan(
                reader.GetInt32("id"),
                reader.GetInt32("user_id"),
                reader.GetInt32("idCopy"),
                reader.GetDateTime("checkout_date"),
                reader.GetDateTime("due_date")
            );

            loan.ReturnDate = reader.IsDBNull(returnDateIndex) 
                ? null 
                : reader.GetDateTime("return_date");

            return loan;
        }
    }
}