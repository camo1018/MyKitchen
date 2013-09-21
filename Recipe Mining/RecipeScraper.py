import urllib.request
import json
from bs4 import BeautifulSoup
import time

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
    for i in range(1,2):
        print("PAGE %d OF 101"%i)
        req = urllib.request.urlopen("http://www.recipepuppy.com/api/?p="+str(i))
        data = json.loads(req.read().decode('utf-8'))
        urls = [ele['href'] for ele in data['results']]
        f = open("recipes.txt","w")
        g = open("ingredients.txt","w")
        for x in urls:
            print("\tPARSING: "+x)
            try:
                info = getAll(x)
                f.write("^")
                for q in info:
                    f.write("~"+str(q))
                for t in info[3]:
                    g.write(t[1]+"\n")
            except:
                pass
    g.close()
    f.close()
    print("COMPLETED FULL MINING IN TIME(s): "+str(time.time()-start))
