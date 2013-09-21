create table Recipes (
id int primary key identity,
recipeName varchar(255) not null,
imageUrl varchar(MAX),
healthRating int default(0),
timeToComplete int default(0),
ingredient1Id int foreign key references Ingredients(id),
ingredient1Count int default(0),
ingredient2Id int foreign key references Ingredients(id),
ingredient2Count int default(0),
ingredient3Id int foreign key references Ingredients(id),
ingredient3Count int default(0),
ingredient4Id int foreign key references Ingredients(id),
ingredient4Count int default(0),
ingredient5Id int foreign key references Ingredients(id),
ingredient5Count int default(0),

)

create table Ingredients (
id int primary key identity,
ingredientType varchar(255) not null,
measuringUnit varchar(255) not null
)
