using LibraryManagement.models;
using LibraryManagement.data;
using MySql.Data.MySqlClient;

namespace LibraryManagement.repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DbContext _context;

        public BookRepository(DbContext context)
        {
            _context = context;
        }

        public List<Book> GetAll()
        {
            var books = new List<Book>();
            using var connection = _context.CreerConnection();
            
            using var cmd = new MySqlCommand("SELECT * FROM book", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                books.Add(MapBook(reader));
            }
            return books;
        }

        public Book? GetById(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("SELECT * FROM book WHERE idBook = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
                return MapBook(reader);

            return null;
        }

        public void Add(Book book)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"INSERT INTO book (title, isbn, year_publication, description) 
                  VALUES (@title, @isbn, @year, @description)", connection);

            cmd.Parameters.AddWithValue("@title", book.Title);
            cmd.Parameters.AddWithValue("@isbn", book.Isbn);
            cmd.Parameters.AddWithValue("@year", book.Year.ToString());
            cmd.Parameters.AddWithValue("@description", book.Description ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Update(Book book)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"UPDATE book SET 
                    title = @title, 
                    isbn = @isbn, 
                    year_publication = @year, 
                    description = @description 
                  WHERE idBook = @id", connection);

            cmd.Parameters.AddWithValue("@id", book.Id);
            cmd.Parameters.AddWithValue("@title", book.Title);
            cmd.Parameters.AddWithValue("@isbn", book.Isbn);
            cmd.Parameters.AddWithValue("@year", book.Year.ToString());
            cmd.Parameters.AddWithValue("@description", book.Description ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("DELETE FROM book WHERE idBook = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        // Méthode privée pour mapper le reader vers un objet Book
        private Book MapBook(MySqlDataReader reader)
        {
            return new Book(
                reader.GetInt32("idBook"),
                reader.GetString("title"),
                reader.GetString("isbn"),
                reader.GetInt32("year_publication"), 
                //Vérifie si la description est null en db et faire operation nécessaire
                reader.IsDBNull(reader.GetOrdinal("description"))
                    ? null 
                    : reader.GetString("description")
            );
        }
    }
}