using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyKitchen.Database;

namespace MyKitchen.Controllers
{
    public class UsersController : DatabaseController
    {
        #region Razor Models
        public class RegistrationRazorModel
        {
            public string newUsername, newPassword;
        }

        public class LoginRazorModel
        {
            public string username, password;
        }
        #endregion

        public ActionResult Login()
        {
            return View(new LoginRazorModel());
        }

        public ActionResult Registration()
        {
            return View(new RegistrationRazorModel());
        }

        [HttpPost]
        public ActionResult AddUser(string username, string password)
        {
            int newId = this.Database.RegisteredUser.Count() + 1;
            var newUser = new RegisteredUser { ID = newId, userName = username, userPassword = password };

            ((Table<RegisteredUser>)this.Database.RegisteredUser).InsertOnSubmit(newUser);

            var result = this.Database.SubmitChanges();

            return Json(result);
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var hasAny = this.Database.RegisteredUser.Any(user => user.userName == username && user.userPassword == password);

            return Json(hasAny);
        }
    }
}
