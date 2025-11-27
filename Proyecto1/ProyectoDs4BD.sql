
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'CalculadoraDB')
BEGIN
    CREATE DATABASE CalculadoraDB;
    PRINT 'Base de datos CalculadoraDB creada exitosamente';
END
ELSE
BEGIN
    PRINT 'La base de datos CalculadoraDB ya existe';
END
GO


USE CalculadoraDB;
GO


IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Calculos]') AND type in (N'U'))
BEGIN
    CREATE TABLE Calculos (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Valor1 FLOAT NOT NULL,
        Valor2 FLOAT NOT NULL,
        Operacion VARCHAR(10) NOT NULL,
        Resultado FLOAT NOT NULL,
        Fecha DATETIME DEFAULT GETDATE()
    );
    
    PRINT 'Tabla Calculos creada exitosamente';
END
ELSE
BEGIN
    PRINT 'La tabla Calculos ya existe';
END
GO


SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Calculos';
GO


SELECT * FROM Calculos ORDER BY Fecha DESC;
GO