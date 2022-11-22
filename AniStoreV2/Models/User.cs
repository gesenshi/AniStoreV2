using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AniStoreV2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SteamLink { get; set; }
        public int? RoleID { get; set; }
        public Role Role { get; set; }
    }
}