create table Ingredient (
id int auto_increment primary key,
ingredientName varchar(255) not null,
measuringUnit varchar(255) not null
)

create table Nutrient (
id int auto_increment primary key,
nutrientName varchar(255) not null,
)

create table Recipe (
id int auto_increment primary key,
portion varchar(20) not null,
recipeName varchar(255) not null,
<<<<<<< HEAD
imageUrl varchar(400) not null,
healthRating int,
timeToComplete int,
directions varchar(1000) not null,
calories int(10) not null,
caloriesFromFat int(10) not null
)

create table RegisteredUser (
id int auto_increment primary key,
userName varchar(16) not null,
userPassword varchar(16) not null
=======
imageUrl varchar(MAX),
healthRating int default(0),
timeToComplete int default(0),
instruction varchar(MAX)
-- Need to add all the ingredients and nutrients counts here as well.
>>>>>>> 5ef945728b70d72d193610957367af3a61984ecd
)

create table RegisteredUser (
id int primary key identity,
userName varchar(16) not null,
userPassword varchar(16) not null -- No encryption for the demo!
)

create table UserRecipe (
id int auto_increment primary key,
userId int,
foreign key(userId) references RegisteredUser(id),
recipeId int,
foreign key(recipeId) references Recipe(id)
)

create table UserIngredient (
<<<<<<< HEAD
id int auto_increment primary key
=======
id int primary key identity
-- Need to add all the ingredient counts here.
>>>>>>> 5ef945728b70d72d193610957367af3a61984ecd
)

