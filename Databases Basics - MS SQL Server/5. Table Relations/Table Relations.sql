01. One-To-One Relationship

CREATE TABLE Passports
(
PassportID INT NOT NULL,
PassportNumber nvarchar(20) NOT NULL,
)

CREATE TABLE Persons
(
PersonID INT NOT NULL,
FirstName nvarchar(20) NOT NULL,
Salary DECIMAL(10,2),
PassportID INT NOT NULL
)

INSERT INTO Passports(PassportId, PassportNumber)
VALUES (101, 'N34FG21B'),
(102, 'K65LO4R7'),
(103, 'ZE657QP2')

INSERT INTO Persons(PersonId, FirstName, Salary, PassportId)
VALUES (1, 'Roberto', 43300, 102),
(2, 'Tom', 56100, 103),
(3, 'Yana', 60200, 101)

ALTER TABLE Passports
ADD PRIMARY KEY(PassportId)

ALTER TABLE Persons
ADD PRIMARY KEY(PersonId)

ALTER TABLE Persons
ADD CONSTRAINT FK_PassportId
FOREIGN KEY (PassportId) REFERENCES Passports(PassportId)

02. One-To-Many Relationship

CREATE TABLE Manufacturers(
ManufacturerID INT NOT NULL ,
Name varchar(20) NOT NULL,
EstablishedOn  date
)

CREATE TABLE Models(
ModelID INT NOT NULL,
Name nvarchar(20) NOT NULL,
ManufacturerID int NOT NULL
)

INSERT INTO Manufacturers(ManufacturerID, Name, EstablishedOn)
VALUES(1, 'BMW', '19160307'),
(2, 'Tesla', '20030101'),
(3, 'Lada', '19660501')


INSERT INTO Models(ModelID, Name ,ManufacturerID)
VALUES(101, 'X1', 1),
(102, 'i6', 1),
(103, 'Model S', 2),
(104, 'Model X', 2),
(105, 'Model 3', 2),
(106, 'Nova', 3)

ALTER TABLE Manufacturers
ADD PRIMARY KEY(ManufacturerID)

ALTER TABLE Models
ADD PRIMARY KEY(ModelID)

ALTER TABLE Models
ADD CONSTRAINT FK_ManufacturerID
FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers(ManufacturerID)

03. Many-To-Many Relationship

CREATE TABLE Students(
StudentID INT NOT NULL ,
Name varchar(20) NOT NULL,
)

CREATE TABLE Exams(
ExamID INT NOT NULL ,
Name varchar(20) NOT NULL,
)

CREATE TABLE StudentsExams(
StudentID INT NOT NULL,
ExamID INT NOT NULL,
)

INSERT INTO Students(StudentID, Name)
VALUES(1, 'Mila'),
(2, 'Toni'),
(3, 'Ron')


INSERT INTO Exams(ExamID, Name)
VALUES(101, 'SpringMVC'),
(102, 'Neo4j'),
(103, 'Oracle 11g')

INSERT INTO StudentsExams(StudentID, ExamID)
VALUES(1, 101),
(1, 102),
(2, 101),
(3, 103),
(2, 102),
(2, 103)
ALTER TABLE Students
ADD PRIMARY KEY(StudentID)

ALTER TABLE Exams
ADD PRIMARY KEY(ExamID)

ALTER TABLE StudentsExams
ADD PRIMARY KEY(StudentID, ExamID)

ALTER TABLE StudentsExams
ADD CONSTRAINT FK_StudentID
FOREIGN KEY (StudentID) REFERENCES Students(StudentID)

ALTER TABLE StudentsExams
ADD CONSTRAINT FK_ExamID
FOREIGN KEY (ExamID) REFERENCES Exams(ExamID)

04. Self-Referencing

CREATE TABLE Teachers
(
TeacherID INT IDENTITY(101,1) NOT NULL PRIMARY KEY,
Name nvarchar(25) NOT NULL,
ManagerID int
CONSTRAINT FK_ManagerID
FOREIGN KEY(ManagerID)
REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers(Name, ManagerID)
VALUES('John', NULL),
('Maya', 106),
('Silvia', 106),
('Ted', 105),
('Mark', 101),
('Greta', 101)

05. Online Store Database

CREATE TABLE Orders
(
OrderID int NOT NULL,
CustomerID int NOT NULL
)

CREATE TABLE Customers
(
CustomerID int NOT NULL,
Name varchar(50) NOT NULL,
Birthday date NOT NULL,
CityID int NOT NULL
)
CREATE TABLE Cities
(
CityID int NOT NULL,
Name varchar(50) NOT NULL
)

CREATE TABLE OrderItems
(
OrderID int NOT NULL,
ItemID int NOT NULL,
)

CREATE TABLE Items
(
ItemID int NOT NULL,
Name varchar(50),
ItemTypeID INT not null
)

CREATE TABLE ItemTypes
(
ItemTypeID int NOT NULL,
Name varchar(50)
)

ALTER TABLE Orders
ADD PRIMARY KEY(OrderID)
ALTER TABLE Customers
ADD PRIMARY KEY(CustomerID)
ALTER TABLE Cities
ADD PRIMARY KEY(CityID)
ALTER TABLE OrderItems
ADD PRIMARY KEY(OrderID, ItemID)
ALTER TABLE Items
ADD PRIMARY KEY(ItemID)
ALTER TABLE ItemTypes
ADD PRIMARY KEY(ItemTypeID)

ALTER TABLE Orders
ADD CONSTRAINT FK_CustomerID
FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)

ALTER TABLE Customers
ADD CONSTRAINT FK_CityID
FOREIGN KEY(CityID) REFERENCES Cities(CityID)

ALTER TABLE OrderItems
ADD CONSTRAINT FK_OrderID
FOREIGN KEY(OrderID) REFERENCES Orders(OrderID)
ALTER TABLE OrderItems
ADD CONSTRAINT FK_ItemID
FOREIGN KEY(ItemID) REFERENCES Items(ItemID)
ALTER TABLE Items
ADD CONSTRAINT FK_ItemTypeID
FOREIGN KEY(ItemTypeID) REFERENCES ItemTypes(ItemTypeID)

06. University Database

CREATE TABLE Majors
(
MajorID int NOT NULL,
Name varchar(50) NOT NULL
)

CREATE TABLE Students
(
StudentID int NOT NULL,
StudentNumber varchar(25) NOT NULL,
StudentName varchar(50) NOT NULL,
MajorID int NOT NULL
)
CREATE TABLE Payments
(
PaymentID int NOT NULL,
PaymentDate date NOT NULL,
PaymentAmount DECIMAL(10,2) NOT NULL,
StudentID int not null
)

CREATE TABLE Agenda
(
StudentID int NOT NULL,
SubjectID int NOT NULL,
)

CREATE TABLE Subjects
(
SubjectID int NOT NULL,
SubjectName varchar(50),
)

ALTER TABLE Majors
ADD PRIMARY KEY(MajorID)
ALTER TABLE Students
ADD PRIMARY KEY(StudentID)
ALTER TABLE Payments
ADD PRIMARY KEY(PaymentID)
ALTER TABLE Agenda
ADD PRIMARY KEY(StudentID, SubjectID)
ALTER TABLE Subjects
ADD PRIMARY KEY(SubjectID)
ALTER TABLE Students
ADD CONSTRAINT FK_MajorID
FOREIGN KEY(MajorID) REFERENCES Majors(MajorID)

ALTER TABLE Payments
ADD CONSTRAINT FK_PaymentStudentID
FOREIGN KEY(StudentID) REFERENCES Students(StudentID)

ALTER TABLE Agenda
ADD CONSTRAINT FK_StudentID
FOREIGN KEY(StudentID) REFERENCES Students(StudentID)
ALTER TABLE Agenda
ADD CONSTRAINT FK_SubjectID
FOREIGN KEY(SubjectID) REFERENCES Subjects(SubjectID)

09. *Peaks in Rila

SELECT Mountains.MountainRange, Peaks.PeakName, Elevation
FROM Peaks
FULL OUTER JOIN Mountains
ON Peaks.MountainId = Mountains.Id
WHERE Mountains.MountainRange = 'Rila'
ORDER BY Elevation DESC
