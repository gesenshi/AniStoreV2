using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AniStoreV2.Models
{
    public class Genre
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Жанр")]
        [Required]
        public string Name { get; set; }
        public ICollection<Game> Games { get; set; }
        public Genre()
        {
            Games = new List<Game>();
        }
    }
}