using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ComicBooks.Utils;

namespace ComicBooks.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(56, MinimumLength = 3)]
        //custom validation
        [ValidGenreName(allowedLetters: "aoe", 
            ErrorMessage = "GANRE NAME must contain at least one of the following letters [a,o,e] :)")]
        public string Name { get; set; }
    }
}
