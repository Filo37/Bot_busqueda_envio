num1 = int(input("ingrese un numero:"))
num2 = int(input("ingrese otro numero:"))


if num1>num2:
    print(f" {num1} es mayor")
    if num1 % 2 == 0:
        print("es par")
else:
    print("es inpar")
elif num2>num1:
    print(f" {num2} es mayor a {num1}")
    if num2 % 2 == 0:
        print("es par")
    else:
        print("es inpar")
else:
        print("ambos numeros son iguales")
