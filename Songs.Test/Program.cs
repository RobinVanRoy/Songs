using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Songs.Classes;
using Songs.DataLayer;
using Songs.DataLayer.Contexts;

namespace Songs.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            //var ini = new SongInitializer();
            
            using (var db = new SongContext())
            {
                Database.SetInitializer(new DropCreateDatabaseAlways<SongContext>());
                Console.WriteLine("aantal songs: " + db.Songs.Count());
                Console.Read();
            }
            //var db = new SongContext();
            //ct.Database.Delete();
            //ct.Database.Create();
            

            /*
            using (var context = new SongContext())
            {
                Song song = context.Songs.FirstOrDefault();
                Console.WriteLine("Songtitle: " + song.Title);
                Console.WriteLine("Song album: " + song.Albums.First().Title);
                Console.WriteLine("Song genreId: " + song.GenreId);
                Console.WriteLine("Song genre: " + song.SongGenre.Name);
                Console.WriteLine("song performer: " + song.Performers.First().FirstName);

                Console.Read();
            }
            */
        }
    }
}
