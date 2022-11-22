using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AniStoreV2.Models
{
    public class Game
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        [Display(Name = "Название")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Автор")]
        [Required]
        public string Author { get; set; }
        [Display(Name = "Описание")]
        [Required]
        public string Description { get; set; }
        [Display(Name = "Цена")]
        [Required]
        public int Price { get; set; }
        [Display(Name = "Рейтинг")]
        public int Rating { get; set; }
        [Display(Name = "Фото")]
        [Required]
        public string Photo { get; set; }
        [Display(Name = "Жанр")]
        [Required]
        public int? GenreID { get; set; }
        public Genre Genre { get; set; }
    }
}