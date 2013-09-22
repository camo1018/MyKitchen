using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyKitchen.Database
{
    [Table]
    public class Recipe
    {
        private EntityRef<Ingredient> _ingredient;

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(CanBeNull = false)]
        public string recipeName { get; set; }

        [Column]
        public string imageUrl { get; set; }

        [Column]
        public int healthRating { get; set; }

        [Column]
        public int timeToComplete { get; set; }

        [Column]
        public string instruction { get; set; }

        // Ingredients

        // Nutrients
    }

    [Table]
    public class Ingredient
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(CanBeNull = false)]
        public string ingredientType { get; set; }

        [Column(CanBeNull = false)]
        public string measuringType { get; set; }
    }

    [Table]
    public class RegisteredUser
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(CanBeNull = false)]
        public string userName { get; set; }

        [Column(CanBeNull = false)]
        public string userPassword { get; set; }
    }

    [Table]
    public class Nutrient
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(CanBeNull = false)]
        public string nutrientName { get; set; }
    }

    [Table]
    public class UserRecipe
    {
        private EntityRef<Recipe> _recipe;

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column(CanBeNull = false)]
        public int recipeId { get; set; }

        [Association(IsForeignKey = true, OtherKey = "ID", ThisKey = "recipeId", Storage = "_recipe")]
        public Recipe recipe
        {
            get { return _recipe.Entity; }
            set { _recipe.Entity = value; ID = value.ID; }
        }
    }

    [Table]
    public class UserIngredient
    {
        private EntityRef<Ingredient> _ingredient;

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        // Ingredients
    }
}
