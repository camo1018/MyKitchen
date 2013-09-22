using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyKitchen.Database
{
    public interface IKitchenDatabase
    {
        bool SubmitChanges();

        IQueryable<Recipe> Recipe { get; }
        IQueryable<Ingredient> Ingredient { get; }
        IQueryable<RegisteredUser> RegisteredUser { get; }
        IQueryable<Nutrient> Nutrient { get; }
        IQueryable<UserRecipe> UserRecipe { get; }
        IQueryable<UserIngredient> UserIngredient { get; }
    }
}
