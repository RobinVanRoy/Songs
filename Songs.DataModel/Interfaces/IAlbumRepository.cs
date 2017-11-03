using System;
using System.Linq;
using Songs.Classes;

namespace Songs.DataLayer.Interfaces
{
    public interface IAlbumRepository:IDisposable
    {
        IQueryable<Album> All { get; }
        Album Find(int? id);
        void InsertOrUpdate(Album album);
        void Delete(int albumId);
        void Save();
    }
}
