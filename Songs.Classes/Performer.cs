using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Songs.Classes
{
    public class Performer
    {
        [Key]
        public int PerformerId { get; set; }

        [DisplayName("First name")]
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name has a minimum length of 2 and a maximum length of 50")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "First name may only contain letters")]
        public string FirstName { get; set; }

        [DisplayName("Surname")]
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(50, ErrorMessage = "Surname has a minimum length of 2 and a maximum length of 50")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Surname may only contain letters")]
        public string LastName { get; set; }

        [DisplayName("Alias")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Alias has a minimum length of 1 and a maximum length of 50")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "Alias may only contain letters and numbers")]
        public string NickName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Birthday { get; set; }

        [DisplayName("Birthplace")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Birthplace has a minimum length of 3 and a maximum length of 50")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Birthplace may only contain letters")]
        public string BirthPlace { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Country has a minimum length of 3 and a maximum length of 50")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "First name may only contain letters")]
        public string Country { get; set; }
        public virtual ICollection<Song> Songs { get; set; } = new HashSet<Song>();

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}
