using System;
using System.Linq;
using Songs.Classes;

namespace Songs.DataLayer.Interfaces
{
    public interface IPerformerRepository: IDisposable
    {
        IQueryable<Performer> All { get; }
        Performer Find(int id);
        void InsertOrUpdate(Performer performer);
        void Delete(int performerId);
        void Save();
    }
}
