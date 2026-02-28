using LibraryManagement.models;
using LibraryManagement.data;
using MySql.Data.MySqlClient;

namespace LibraryManagement.repositories
{
    public class BookCopyRepository : IBookCopyRepository
    {
        private readonly DbContext _context;

        public BookCopyRepository(DbContext context)
        {
            _context = context;
        }

        public List<BookCopy> GetAll()
        {
            var copies = new List<BookCopy>();
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"SELECT bc.*, b.title, b.isbn, b.year_publication, b.description 
                  FROM bookcopy bc 
                  JOIN book b ON bc.idBook = b.idBook", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
                copies.Add(MapBookCopy(reader));

            return copies;
        }

        public BookCopy? GetById(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"SELECT bc.*, b.title, b.isbn, b.year_publication, b.description 
                  FROM bookcopy bc 
                  JOIN book b ON bc.idBook = b.idBook
                  WHERE bc.idCopy = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
                return MapBookCopy(reader);

            return null;
        }

        public void Add(BookCopy bookCopy)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"INSERT INTO bookcopy (status, barcode, idBook) 
                  VALUES (@status, @barcode, @idBook)", connection);

            cmd.Parameters.AddWithValue("@status", bookCopy.Status);
            cmd.Parameters.AddWithValue("@barcode", bookCopy.Barcode);
            cmd.Parameters.AddWithValue("@idBook", bookCopy.Book.Id);

            cmd.ExecuteNonQuery();
        }

        public void Update(BookCopy bookCopy)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"UPDATE bookcopy SET 
                    status = @status, 
                    barcode = @barcode, 
                    idBook = @idBook 
                  WHERE idCopy = @id", connection);

            cmd.Parameters.AddWithValue("@id", bookCopy.Id);
            cmd.Parameters.AddWithValue("@status", bookCopy.Status);
            cmd.Parameters.AddWithValue("@barcode", bookCopy.Barcode);
            cmd.Parameters.AddWithValue("@idBook", bookCopy.Book.Id);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("DELETE FROM bookcopy WHERE idCopy = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private BookCopy MapBookCopy(MySqlDataReader reader)
        {
            int descIndex = reader.GetOrdinal("description");

            var book = new Book(
                reader.GetInt32("idBook"),
                reader.GetString("title"),
                reader.GetString("isbn"),
                int.Parse(reader.GetString("year_publication")),
                reader.IsDBNull(descIndex) ? null : reader.GetString("description")
            );

            return new BookCopy(
                reader.GetInt32("idCopy"),
                reader.GetString("barcode"),
                reader.GetString("status"),
                book
            );
        }
    }
}