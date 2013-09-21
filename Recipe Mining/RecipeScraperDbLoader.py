import urllib.request
import json
from bs4 import BeautifulSoup
import time
from sqlalchemy import *
import sys

start = time.time()

def getAll(url):
    soup = BeautifulSoup(urllib.request.urlopen(url));
    name = soup.find("meta",{"id":"metaOpenGraphTitle"})['content']
    servingSize =  int(soup.find("div",{"class":"servings"}).find("span",{"id":"lblYield"}).string[:2])
    try:
        hours = soup.find("span",{"id":"totalHoursSpan"}).find("em").string
        if hours == "":
            hours = 0
        else:
            hours = int(hours)
    except(AttributeError):
        hours = 0
    try:
        mins = soup.find("span",{"id":"totalMinsSpan"}).find("em").string
        if mins == "":
            mins = 0
        else:
            mins = int(mins)
    except(AttributeError):
        mins = 0
    timeToPrep = hours*60+mins
    
    ingredients = []
    for li in soup.find_all("li",{"id":"liIngredient"}):
        try:
            amount = li.find("span",{"id":"lblIngAmount"}).string
        except(AttributeError):
            amount = None
        try:
            iGname = li.find("span",{"id":"lblIngName"}).string
        except(AttributeError):
            iGname =None
        ingredients.append((amount, iGname))
    directions = ""
    founds = soup.find_all("span",{"class":"plaincharacterwrap break"})
    for i in range(len(founds)):
        directions += str(i)+". "+ founds[i].string+"\n"
    return [name, servingSize, timeToPrep,ingredients,directions]

if __name__ == "__main__":
    print("This will either populate your Ingredients table or your Recipes table")
    time.sleep(3)
    print("\n")
    print("Please make sure that the table you wish to populate has been created but that all rows are dropped")
    time.sleep(3)
    print("\n")
    if input("Continue? Y/n ") == "n":
        sys.exit(0);
    print("\ndialect+driver://username:password@host:port/database")
    url = input("Enter: ")
    engine = create_engine(url)
    cur = engine.cursor()
    print("\n")
    test = input("Run in test mode? (will only attempt the first query and then terminate) (Y/n) ")
    print("\n")
    a = input("Populate Ingredients? (will not populate Recipes)")
    for i in range(1,101):
        print("PAGE %d OF 101"%i)
        req = urllib.request.urlopen("http://www.recipepuppy.com/api/?p="+str(i))
        data = json.loads(req.read().decode('utf-8'))
        urls = [ele['href'] for ele in data['results']]
        for x in urls:
            print("\tPARSING: "+x)
            try:
                info = getAll(x)
                if a=="y" or a=="Y" or a=="Yes" or a =="yes" or a=="YES":
                    for t in info[3]:
                        result = cur.execute("INSERT INTO Ingredients(name) VALUES(%s)"%t)
                else:
                    for y in info:
                        result = cur.execute("INSERT INTO Recipe(name,servingSize,timeToPrep,directions) VALUES(%s,%d,%d,%s)"%(y[0],y[1],y[2],y[4]))
                        for ingNum in range(len(y[3])):
                            ingKey = cur.execute("SELECT id FROM Ingredients WHERE name = %s"%y[3][ingNum][1])['id']
                            cur.execute("INSERT INTO Recipe(Ingredient%dId, Ingredient%dAmount) VALUES(%d, %s)"%(ingNum,ingNum,ingKey,y[3][ingNum][0]))
            except:
                print("passing")
                pass
            if test == "Y":
                break
    curr.close()
    engine.close()
    print("COMPLETED FULL MINING AND WRITING IN TIME(s): "+str(time.time()-start))
