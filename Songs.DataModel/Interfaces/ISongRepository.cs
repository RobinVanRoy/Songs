using System;
using System.Linq;
using System.Linq.Expressions;
using Songs.Classes;

namespace Songs.DataLayer.Interfaces
{
    public interface ISongRepository: IDisposable
    {
        IQueryable<Song> AllSongs { get; }
        IQueryable<Performer> AllPerformers { get; }
        IQueryable<Album> AllAlbums { get; }
        IQueryable<Genre> AllGenres { get; }
        Song Find(int id);
        Song FindWithGenreAlbumPerformer(int id);
        void InsertOrUpdate(Song song);
        void Delete(int songId);
        void Save();
    }
}
