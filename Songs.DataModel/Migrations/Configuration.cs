using System.Data.Entity;
using System.Linq;
using Songs.DataLayer.Contexts;

namespace Songs.DataLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using Classes;

    internal sealed class Configuration : DbMigrationsConfiguration<SongContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            //Database.SetInitializer(new DropCreateDatabaseAlways<SongContext>());
        }
        
        protected override void Seed(SongContext context)
        {
            Genre genre1 = new Genre
            {
                Name = "Rock"
            };
            context.Genres.AddOrUpdate(genre1);

            Performer performer1 = new Performer
            {
                FirstName = "Ikke",
                LastName = "Testing",
                NickName = "Ananas",
                Birthday = new DateTime(1980, 10, 19),
                BirthPlace = "Antwerpen",
                Country = "Belgie"
            };
            context.Performers.AddOrUpdate(performer1);

            Album album1 = new Album
            {
                Title = "Testalbum",
                ReleaseDate = new DateTime(2000, 11, 20)
            };
            context.Albums.AddOrUpdate(album1);

            Song song1 = new Song
            {
                Title = "Baracuda",
                ReleaseDate = new DateTime(1999, 05, 05),
                Duration = new TimeSpan(0, 2, 30)
            };
            context.Songs.AddOrUpdate(song1);

            context.SaveChanges();

            Song song = context.Songs.FirstOrDefault();
            Genre genre = context.Genres.FirstOrDefault();
            Album album = context.Albums.FirstOrDefault();
            Performer performer = context.Performers.FirstOrDefault();

            //System.Diagnostics.Debug.WriteLine(song.Title);

            if (song != null)
            {
                song.Performers.Add(performer);
                song.Albums.Add(album);
                if (genre != null)
                {
                    song.GenreId = genre.GenreId;
                    song.SongGenre = genre;
                }
            }

            if (album != null && genre != null)
            {
                album.GenreId = genre.GenreId;
                album.AlbumGenre = genre;
            }

            context.SaveChanges();
            
        }
        
    }
}
