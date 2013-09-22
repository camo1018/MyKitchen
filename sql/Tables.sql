create table Ingredient (
id int primary key identity,
ingredientName varchar(255) not null,
measuringUnit varchar(255) not null
)

create table Nutrient (
id int primary key identity,
nutrientName varchar(255) not null,
dailyValue float
)

create table Recipe (
id int primary key identity,
recipeName varchar(255) not null,
imageUrl varchar(MAX),
healthRating int default(0),
timeToComplete int default(0)
-- Need to add all the ingredients and nutrients counts here as well.
)

create table UserRecipe (
id int primary key identity,
userId int not null foreign key references RegisteredUser(id),
recipeId int not null foreign key references Recipe(id)
)

create table UserIngredient (
id int primary key identity,
-- Need to add all the ingredient counts here.
)

create table RegisteredUser (
id int primary key identity,
userName varchar(16) not null,
userPassword varchar(16) not null, -- No encryption for the demo!
)