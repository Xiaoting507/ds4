--Ejemplo 1
--El asterisco significa que se quiere seleccionar todos los campos de la
--tabla Products

SELECT * FROM Products

--Ejemplo 2
--Para seleccionar ciertos campos hay que colocarlos como en un listado
--en este ejemplo solo se mostraran los registros almacenados en los campos ProductID
--ProductName y UnitPrice

SELECT ProductID, ProductName, UnitPrice FROM Products

--Ejemplo 3
--Seleccionar los datos de la tabla Products donde el dato almacenado en el campo
--UnitPrice sea mayor a 15
SELECT ProductID, ProductName, UnitPrice
FROM Products
WHERE UnitPrice > 15

--Ejemplo 4
--Seleccionar los datos de la tabla Products donde el dato almacenado en el campo
--UnitPrice seae mayor o igual a 15 y menor o igual a 50
SELECT ProductID, ProductName, UnitPrice
FROM Products
WHERE UnitPrice >= 15 AND UnitPrice <= 50

--Ejemplo 5
--Otra forma de crear la consulta anterior es utilizando la instrucción BETWEEN
SELECT ProductID, ProductName, UnitPrice
FROM Products
WHERE UnitPrice BETWEEN 15 AND 50

--Ejemplo 6
--Haciendo uso del operador NOT obtenemos los registros de la tabla Products donde
--el dato almacenado en el campo UnitPrice sea menor que 15
SELECT ProductID, ProductName, UnitPrice
FROM Products
WHERE NOT UnitPrice > 15

--Ejemplo 7
--Seleccionar los registros de la tabla Products donde el dato almacenado en el campo
--ProductID sea mayor a 50 y el dato almacenado en el campo UnitPrice sea menor a 10
SELECT ProductID, ProductName, UnitPrice
FROM Products
WHERE ProductID > 15 OR UnitPrice < 10

--Ejemplo 8
--Seleccionar los campos EmployeeID y LastName de la tabla Employees donde el dato almacenado
--en el campo LastName comience con la letra D
SELECT EmployeeID, LastName FROM Employees
WHERE LastName LIKE 'D%'

--Ejemplo 9
--Seleccionar los campos EmployeeID y LastName de la tabla Employees donde el dato almacenado
--en el campo LastName termine con la letra N
SELECT EmployeeID, LastName FROM Employees
WHERE LastName LIKE '%N'

--Ejemplo 10
--Seleccionar los campos EmployeeID, LastName y Title de la tabla Employees donde el dato
--almacenado en el campo Title se encuentre en la palabra SALES, no importando en que posicion
SELECT EmployeeID, LastName, Title FROM Employees
WHERE Title LIKE '%SALES%'

--Ejemplo 11
--Seleccionar los campos EmployeeID y LastName de empleados EXCEPTO aquellos donde el dato almacenado
--en LastName comience con la letra D
SELECT EmployeeID, LastName FROM Employees
WHERE LastName NOT LIKE 'D%'

--Ejemplo 12
--Ordenar de forma ascendente los registros almacenados en los campos ProductID, ProductName
-- y UnitPrice de la tabla Products, se ordenaran por medio del campo ProductID
SELECT ProductID, ProductName, UnitPrice
FROM Products
ORDER BY ProductID ASC
--De forma predeterminada los datos se ordenan de forma ascendente, por lo tanto la instrucción
--ASC opcional

--Ejemplo 13
--Ordenar de forma descendente los registros almacenados en los campos ProductID, ProductName
-- y UnitPrice de la tabla Products, se ordenaran por medio del campo ProductID
SELECT ProductID, ProductName, UnitPrice
FROM Products
ORDER BY ProductID DESC

--Ejemplo 14
--Seleccionar todos los registros no repetidos almacenados en el campo OrderID
--de ta labla OrderDetails
SELECT DISTINCT OrderID FROM [Order Details]

--Ejemplo 15
--Mostrar los primeros cinco registros de la tabla Order Details
SELECT TOP 5 OrderID, ProductID, Quantity
FROM [Order Details]

--Ejemplo 16
--En el ejemplo siguiente se mostraran el 10% de todos los pedidos almacenados en la tabla Order Details
SELECT TOP 10 PERCENT OrderID, ProductID, Quantity
FROM [Order Details]

--Ejemplo 17
--Seleccionar los datos almacenados en el campo CategoryName de la tabla Categories y renombrar 
-- a la columna con el nombre Nombre de Categoria
SELECT CategoryName AS [Nombre de Categoria]
FROM Categories

--Ejemplo 18
--Se quiere conocer cual seria la fecha de envio (ShippedDate) con un retraso de 5 días
--Mostrar los campos OrderID, OrderDate y ShippedDate de la tabla Orders
SELECT OrderId, OrderDate, ShippedDate + 5 AS RetrasoEnvio
FROM Orders

--Ejemplo 19
SELECT OrderID, P.ProductID, ProductName
FROM Products P
INNER JOIN [Order Details] OD
ON P.ProductID=OD.ProductID

--Ejemplo 20
SELECT ProductName, CompanyName, ContactName
FROM Products P
FULL JOIN Suppliers S
ON P.SupplierID=S.SupplierID