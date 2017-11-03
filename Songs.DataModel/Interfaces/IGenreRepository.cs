using System;
using System.Linq;
using Songs.Classes;

namespace Songs.DataLayer.Interfaces
{
    public interface IGenreRepository: IDisposable
    {
        IQueryable<Genre> All { get; }
        Genre Find(int id);
        void InsertOrUpdate(Genre genre);
        void Delete(int genreId);
        void Save();
    }
}
