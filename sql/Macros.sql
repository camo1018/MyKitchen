--Populate Ingredients in Recipe Table
declare @id int
declare ingredientCursor cursor
	for select id from Ingredient
open ingredientCursor
fetch next from ingredientCursor
	into @id
	while @@FETCH_STATUS = 0
	begin
		declare @sql nvarchar(100)
		set @sql = 'alter table Recipe add ingredient' + cast(@id as nvarchar(10)) + 'Count int default(0)'
		exec sp_executesql @sql
		fetch next from ingredientCursor into @id
	end
close ingredientCursor
deallocate ingredientCursor
go

--Populate nutrients in Recipe Table
declare @id int
declare nutrientCursor cursor
	for select id from Nutrient
open nutrientCursor
fetch next from nutrientCursor
	into @id
	while @@FETCH_STATUS = 0
	begin
		declare @sql nvarchar(100)
		set @sql = 'alter table Recipe add nutrient' + cast(@id as nvarchar(10)) + 'Count int default(0)'
		exec sp_executesql @sql
		fetch next from nutrientCursor into @id
	end
close nutrientCursor
deallocate nutrientCursor
go

--Populate Ingredients in UserIngredient Table
declare @id int
declare ingredientCursor cursor
	for select id from Ingredient
open ingredientCursor
fetch next from ingredientCursor
	into @id
	while @@FETCH_STATUS = 0
	begin
		declare @sql nvarchar(100)
		set @sql = 'alter table UserRecipe add ingredient' + cast(@id as nvarchar(10)) + 'Count int default(0)'
		exec sp_executesql @sql
		fetch next from ingredientCursor into @id
	end
close ingredientCursor
deallocate ingredientCursor
go