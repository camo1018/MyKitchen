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

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
