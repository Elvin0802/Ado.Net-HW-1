
USE [master]
GO

CREATE DATABASE [AppUsers]
GO

USE [AppUsers]
GO

CREATE TABLE [MainUsers]
(
[Id] int IDENTITY(1, 1) NOT NULL,
[Name] nvarchar(50) NULL,
[Surname] nvarchar(50) NULL,
[Age] int NULL,
[Username] nvarchar(50) NOT NULL,
[Password] nvarchar(50) NOT NULL,

CONSTRAINT PK_MainUsers PRIMARY KEY([Id])
)
GO

INSERT INTO [MainUsers] 
	([Name], [Surname], [Age], [Username], [Password])
VALUES 
	('Elvin0', 'Sirajli0', 21, 'elvin0','elvin00'),
	('Elvin1', 'Sirajli1', 22, 'elvin1','elvin11'),
	('Elvin2', 'Sirajli2', 23, 'elvin2','elvin22'),
	('Elvin3', 'Sirajli3', 24, 'elvin3','elvin33'),
	('Elvin4', 'Sirajli4', 25, 'elvin4','elvin44')
GO

SELECT * 
FROM [MainUsers]
ORDER BY [Name]

