using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
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

        [HttpPost]
        public ActionResult SearchRecipes(List<float> userIngredients, int userId, int budget, int alottedTime, int healthiness)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyKitchenDatabaseConnectionString"].ToString();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                IQueryable<Recipe> recipes = this.Database.Recipe.Where(x => x.timeToComplete < alottedTime);

                if (!recipes.Any())
                    return null;

                var healthiestRecipes =
                    recipes.Where(x => x.healthRating <= healthiness).OrderBy(x => x.healthRating).Reverse().Take(20);

                var makeableRecipes = healthiestRecipes;

                for (int i = 1; i < userIngredients.Count + 1; i++)
                {
                    int userIngredientCount = 0;
                    using (
                        var command =
                            new SqlCommand(String.Format(
                                "select ingredient{0}Count from UserIngredient where id = {1}", i, userId)))
                    {
                        SqlDataReader reader = command.ExecuteReader();
		                while (reader.Read())
		                {
		                    userIngredientCount = reader.GetInt32(0);
		                }
                    }
                    
                    using (var command = new SqlCommand(String.Format("select id from Recipe where ingredient{0}Count > {1} ", i, userIngredientCount)))
                    {
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            makeableRecipes = makeableRecipes.Where(x => x.ID != id);
                        }
                    }
                }

                // Nutrition comes into play later.
                // For now, order by the cooking duration.
                return Json(makeableRecipes.OrderBy(x => x.timeToComplete).ToList());
            }
        }
    }
}
