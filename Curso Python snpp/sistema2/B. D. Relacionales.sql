-- python_sql.sql
DROP DATABASE IF EXISTS curso_python_db_madrid;
CREATE DATABASE curso_python_db_madrid
CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
USE curso_python_db_madrid;

-- Tabla Categoría
CREATE TABLE Categoria (
  id_categoria INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL UNIQUE
);

-- Tabla Productos
CREATE TABLE Producto (
  id_producto INT AUTO_INCREMENT PRIMARY KEY,
  nombre VARCHAR(100) NOT NULL,
  id_categoria INT,
  precio DECIMAL(10,2) DEFAULT 0.0,
  iva_porcentaje TINYINT DEFAULT 21,
  CONSTRAINT fk_categoria FOREIGN KEY (id_categoria)
    REFERENCES Categoria(id_categoria)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);

-- Tabla Inventario
CREATE TABLE Inventario (
  id_producto INT PRIMARY KEY,
  cantidad INT CHECK (cantidad >= 0),
  stock_minimo INT DEFAULT 0 CHECK (stock_minimo >= 0),
  CONSTRAINT fk_inventario_producto FOREIGN KEY (id_producto)
    REFERENCES Producto(id_producto)
    ON DELETE CASCADE
    ON UPDATE CASCADE
);
-- Base de Datos Relacionales                Programación Python

-- Trigger: Al insertar producto, crear fila en Inventario
DROP TRIGGER IF EXISTS after_insert_producto;

DELIMITER $$
CREATE TRIGGER after_insert_producto
AFTER INSERT ON Producto
FOR EACH ROW
BEGIN
  INSERT INTO Inventario (id_producto, cantidad, stock_minimo)
  VALUES (NEW.id_producto, 0, 0);
END $$
DELIMITER ;
select * FROM CATEGORIA;

INSERT INTO CATEGORIA VALUES (default, "PANIFICADOS");
INSERT INTO CATEGORIA(id_categoria, nombre VALUES(default, "BEBIDAS");
INSERT INTO CATEGORIA(nombre) VALUES ("FRUTAS Y VERDURAS");

-- Insertar Categorías
INSERT INTO Categoria (nombre) VALUES
('Bebidas'), ('Snacks'), ('Dulces');

-- Insertar Productos
INSERT INTO Producto (nombre, id_categoria, precio, iva_porcentaje)
VALUES
('Coca Cola', 1, 1.2, 21),
('Agua', 1, 0.8, 10),
('Patatas', 2, 1.5, 21),
('Galletas', 3, 2.0, 10),
('Chicle', 3, 0.5, 21);
insert into PRODUCTO VALUES(default, "PAN FELIPE",1,7000,10);
-- Consultar productos y categorías
SELECT * FROM Producto;
SELECT * FROM Categoria;
select * From Inventario;

-- actualizar el stock de los productos
UPDATE Inventario
SET cantidad = cantidad + 50
WHERE id_producto = 3;

-- actualizar precio
UPDATE PRODUCTO SET PRECIO = PRECIO + 1000 WHERE ID_PRODUCTO = 1;
-- recuperar productos y sus categorías y su stock

SELECT C.NOMBRE AS "CATEGORIA", P.NOMBRE AS "PRODUCTO" , I.CANTIDAD AS "STOCK"
FROM CATEGORIA C JOIN PRODUCTO P ON C.ID_CATEGORIA = P.ID_CATEGORIA
JOIN INVENTARIO I ON P.ID_PRODUCTO = I.ID_PRODUCTO;
