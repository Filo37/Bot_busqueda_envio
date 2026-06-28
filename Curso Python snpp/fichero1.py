texto = input('Introduce un texto')
nombre_fichero = 'archivo-' + texto + '.txt'
f = open(nombre_fichero, 'w') #apertura w= write, r = read, a = append
f.write(f'{texto}\n')
#escritura
fichero = open('data.txt', 'w')
texto = input("Ingrese un texto a guardar: ")
fichero.write(texto)
fichero.close()

#append, actualizar fichero
fichero = open('data.txt','a')

texto = input("Agregue otro texto: ")
fichero.write(f"{texto}")
fichero.close()

#leer fichero
fichero = open('data.txt', 'r')
for x in fichero:
    print(x)
fichero.close()