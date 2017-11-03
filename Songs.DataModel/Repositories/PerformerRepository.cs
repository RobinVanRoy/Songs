using System.Data.Entity;
using System.Linq;
using Songs.Classes;
using Songs.DataLayer.Contexts;
using Songs.DataLayer.Interfaces;

namespace Songs.DataLayer.Repositories
{
    public class PerformerRepository: IPerformerRepository
    {
        private readonly SongContext _context = new SongContext();
        public IQueryable<Performer> All => _context.Performers;
        public Performer Find(int id)
        {
            return _context.Performers.Find(id);
        }
        public void InsertOrUpdate(Performer performer)
        {
            if (performer.PerformerId == default(int))
            {
                _context.Performers.Add(performer);
            }
            else
            {
                _context.Entry(performer).State = EntityState.Modified;
            }
        }
        public void Delete(int performerId)
        {
            Performer performer = _context.Performers.Find(performerId);
            if (performer != null) _context.Performers.Remove(performer);
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
