using LibraryManagement.models;
using LibraryManagement.data;
using MySql.Data.MySqlClient;

namespace LibraryManagement.repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DbContext _context;

        public AuthorRepository(DbContext context)
        {
            _context = context;
        }

        public List<Author> GetAll()
        {
            var authors = new List<Author>();
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("SELECT * FROM author", connection);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
                authors.Add(MapAuthor(reader));

            return authors;
        }

        public Author? GetById(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("SELECT * FROM author WHERE idAuthor = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();

            if (reader.Read())
                return MapAuthor(reader);

            return null;
        }

        public void Add(Author author)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"INSERT INTO author (name, bibliography) 
                  VALUES (@name, @bibliography)", connection);

            cmd.Parameters.AddWithValue("@name", author.Name);
            cmd.Parameters.AddWithValue("@bibliography", author.Biography ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Update(Author author)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand(
                @"UPDATE author SET 
                    name = @name, 
                    bibliography = @bibliography 
                  WHERE idAuthor = @id", connection);

            cmd.Parameters.AddWithValue("@id", author.Id);
            cmd.Parameters.AddWithValue("@name", author.Name);
            cmd.Parameters.AddWithValue("@bibliography", author.Biography ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = _context.CreerConnection();
            using var cmd = new MySqlCommand("DELETE FROM author WHERE idAuthor = @id", connection);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
        }

        private Author MapAuthor(MySqlDataReader reader)
        {
            int bioIndex = reader.GetOrdinal("bibliography");

            return new Author(
                reader.GetInt32("idAuthor"),
                reader.GetString("name"),
                reader.IsDBNull(bioIndex) ? null : reader.GetString("bibliography")
            )
            { Id = reader.GetInt32("idAuthor") };
        }
    }
}