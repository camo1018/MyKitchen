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
        public int ingredient1ID { get; set; }

        [Association(IsForeignKey = true, OtherKey = "ID", ThisKey = "ingredient1Id", Storage = "_ingredient")]
        public Ingredient ingredient1
        {
            get { return _ingredient.Entity; }
            set { _ingredient.Entity = value; ID = value.ID; }
        }

        [Column]
        public float ingredient1Count { get; set; }

        [Column]
        public int ingredient2ID { get; set; }

        [Association(IsForeignKey = true, OtherKey = "ID", ThisKey = "ingredient2Id", Storage = "_ingredient")]
        public Ingredient ingredient2
        {
            get { return _ingredient.Entity; }
            set { _ingredient.Entity = value; ID = value.ID; }
        }

        [Column]
        public float ingredient2Count { get; set; }

        [Column]
        public int ingredient3Id { get; set; }

        [Association(IsForeignKey = true, OtherKey = "ID", ThisKey = "ingredient3Id", Storage = "_ingredient")]
        public Ingredient ingredient3
        {
            get { return _ingredient.Entity; }
            set { _ingredient.Entity = value; ID = value.ID; }
        }

        [Column]
        public float ingredient3Count { get; set; }

        [Column]
        public int ingredient4Id { get; set; }

        [Association(IsForeignKey = true, OtherKey = "ID", ThisKey = "ingredient4Id", Storage = "_ingredient")]
        public Ingredient ingredient4
        {
            get { return _ingredient.Entity; }
            set { _ingredient.Entity = value; ID = value.ID; }
        }

        [Column]
        public float ingredient4Count { get; set; }

        [Column]
        public int ingredient5Id { get; set; }

        [Association(IsForeignKey = true, OtherKey = "ID", ThisKey = "ingredient5Id", Storage = "_ingredient")]
        public Ingredient ingredient5
        {
            get { return _ingredient.Entity; }
            set { _ingredient.Entity = value; ID = value.ID; }
        }

        [Column]
        public float ingredient5Count { get; set; }
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
}
