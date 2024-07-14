
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
	('Elvin', 'Sirajli', 16, 'elvin123','elvin2345')


