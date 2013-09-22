import urllib.request
import json
from bs4 import BeautifulSoup
import time
import sys
from sqlalchemy import *
import re
import pymysql

class AllRecipes:
    def __init__(self, url):
        self.url = url
        self.soup = BeautifulSoup(urllib.request.urlopen(url))

    def __str__(self):
        return self.url

    def getName(self):
        return  self.soup.find("meta",{"id":"metaOpenGraphTitle"})['content']

    def getImageSrc(self):
        return self.soup.find("img",{"id":"imgPhoto"}).src

    def getPortion(self):
         return int(self.soup.find("div",{"class":"servings"}).find("span",{"id":"lblYield"}).string[:2])

    def getHours(self):
        try:
            hours = self.soup.find("span",{"id":"totalHoursSpan"}).find("em").string
            if hours == "":
                hours = 0
            else:
                hours = int(hours)
        except(AttributeError):
            hours = 0
        return hours
    
    def getMinutes(self):
        try:
            mins = self.soup.find("span",{"id":"totalMinsSpan"}).find("em").string
            if mins == "":
                mins = 0
            else:
                mins = int(mins)
        except(AttributeError):
            mins = 0
        return mins

    def getTimeToPrep(self):
        return self.getHours()*60+self.getMinutes()

    def getIngredients(self):
        ingredients = []
        for li in self.soup.find_all("li",{"id":"liIngredient"}):
            try:
                amount = li.find("span",{"id":"lblIngAmount"}).string
            except(AttributeError):
                amount = ""
            try:
                iGname = li.find("span",{"id":"lblIngName"}).string
            except(AttributeError):
                iGname =""
            ingredients.append((amount, iGname))
        return ingredients

    def getDirections(self):
        directions = ""
        founds = soup.find_all("span",{"class":"plaincharacterwrap break"})
        for i in range(len(founds)):
            directions += str(i)+". "+ founds[i].string+"\n"
        return directions

    def getNutrition(self):
        nutritionTable = self.soup.find("div",{"id":"nutritiontable"})
        nutritionList = []
        servingSize = None;
        calories = None;
        caloriesFromFat = None;
        if nutritionTable:
            summary = nutritionTable.find("p",{"class":"top"})
            servingSize = summary.find("span",{"itemprop":"servingSize"}).string
            calories = int(summary.find("span",{"id":"litCalories"}).string)
            caloriesFromFat = int(summary.find_all("strong")[1].string)
            nutritionList = nutritionTable.find("ul",{"class":"nutrDetList"}).find_all("li")
        nutritionFacts = []
        for i in range(1,len(nutritionList)):
            element = ()
            for x in nutritionList[i].find_all("span"):
                if x.string != None:
                    element+=(x.string.replace("*",""),)
            if i==1 :
                element = ('Total Fat',)+element
            nutritionFacts.append(element)

        return [servingSize,calories,caloriesFromFat,nutritionFacts]
    
if __name__=="__main__":
    option = input("Populate Ingredients table(1)\n Populate Recipe Table(2)\n Test one row(3)\n Terminate(*)\n")
    if option == "1":
        user_input=input("user")
        passwd_input=input("passwd")
        host_input=input("host")
        db_input = input("db")
        connection = pymysql.connect(user=user_input,passwd=passwd_input,host=host_input,db=db_input)
        cur = connection.cursor()

        for i in range(1,2):
            print("PAGE %d OF 101"%i)
            req = urllib.request.urlopen("http://www.recipepuppy.com/api/?p="+str(i))
            data = json.loads(req.read().decode('utf-8'))
            urls = [ele['href'] for ele in data['results']]
            for x in urls:
                connection = pymysql.connect(user=user_input,passwd=passwd_input,host=host_input,db=db_input)
                cur = connection.cursor()
                print("\tPARSING: "+x)
                try:
                    recipe = AllRecipes(x);
                except:
                    print("random redirect error dang man")
                for t in recipe.getIngredients():
                    pattern = re.compile("[a-zA-Z]+")
                    units = pattern.search(t[0])
                    if units:
                        units = units.group()
                    else:
                        units = ""
                    try:
                        cur.execute("INSERT INTO ingredient(ingredientName,measuringUnit) VALUES('%s','%s') "%(units,t[1].replace("'","")))
                    except:
                        print("random error ug")
                pattern2 = re.compile("[0-9]+")
                tups = recipe.getNutrition()[3]
                for q in range(len(tups)):
                    cur.execute("INSERT INTO Nutrient(nutrientName) VALUES('%s') "%(tups[q][0])
                connection.commit()
                cur.close()
                connection.close()
    elif option == "2":
        connection = pymysql.connect(user=input("user"),passwd=input("passwd"),host=input("host"),db=input("db"))
        cur = connection.cursor()
        success = True
        for i in range(1,101):
            print("PAGE %d OF 101"%i)
            req = urllib.request.urlopen("http://www.recipepuppy.com/api/?p="+str(i))
            data = json.loads(req.read().decode('utf-8'))
            urls = [ele['href'] for ele in data['results']]
            for x in urls:
                print("\tPARSING: "+x)
                recipe = AllRecipes(x)
                nutritions = recipe.getNutrition()
                query1 = "INSERT INTO Recipe(recipeName,imageUrl,portion,timeToComplete,directions,ServingSize,Calories, CaloriesFromFat"
                query2 = "VALUES('%s','%s','%s',%d,'%s','%s',%d,%d"%(recipe.getName(),recipe.getImageSrc(),recipe.getPortion(),recipe.getTimeToPrep(),recipe.getDirections(),nutritions[0],nutritions[1],nutritions[2])
                ingreds = recipe.getIngredients()
                for ingNum in range(ingreds):
                    ingKey = cur.execute("SELECT id FROM ingredients WHERE ingredientName = '%s'"%ingreds[ingNum][1]).fetchall()[0]
                    query1 += ",Ingredient"+str(ingKey)
                    query2 += ",'%s'"%ingreds[ingNum][0]
                nutrients = nutritions[3]
                for nut in nutrients:
                    nutKey = cur.execute("SELECT id FROM Nutrient WHERE nutrientName = '%s'"%nut[0]).fetchall()[0]
                    query1+= ",Nutrient"+str(nutKey)
                    if nut[2]:
                        percentage = int(nut[2].replace("%","").replace(" ",""))
                    else:
                        percentage = int(nut[1].replace("%","").replace(" ",""))
                    query2+=",%d"%(nutKey,percentage)
                finalQuery = query1+") "+query2+")"
                cur.execute(finalQuery)
                connection.commit()
                cur.close()
                connection.close()
                    
    elif option == "3":
        connection = pymysql.connect(user=input("user"),passwd=input("passwd"),host=input("host"),db=input("db"))
        cur = connection.cursor()
        cur.execute("INSERT INTO ingredient(ingredientName, measuringUnit) VALUES('TEST','litres')")
        cur.close()
        connection.close()
    else:
        sys.exit(0);




    
