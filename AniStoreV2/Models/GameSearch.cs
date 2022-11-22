using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using AniStoreV2.Models;

namespace AniStoreV2.Models
{
    public class GameSearch
    {
        public IEnumerable<Game> Games { get; set; }
        public SelectList Genres { get; set; }
    }
}