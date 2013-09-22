import urllib.request
import json
from bs4 import BeautifulSoup
import time

start = time.time()

def nutrition(soup):
    nutritionTable = soup.find("div",{"id":"nutritiontable"})
    find("p",{"class":"top"})

def getAll(url):
       



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
