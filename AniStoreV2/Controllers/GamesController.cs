using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AniStoreV2.Models;

namespace AniStoreV2.Controllers
{
    public class GamesController : Controller
    {
        private GameContext db = new GameContext();

        // GET: Games
        public ActionResult Index()
        {
            var games = db.Games.Include(g => g.Genre);
            return View(games.ToList());
        }

        [HttpGet]
        public ActionResult Search(int? genre)
        {
            IQueryable<Game> games = db.Games.Include(g => g.Genre);
            if (genre != null && genre != 0)
            {
                games = games.Where(n => n.GenreID == genre);
            }
            List<Genre> genres = db.Genres.ToList();
            genres.Insert(0, new Genre { Id = 0, Name = "Все" });
            GameSearch search = new GameSearch { Games = games, Genres = new SelectList(genres, "Id", "Name") };
            return View(search);
        }

        // GET: Games/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Game game = db.Games.Find(id);
            Genre genre = db.Genres.FirstOrDefault(s => s.Id == game.GenreID);
            ViewBag.Name = genre.Name;
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            ViewBag.GenreID = new SelectList(db.Genres, "Id", "Name");
            return View();
        }

        // POST: Games/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Author,Description,Price,Rating,Photo,GenreID")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Games.Add(game);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreID = new SelectList(db.Genres, "Id", "Name", game.GenreID);
            return View(game);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreID = new SelectList(db.Genres, "Id", "Name", game.GenreID);
            return View(game);
        }

        // POST: Games/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Author,Description,Price,Rating,Photo,GenreID")] Game game)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.GenreID = new SelectList(db.Genres, "Id", "Name", game.GenreID);
            return View(game);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Game game = db.Games.Find(id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
