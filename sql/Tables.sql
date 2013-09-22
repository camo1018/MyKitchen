create table Ingredient (
id int auto_increment primary key,
ingredientName varchar(255) not null,
measuringUnit varchar(255) not null
)

create table Nutrient (
id int auto_increment primary key,
nutrientName varchar(255) not null,
dailyValue float
)

create table Recipe (
id int auto_increment primary key,
portion varchar(20) not null,
recipeName varchar(255) not null,
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
)

create table UserRecipe (
id int auto_increment primary key,
userId int,
foreign key(userId) references RegisteredUser(id),
recipeId int,
foreign key(recipeId) references Recipe(id)
)

create table UserIngredient (
id int auto_increment primary key
)

