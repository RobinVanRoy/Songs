using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using Songs.Classes;
using Songs.DataLayer.Contexts;
using Songs.DataLayer.Interfaces;

namespace Songs.DataLayer.Repositories
{
    public class AlbumRepository: IAlbumRepository
    {
        private readonly SongContext _context = new SongContext();
        public IQueryable<Album> All => _context.Albums.Include(a=>a.AlbumGenre);
        public Album Find(int? id)
        {
            return _context.Albums.Include(a => a.AlbumGenre).First(a => a.AlbumId == id);
        }
        public void InsertOrUpdate(Album album)
        {
            if (album.AlbumId == default(int))
            {
                SqlParameter parTitle = new SqlParameter("@Title", album.Title);
                SqlParameter parReleaseDate = new SqlParameter("@ReleaseDate", album.ReleaseDate);
                SqlParameter parGenreId = new SqlParameter("@GenreId", album .GenreId);
                _context.Database.ExecuteSqlCommand("dbo.Album_Insert @Title, @ReleaseDate, @GenreId", parTitle, parReleaseDate,
                    parGenreId);
            }
            else
            {
                SqlParameter parAlbumId = new SqlParameter("@AlbumId", album.AlbumId);
                SqlParameter parTitle = new SqlParameter("@Title", album.Title);
                SqlParameter parReleaseDate = new SqlParameter("@ReleaseDate", album.ReleaseDate);
                SqlParameter parGenreId = new SqlParameter("@GenreId", album.GenreId);
                _context.Database.ExecuteSqlCommand("dbo.Album_Update @AlbumId, @Title, @ReleaseDate, @GenreId", parAlbumId, parTitle, parReleaseDate,
                    parGenreId);
            }
        }
        public void Delete(int albumId)
        {
            var sqlpar = new SqlParameter("@AlbumId", albumId);
            _context.Database.ExecuteSqlCommand("dbo.Album_Delete @AlbumId", sqlpar);
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
