using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Songs.Classes
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }

        [Required(ErrorMessage = "The Title field is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Title field has a minimum length of 3 and a maximum length of 50")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "The Title field may only contain letters and numbers")]
        public string Title { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Releasedate is NOT a date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();

        [ForeignKey("AlbumGenre")]
        public int? GenreId { get; set; }
        [DisplayName("Genre")]
        public Genre AlbumGenre { get; set; }
    }
}
