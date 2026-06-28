'''
    Contar palabras de un archivo o una web
'''

from urllib import request
from urllib.error import URLError
def contar_palabras(url):
    try:
        f = request.urlopen(url)
    except URLError:
        return "error de url"
    else:
        contenido = f.read()
        return len(contenido.split())

url = 'https://es.wikipedia.org/wiki/Python'
print(contar_palabras(url))
print("\n-------------------------------\n")
print(contar_palabras(url))