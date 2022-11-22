using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AniStoreV2.Models;

namespace AniStoreV2.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model) // Создаём пост-метод, используя в качестве данных модель регистрации
        {
            if(ModelState.IsValid) // Если модель существует, то
            {
                User user = null; // Задаём переменную класса пользователь
                using(GameContext db = new GameContext()) // Вставляем функцию using для оптимизации
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email); // Находим первого попавшегося пользователя по логину
                    if(user == null) // Запускаем условный оператор для исключения
                    {
                        using(GameContext db1 = new GameContext())
                        {
                            db1.Users.Add(new Models.User { Email = model.Email, Password = model.Password, SteamLink = null, RoleID = 1 });
                            db1.SaveChanges();
                            user = db1.Users.Where(u => u.Email == model.Email && u.Password == model.Password).FirstOrDefault();
                        }
                        if(user != null)
                        {
                            FormsAuthentication.SetAuthCookie(model.Email, true);
                            return RedirectToAction("Index", "Games");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Пользователь с таким логином уже существует!");
                        }
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (GameContext db = new GameContext())
                {
                    user = db.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Games");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя не существует, попробуйте зарегистрироваться!");
                }
            }
            return View(model);
        }
    }
}