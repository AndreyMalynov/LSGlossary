using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LSGlossary.Models;
using LSGlossary.Abstract;
using System.Web.Security;

namespace LSGlossary.Controllers
{
    public class HomeController : Controller
    {
        private IUsersContext users;

        public HomeController()
        {
            users = new UsersContext();
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("PersonalPage");
            return View();
        }

        [HttpPost]
        public ActionResult Login(string login)
        {
            int idOfUser = users.GetUserIdByLogin(login);
            if (idOfUser == -1)
            {
                users.AddUserIdentity(login);
                idOfUser = users.GetUserIdByLogin(login);

            }

            FormsAuthentication.SetAuthCookie(idOfUser.ToString(), true);
            return RedirectToAction("PersonalPage");
        }

        public ActionResult PersonalPage()
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login");

            using(UserContext user = new UserContext(int.Parse(User.Identity.Name)))
            {
                UserForView userForView = new UserForView(user.GetLogin(), user.GetWords());
                return View(userForView);
            }           
        }
    }
}
