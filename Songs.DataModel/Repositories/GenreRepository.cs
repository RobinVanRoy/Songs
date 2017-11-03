using System.Data.SqlClient;
using System.Linq;
using Songs.Classes;
using Songs.DataLayer.Contexts;
using Songs.DataLayer.Interfaces;

namespace Songs.DataLayer.Repositories
{
    public class GenreRepository: IGenreRepository
    {
        private readonly SongContext _context = new SongContext();
        public IQueryable<Genre> All => _context.Genres;
        public Genre Find(int id)
        {
            var sqlQuery = "SELECT * FROM dbo.Genres WHERE GenreId=@GenreId";
            SqlParameter parGenreId = new SqlParameter("@GenreId", id);
            var result = _context.Genres.SqlQuery(sqlQuery, parGenreId).SingleOrDefault();
            return result;
        }
        public void InsertOrUpdate(Genre genre)
        {
            if (genre.GenreId == default(int))
            {
                var sqlQuery = "INSERT INTO dbo.Genres (Name) VALUES(@Name)";
                SqlParameter parName = new SqlParameter("@Name", genre.Name);
                _context.Database.ExecuteSqlCommand(sqlQuery, parName);
            }
            else
            {
                var sqlQuery = "UPDATE dbo.Genres SET Name=@Name WHERE GenreId=@GenreId";
                SqlParameter parName = new SqlParameter("@Name", genre.Name);
                SqlParameter parGenreId = new SqlParameter("@GenreId", genre.GenreId);
                _context.Database.ExecuteSqlCommand(sqlQuery,parName,parGenreId);
            }
        }
        public void Delete(int genreId)
        {
            var sqlQuery = "DELETE FROM dbo.Genres WHERE GenreId=@GenreId";
            SqlParameter parGenreId = new SqlParameter("@GenreId", genreId);
            _context.Database.ExecuteSqlCommand(sqlQuery, parGenreId);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
