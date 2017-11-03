using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Songs.Classes;
using Songs.DataLayer.Contexts;
using Songs.DataLayer.Interfaces;

namespace Songs.DataLayer.Repositories
{
    public class SongRepository: ISongRepository
    {
        private readonly SongContext _context = new SongContext();
        public IQueryable<Song> AllSongs => _context.Songs.Include(s=>s.SongGenre);
        public IQueryable<Performer> AllPerformers => _context.Performers;
        public IQueryable<Album> AllAlbums => _context.Albums;
        public IQueryable<Genre> AllGenres => _context.Genres;
        public Song FindWithGenreAlbumPerformer(int id)
        {
            return _context.Songs.Include(s => s.Performers).Include(s => s.Albums).Include(s => s.SongGenre).First(s => s.SongId == id);
        }
        public Song Find(int id)
        {
            return _context.Songs.Find(id);
        }
        public void InsertOrUpdate(Song song)
        {
            if (song.SongId == default(int))
            {
                _context.Songs.Add(song);
            }
            else
            {
                _context.Entry(song).State = EntityState.Modified;
            }
        }
        public void Delete(int songId)
        {
            Song song = _context.Songs.Find(songId);
            if(song != null) _context.Songs.Remove(song);
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
