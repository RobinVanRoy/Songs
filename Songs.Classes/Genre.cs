using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Songs.Classes
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "The Name field is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The Name field has a minimum length of 3 and a maximum length of 50")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "The Name field may only contain letters and numbers")]
        public string Name { get; set; }
        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();
        public virtual ICollection<Album> Albums { get; set; } = new HashSet<Album>();
    }
}
