using System.Collections.Generic;
using System.Web.Mvc;
using Songs.Classes;

namespace Songs.UI.Viewmodels
{
    public class EditCreateSong
    {
        public Song EditSong { get; set; }
        public MultiSelectList Performers { get; set; }
        public MultiSelectList Albums { get; set; }
        public List<int> SelectedPerformers { get; set; }
        public List<int> SelectedAlbums { get; set; }
    }
}