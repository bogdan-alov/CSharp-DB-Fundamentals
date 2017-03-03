04. Insert Records in Both Tables

INSERT INTO Towns(Id,Name)
VALUES ('1', 'Sofia');
INSERT INTO Towns(Id,Name)
VALUES ('2', 'Plovdiv');
INSERT INTO Towns(Id,Name)
VALUES ('3', 'Varna');

INSERT INTO Minions(Id,Name, Age, TownId)
VALUES ('1', 'Kevin', '22', '1');
INSERT INTO Minions(Id,Name, Age, TownId)
VALUES ('2', 'Bob', '15', '3');
INSERT INTO Minions(Id,Name, Age, TownId)
VALUES ('3', 'Steward', NULL , '2');


07. Create Table People

CREATE TABLE People
(
 Id int Identity(1,1) PRIMARY KEY,
 Name nvarchar(200) NOT NULL,
 Picture image, 
 Height decimal,
 Weight decimal,
 Gender char(1) CHECK ( Gender = 'f' OR Gender = 'm') NOT NULL,
 Birthdate date NOT NULL,
 Biography nvarchar(max) 
)

INSERT INTO People (Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Atanas', NULL, '180', '78', 'm', '20000101', 'Hello Im Atanas');
INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Pesho', NULL, '180', '78', 'm', '20000102', 'Hello Im Pesho');
INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Gosho', NULL, '180', '78', 'm', '20000103', 'Hello Im Gosho');
INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Atanas', NULL, '180', '78', 'm', '20000105', 'Hello Im Atanas');
INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography)
VALUES ('Gergana', NULL, '160', '50', 'f', '19970103', 'Hello Im Gergana');


08. Create Table Users

CREATE TABLE Users 
(
 Id int Identity(1,1) PRIMARY KEY,
 Username varchar(200) NOT NULL,
 Password varchar(26) NOT NULL , 
 ProfilePicture image,
 LastLoginTime datetime,
 isDeleted Bit NOT NULL, 
)

INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, isDeleted)
VALUES ('alovbg213', '123321', NULL, NULL, 1);
INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, isDeleted)
VALUES ('alovbg21123', '123321', NULL, NULL, 1);
INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, isDeleted)
VALUES ('alovbg21123123', '123321', NULL, NULL, 1);
INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, isDeleted)
VALUES ('alovbg212131', '123321', NULL, NULL, 1);
INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, isDeleted)
VALUES ('alovbg211231', '123321', NULL, NULL, 1);


13. Movies Database

CREATE TABLE Directors 
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
DirectorName nvarchar(25) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Genres
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
GenreName varchar(10) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Categories
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
CategoryName varchar(10) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE Movies
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
Title nvarchar(50) NOT NULL,
DirectorId int NOT NULL,
CopyrightYear int NOT NULL,
Lenght decimal(5,2) NOT NULL,
GenreId int NOT NULL,
CategoryId int NOT NULL,
Rating decimal(4,2),
Notes nvarchar(max)
)

INSERT INTO Directors(DirectorName, Notes)
VALUES ('Bogdan Alov', NULL),
('Bogdan Alov', NULL),
('Bogdan Alov', NULL),
('Bogdan Alov', NULL),
('Bogdan Alov', NULL);


INSERT INTO Genres(GenreName, Notes)
VALUES ('Action', NULL),
('Comedy', NULL),
('SciFi', NULL),
('xXx', NULL),
('Horror', NULL);

INSERT INTO Categories(CategoryName, Notes)
VALUES ('School', NULL),
('Movies', NULL),
('Games', NULL),
('Facebook', NULL),
('Sport', NULL);

INSERT INTO Movies(Title, DirectorId, CopyrightYear, Lenght, GenreId, CategoryId,Rating,Notes)
VALUES ('Predestitination', 1, 2017, 220, 1, 2, 7, NULL),
('Rocky Balboa', 1, 2017, 220, 1, 2, 7, NULL),
('Wrong Turn', 1, 2017, 220, 1, 2, 7, NULL),
('Djihad maika', 1, 2017, 220, 1, 2, 7, NULL),
('Lucifer', 1, 2016, 220, 1, 2, 7, NULL);

14. Car Rental Database

CREATE TABLE Categories
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
Category varchar(50) NOT NULL,
DailyRate decimal(5,2),
WeeklyRate decimal(5,2),
MonthlyRate decimal(5,2),
WeekendRate decimal(5,2)
)
CREATE TABLE Cars 
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
PlateNumber int NOT NULL,
Make varchar(50),
CarYear int NOT NULL,
CategoryId int,
Doors int NOT NULL,
Picture varbinary(max),
Condition varchar(255),
Available bit
)

CREATE TABLE Employees
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
FirstName nvarchar(50) NOT NULL,
LastName nvarchar(50) NOT NULL,
Title varchar(25),
Notes nvarchar(max)
)

CREATE TABLE Customers
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
DriverLicenseNumber varchar(15) NOT NULL,
FullName nvarchar(50) NOT NULL ,
Adress varchar(50),
City varchar(30),
ZIPCode int,
Notes nvarchar(max),
)
CREATE TABLE RentalOrders
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
EmployeeId int,
CustomerId int,
CarId int,
CarCondition nvarchar(255),
TankLevel int ,
KilometrageStart decimal(8,2),
KilometrageEnd decimal(8,2),
TotalKilometrage decimal(10,2),
StartDate date,
EndDate date,
TotalDays int ,
RateApplied int,
TaxRate decimal(5,2),
OrderStatus bit,
Notes varchar(50)
)

INSERT INTO RentalOrders(EmployeeId, CustomerId, CarId,CarCondition,TankLevel, KilometrageStart, KilometrageEnd, TotalKilometrage,StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, Notes)
VALUES (1, 2, 3,'Good', NULL, 50000, 200000, 150000, '20121202','20121207', 5, NULL, NULL, NULL, NULL ), 
(1, 2, 3,'Good', NULL, 50000, 200000, 150000, '20121202','20121207', 5, NULL, NULL, NULL, NULL ), 
(1, 2, 3,'Good', NULL, 50000, 200000, 150000, '20121202','20121207', 5, NULL, NULL, NULL, NULL );

INSERT INTO Categories(Category, DailyRate, WeeklyRate,MonthlyRate, WeekendRate)
VALUES ('Fast', 10, 50, 100, 5),
('Slow', 35, 80, 100, 1),
('Middle', 5, 80, 20, 6);

INSERT INTO Customers(DriverLicenseNumber, FullName, Adress,ZIPCode,Notes)
VALUES ('123ABS31', 'Bogdan Alov', 'Maritza 18',1113, NULL),
('123NC231', 'Stanislav Ivanov', 'Bond Street 69',4000, 'A pipe'),
('4567SA321', 'Angel Alov', 'Maritza 18', 1113 , NULL);

INSERT INTO Employees(FirstName, LastName, Title,Notes)
VALUES ('Bogdan', 'Alov', 'Senior','Как е?'),
('Stanislav', 'Nedialkov', 'Junior', 'Im not good at League of Legends'),
('Hasan', 'Hasanov', 'Junior','Hi.');

INSERT INTO Cars(PlateNumber, Make, CarYear,CategoryId, Doors, Picture, Condition, Available)
VALUES (1235678, NULL, 1997, NULL,4, NULL, 'Very good', 1),
(0886213099, 'VW', 2012, NULL,4, NULL, 'Good', 1),
(5780, NULL, 1997, NULL,2, NULL, 'Excellent', 0);


15. Hotel Database

CREATE TABLE Employees
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
FirstName nvarchar(50) NOT NULL,
LastName nvarchar(50) NOT NULL,
Title varchar(25),
Notes nvarchar(max)
)
CREATE TABLE Customers
(
Id int NOT NULL Identity(1,1) PRIMARY KEY,
AccountNumber varchar(15) NOT NULL,
FirstName nvarchar(20) NOT NULL ,
LastName nvarchar(20) NOT NULL ,
PhoneNumber varchar(50),
EmergencyName varchar(50),
EmergencyNumber int,
Notes nvarchar(max),
)
CREATE TABLE RoomStatus
(
RoomStatus varchar(15) NOT NULL,
Notes nvarchar(max)
)
CREATE TABLE RoomTypes
(
RoomType varchar(15) NOT NULL,
Notes nvarchar(max)
)

CREATE TABLE BedTypes
(
BedType varchar(15) NOT NULL,
Notes nvarchar(max)
)
CREATE TABLE Rooms
(
RoomNumber int Identity(1,10) PRIMARY KEY,
RoomType varchar(15) NOT NULL,
BedType varchar(15) NOT NULL,
Rate decimal (4,2),
RoomStatus varchar(15) NOT NULL,
Notes nvarchar(max)
)
CREATE TABLE Payments
(
Id int Identity(1,10) PRIMARY KEY,
EmployeeId int,
PaymentDate date,
AccountNumber varchar(15),
FirstDateOccupied date,
LastDateOccupied date,
TotalDays int,
AmountCharged money,
TaxRate decimal(5,2),
TaxAmount decimal(5,2),
PaymentTotal decimal(10,2),
Notes nvarchar(max)
)

CREATE TABLE Occupancies
(
Id int Identity(1,10) PRIMARY KEY,
EmployeeId int,
DateOccupied date,
AccountNumber varchar(15),
RoomNumber int,
RateApplied decimal(5,2),
PhoneCharge bit,
Notes nvarchar(max)
)

INSERT INTO Employees(FirstName,LastName,Title, Notes)
VALUES ('Bogdan', 'Alov', 'Senior', NULL),
('Bogdan', 'Alov', 'Senior', NULL),
('Bogdan', 'Alov', 'Senior', NULL);

INSERT INTO Customers(AccountNumber, FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber, Notes)
VALUES ('BN21325GB', 'Bogdan', 'Alov', NULL, 'SOS', 112, NULL),
('BN21325GB', 'Bogdan', 'Alov', NULL, 'SOS', 112, NULL),
('BN21325GB', 'Bogdan', 'Alov', NULL, 'SOS', 112, NULL);

INSERT INTO RoomStatus(RoomStatus, Notes)
VALUES ('Free', NULL),
('Free', NULL),
('Free', NULL);

INSERT INTO BedTypes(BedType, Notes)
VALUES ('for Two', NULL),
('For One', NULL),
('For One', NULL);

INSERT INTO RoomTypes(RoomType, Notes)
VALUES ('for Two', NULL),
('For One', NULL),
('For THREE', NULL);

INSERT INTO Rooms(RoomType, BedType, Rate, RoomStatus, Notes)
VALUES ('Dual','Oval', 10, 'Free', NULL),
('for Two','For Two', 10, 'Free', NULL),
('for Two','For Two', 10, 'Free', NULL);

INSERT INTO Payments(EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, AmountCharged, TaxRate, TaxAmount, PaymentTotal, Notes)
VALUES (1, '20170110', 'BNCSAS213', '20160106', '20170110', 112, 1400, 10, 300, 22222, NULL ),
(1, '20170110', 'BNCSAS213', '20160106', '20170110', 112, 1400, 10, 300, 22222, NULL ),
(1, '20170110', 'BNCSAS213', '20160106', '20170110', 112, 1400, 10, 300, 22222, NULL );

INSERT INTO Occupancies(EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge, Notes)
VALUES (1, '20170107', 'HeyS412', 101, NULL, NULL, NULL),
(1, '20170107', 'HeyS412', 101, NULL, NULL, NULL),
(1, '20170107', 'HeyS412', 101, NULL, NULL, NULL);



19. Basic Select All Fields

SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

20. Basic Select All Fields and Order Them

SELECT * FROM Towns ORDER BY Name
SELECT * FROM Departments ORDER BY Name
SELECT * FROM Employees ORDER BY Salary DESC

21. Basic Select Some Fields

SELECT [Name]
  FROM Towns ORDER BY Name

SELECT [Name]
  FROM Departments ORDER BY Name

SELECT [FirstName],
       [LastName], 
	   [JobTitle],
	   [Salary]
  FROM Employees ORDER BY Salary DESC


22. Increase Employees Salary

UPDATE Employees
SET Salary += Salary / 10;
SELECT [Salary]
  FROM Employees

23. Decrease Tax Rate

UPDATE Payments
SET TaxRate =TaxRate*0.97;
SELECT [TaxRate]
FROM Payments

24. Delete All Records

TRUNCATE TABLE Occupancies



