using System.Collections.Generic;
using Songs.Classes;

namespace Songs.UI.Viewmodels
{
    public class AllViewmodel
    {
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<Performer> Performers { get; set; }
    }
}