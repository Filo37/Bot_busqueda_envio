num1 = int(input("ingrese un numero inicial: "))
num2 = int(input("ingrese otro mumero limite: "))


print("valores entre", num1, "y", num2)
for x in range(num1+1, num2):
    print( f"| {x} ", end="")
print("|", end="\n")