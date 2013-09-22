using System;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace MyKitchen.Database
{
    public class KitchenDatabase : IKitchenDatabase, IDisposable
    {
        private DataContext context;

        private IQueryable<Recipe> recipe;
        private IQueryable<Ingredient> ingredient;
        private IQueryable<RegisteredUser> registeredUser;
        private IQueryable<Nutrient> nutrient;
        private IQueryable<UserRecipe> userRecipe;
        private IQueryable<UserIngredient> userIngredient;

        public KitchenDatabase(IDbConnection connection)
        {
            context = new DataContext(connection);
        }

        public void SetDataLoadOptions(DataLoadOptions options)
        {
            this.context.LoadOptions = options;
        }

        public bool SubmitChanges()
        {
            try
            {
                this.context.SubmitChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IQueryable<Recipe> Recipe
        {
            get { return recipe ?? (recipe = context.GetTable<Recipe>()); }

            private set { recipe = value; }
        }

        public IQueryable<Ingredient> Ingredient
        {
            get { return ingredient ?? (ingredient = context.GetTable<Ingredient>()); }

            private set { ingredient = value; }
        }

        public IQueryable<RegisteredUser> RegisteredUser
        {
            get { return registeredUser ?? (registeredUser = context.GetTable<RegisteredUser>()); }

            private set { registeredUser = value; }
        }

        public IQueryable<Nutrient> Nutrient
        {
            get { return nutrient ?? (nutrient = context.GetTable<Nutrient>()); }

            private set { nutrient = value; }
        }

        public IQueryable<UserRecipe> UserRecipe
        {
            get { return userRecipe ?? (userRecipe = context.GetTable<UserRecipe>()); }

            private set { userRecipe = value; }
        }

        public IQueryable<UserIngredient> UserIngredient
        {
            get { return userIngredient ?? (userIngredient = context.GetTable<UserIngredient>()); }

            private set { userIngredient = value; }
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
