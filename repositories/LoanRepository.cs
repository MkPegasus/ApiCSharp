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
            using var cmd = new MySqlCommand("SELECT * FROM loans", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
                loans.Add(MapLoan(reader));

            return loans;
        }

        public Loan? GetById(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("SELECT * FROM loans WHERE id = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
                return MapLoan(reader);

            return null;
        }

        public void Add(Loan loans)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"INSERT INTO loans (user_id, idCopy, checkout_date, due_date) 
                  VALUES (@userId, @copyId, @checkoutDate, @dueDate)", connection);

            cmd.Parameters.AddWithValue("@userId", loans.UserId);
            cmd.Parameters.AddWithValue("@copyId", loans.CopyId);
            cmd.Parameters.AddWithValue("@checkoutDate", loans.CheckoutDate);
            cmd.Parameters.AddWithValue("@dueDate", loans.DueDate);

            cmd.ExecuteNonQuery();
        }

        public void Update(Loan loans)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"UPDATE loans SET 
                    user_id = @userId, 
                    idCopy = @copyId, 
                    checkout_date = @checkoutDate, 
                    due_date = @dueDate, 
                    return_date = @returnDate 
                  WHERE id = @id", connection);

            cmd.Parameters.AddWithValue("@id", loans.Id);
            cmd.Parameters.AddWithValue("@userId", loans.UserId);
            cmd.Parameters.AddWithValue("@copyId", loans.CopyId);
            cmd.Parameters.AddWithValue("@checkoutDate", loans.CheckoutDate);
            cmd.Parameters.AddWithValue("@dueDate", loans.DueDate);
            cmd.Parameters.AddWithValue("@returnDate", loans.ReturnDate ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("DELETE FROM loans WHERE id = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private Loan MapLoan(MySqlDataReader reader)
        {
            int returnDateIndex = reader.GetOrdinal("return_date");

            var loans = new Loan(
                reader.GetInt32("id"),
                reader.GetInt32("user_id"),
                reader.GetInt32("idCopy"),
                reader.GetDateTime("checkout_date"),
                reader.GetDateTime("due_date")
            );

            loans.ReturnDate = reader.IsDBNull(returnDateIndex) 
                ? null 
                : reader.GetDateTime("return_date");

            return loans;
        }
    }
}