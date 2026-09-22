import os
import time


def esperar (texto):
    for x in range(5):
        os. system(" cls")
        print (f" Cargando {texto} {x * 20} %")
        time.sleep(0.5)
while True:
    os. system("cls")
    print("Dashboard de Windows")
    op = input("1. Calc\n2. Paint\n3. Apagar\n4. No apagar\nø. Salir\nOpción: ")
    if op == "1":
        esperar("Calculadora")
        os. system("calc")
    elif op == "2":
        esperar ("Paint")
        os. system("mspaint")
    elif op == "3":
        esperar(" Apagando en 5 minutos")
        os. system(" shutdown -5 -t 300")
    elif op == "4":
        esperar ("Cancelar apagado")
        os. system(" shutdown -a")
    elif op == "0":
        print("FIN")
        break
else:
    print(" (6_o)")