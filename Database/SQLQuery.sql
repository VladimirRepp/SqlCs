-- Укажите ваше название БД
CREATE DATABASE [(412)database]

--Для повторного использования активируем БД
USE [(412)database]

--Создаем необходимые таблицы
GO
CREATE TABLE Users
(
	Id INT IDENTITY (1, 1) NOT NULL,
    Login NVARCHAR(20) NOT NULL,	
    Pasword VARCHAR(30) NOT NULL,
    Role VARCHAR(20) NOT NULL
)

GO
CREATE TABLE Clients
(
	Id INT IDENTITY (1, 1) NOT NULL,
    Fullname NVARCHAR(20) NOT NULL,	
    Passport VARCHAR(30) NOT NULL,
    Phone VARCHAR(20) NOT NULL
)

GO
CREATE TABLE Contracts
(
	Id INT IDENTITY (1, 1) NOT NULL,
	IdClient INT NOT NULL,
	IdSchedule INT NOT NULL
)

GO
CREATE TABLE Staff
(
	Id INT IDENTITY (1, 1) NOT NULL,
    Fullname NVARCHAR(50) NOT NULL,	
    Position VARCHAR(50) NOT NULL,
    Phone VARCHAR(20) NOT NULL
)

GO
CREATE TABLE Schedules
(
	Id INT IDENTITY (1, 1) NOT NULL,
	IdSpecialization INT NOT NULL,
    [Date] NVARCHAR(20) NOT NULL,	
	[Time] VARCHAR(10) NOT NULL
)

GO
CREATE TABLE Specializations
(
	Id INT IDENTITY (1, 1) NOT NULL,
	IdEmployee INT NOT NULL,
    [Name] NVARCHAR(20) NOT NULL,	
	[Office] VARCHAR(50) NOT NULL,
	Cost VARCHAR(10) NOT NULL
)

--Проветка
Go
Select * From Clients

Go
Select * From Staff

Go
Select * From Contracts

Go
Select * From Schedules

Go
Select * From Specializations

ALTER TABLE Contracts
ADD RegistrationDate NVARCHAR(50) NULL;

GO
UPDATE Contracts SET RegistrationDate = '05.06.2023'

-- Создать БД
GO
CREATE DATABASE "(425)database"

-- Использовать БД
GO
USE "(425)database"

-- Создать таблицу 
GO
CREATE TABLE Employee 
(
    Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    Fullname VARCHAR(255) NOT NULL,
	Phone VARCHAR(15),
	Email VARCHAR(30),
    Position VARCHAR(50),
    Birthday Date
);

-- Удалить таблицу
GO
DROP TABLE Employee;

-- Выборка всех данных из таблицы 
GO
SELECT * FROM Employee;

-- Очистить таблицу 
GO
TRUNCATE TABLE Employee;

-- Вставка данных в таблицу 
GO
INSERT INTO Employee 
VALUES ('Иван Иванов Иванович', '111-000', '1mail@mail.com', 'Должность 1', '1989-01-01');
INSERT INTO Employee 
VALUES ('Иван1 Иванов1 Иванович1', '111-111', '2mail@mail.com', 'Должность 2', '1989-02-02');
INSERT INTO Employee 
VALUES ('Иван2 Иванов2 Иванович2', '111-222', '3mail@mail.com', 'Должность 3', '1989-03-03');
INSERT INTO Employee 
VALUES ('Иван3 Иванов3 Иванович3', '111-333', '4mail@mail.com', 'Должность 4', '1989-04-04');
INSERT INTO Employee 
VALUES ('Иван4 Иванов4 Иванович4', '111-444', '4mail@mail.com', 'Должность 5', '1989-05-05');