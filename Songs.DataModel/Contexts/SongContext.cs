using System.Data.Entity;
using Songs.Classes;

namespace Songs.DataLayer.Contexts
{
    public class SongContext : DbContext
    {
        public SongContext(): base("Songs"){}
        public DbSet<Album> Albums { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Performer> Performers { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().MapToStoredProcedures();
        }
    }
}
