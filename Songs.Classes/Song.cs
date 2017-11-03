using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Songs.Classes
{
    public class Song
    {
        [Key]
        public int SongId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "The Title has a minimum length of 1 and a maximum length of 50")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "The Title may only contain letters and numbers")]
        public string Title { get; set; }
        public TimeSpan Duration { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Releasedate is NOT a date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Albums")]
        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();

        [Display(Name = "Performers")]
        public virtual ICollection<Performer> Performers { get; set; } = new HashSet<Performer>();

        [ForeignKey("SongGenre")]
        public int? GenreId { get; set; }
        [DisplayName("Genre")]
        public virtual Genre SongGenre { get; set; }
    }
}
