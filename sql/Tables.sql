create table Ingredient (
id int primary key identity,
ingredientType varchar(255) not null,
measuringUnit varchar(255) not null
)

create table Recipe (
id int primary key identity,
recipeName varchar(255) not null,
imageUrl varchar(MAX),
healthRating int default(0),
timeToComplete int default(0),
ingredient1Id int foreign key references Ingredient(id),
ingredient1Count float default(0),
ingredient2Id int foreign key references Ingredient(id),
ingredient2Count float default(0),
ingredient3Id int foreign key references Ingredient(id),
ingredient3Count float default(0),
ingredient4Id int foreign key references Ingredient(id),
ingredient4Count float default(0),
ingredient5Id int foreign key references Ingredient(id),
ingredient5Count float default(0),

)

create table RegisteredUser (
id int primary key identity,
userName varchar(16) not null,
userPassword varchar(16) not null, -- No encryption for the demo!
)