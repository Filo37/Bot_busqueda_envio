import os, time, random
def Lanzar_dados ():
    return random. randrange (1,7)
while True:
    op = int (input ("Elige un número del dado: "))
    if op >=1 and op <=6:
        os. system("cls")
        print (f"Elegiste el {op}")
        print ("Lanzamos el dado")
        time. sleep (3)
        dado = lanzar_dados ()
        print (f"Ha caido el {dado}")
    else:
        print ("Número inválido. Inténtalo de nuevo.")
   
   
    if dado == op :
        print ("Buena suerte, haz ganado!!!")
    else:
        print ("Haz perdido, vuelve a intentarlo!!!")
    print ("Juego terminado")
    break 