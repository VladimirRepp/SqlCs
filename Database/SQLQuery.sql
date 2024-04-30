-- ������� ���� �������� ��
CREATE DATABASE [(412)database]

--��� ���������� ������������� ���������� ��
USE [(412)database]

--������� ����������� �������
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

--��������
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

-- ������� ��
GO
CREATE DATABASE "(425)database"

-- ������������ ��
GO
USE "(425)database"

-- ������� ������� 
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

-- ������� �������
GO
DROP TABLE Employee;

-- ������� ���� ������ �� ������� 
GO
SELECT * FROM Employee;

-- �������� ������� 
GO
TRUNCATE TABLE Employee;

-- ������� ������ � ������� 
GO
INSERT INTO Employee 
VALUES ('���� ������ ��������', '111-000', '1mail@mail.com', '��������� 1', '1989-01-01');
INSERT INTO Employee 
VALUES ('����1 ������1 ��������1', '111-111', '2mail@mail.com', '��������� 2', '1989-02-02');
INSERT INTO Employee 
VALUES ('����2 ������2 ��������2', '111-222', '3mail@mail.com', '��������� 3', '1989-03-03');
INSERT INTO Employee 
VALUES ('����3 ������3 ��������3', '111-333', '4mail@mail.com', '��������� 4', '1989-04-04');
INSERT INTO Employee 
VALUES ('����4 ������4 ��������4', '111-444', '4mail@mail.com', '��������� 5', '1989-05-05');