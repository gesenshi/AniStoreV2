using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace AniStoreV2.Models
{
    public class RegisterModel
    {
        [Display(Name = "Почта")]
        public string Email { get; set; }
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}