-- Section 1: DDL

CREATE TABLE Deposits
(
DepositID int PRIMARY KEY IDENTITY,
Amount decimal(10,2),
StartDate date,
EndDate date,
DepositTypeID int,
CustomerID int
)

CREATE TABLE DepositTypes
(
DepositTypeID int PRIMARY KEY ,
Name varchar(20) 
)

CREATE TABLE EmployeesDeposits
(
EmployeeID int,
DepositID int,
CONSTRAINT PK_EmplooyeesDeposits PRIMARY KEY(EmployeeID, DepositID)
)

CREATE TABLE CreditHistory
(
CreditHistoryID int PRIMARY KEY ,
Mark char(1),
StartDate date,
EndDate date,
CustomerID int
)

CREATE TABLE Payments
(
PaymentID int PRIMARY KEY ,
Date date,
Amount decimal(10,2),
LoanID int
)

CREATE TABLE Users
(
UserID int PRIMARY KEY,
UserName varchar(20),
Password varchar(20),
CustomerID int
)

ALTER TABLE Employees
ADD ManagerID int 

ALTER TABLE Employees
ADD CONSTRAINT FK_ManagerID_EmployeeID
FOREIGN KEY(ManagerID) REFERENCES Employees(EmployeeID)

ALTER TABLE Deposits
ADD CONSTRAINT FK_Deposits_DepositTypes
FOREIGN KEY(DepositTypeID) REFERENCES DepositTypes(DepositTypeID)

ALTER TABLE Deposits
ADD CONSTRAINT FK_Deposits_Customers
FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)

ALTER TABLE EmployeesDeposits
ADD CONSTRAINT FK_EmployeesDeposits_Employees
FOREIGN KEY(EmployeeID) REFERENCES Employees(EmployeeID)

ALTER TABLE EmployeesDeposits
ADD CONSTRAINT FK_EmployeesDeposits_Deposits
FOREIGN KEY(DepositID) REFERENCES Deposits(DepositID)

ALTER TABLE CreditHistory
ADD CONSTRAINT FK_CreditHistory_Customers
FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)

ALTER TABLE Payments
ADD CONSTRAINT FK_Payments_Loans
FOREIGN KEY(LoanID) REFERENCES Loans(LoanID)

ALTER TABLE Users
ADD CONSTRAINT FK_Users_Customers
FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID)

-- Section 2: DML - P01. Inserts

ALTER TABLE EmployeesDeposits
DROP CONSTRAINT FK_EmployeesDeposits_DepositID

INSERT INTO EmployeesDeposits (EmployeeID, DepositID)
VALUES	(15,4),
		(20,15),
		(8,7),
		(4,8),
		(3,13),
		(3,8),
		(4,10),
		(10,1),
		(13,4),
		(14,9)
INSERT INTO DepositTypes(DepositTypeID, Name)
VALUES	(1, 'Time Deposit'),
		(2, 'Call Deposit'),
		(3, 'Free Deposit')

INSERT INTO Deposits(Amount, StartDate, EndDate, DepositTypeID, CustomerID)
SELECT  
CASE
WHEN c.DateOfBirth > '1980-01-01' AND c.Gender = 'M' THEN 1100
WHEN  c.DateOfBirth < '1980-01-01' AND c.Gender = 'M' THEN 1600
WHEN c.DateOfBirth > '1980-01-01' AND c.Gender = 'F' THEN 1200
else 1700
END as Amount, 
CONVERT(date, getdate()),
NULL,
CASE
when c.CustomerID > 15 then 3
when c.CustomerID % 2 = 0 THEN 2 
when c.CustomerID % 2 != 0 THEN 1 
end,
c.CustomerID
FROM Customers c
WHERE c.CustomerID < 20

ALTER TABLE EmployeesDeposits
ADD CONSTRAINT FK_EmployeesDeposits_DepositID
FOREIGN KEY (DepositID) REFERENCES Deposits(DepositID)

-- Section 2: DML - P02. Update

UPDATE Employees
set ManagerID = 1
WHERE EmployeeID BETWEEN 2 AND 10

UPDATE Employees
set ManagerID = 11
WHERE EmployeeID BETWEEN 12 AND 20

UPDATE Employees
set ManagerID = 21
WHERE EmployeeID BETWEEN 22 AND 30

UPDATE Employees
set ManagerID = 1
WHERE EmployeeID IN (11,21)

-- Section 2: DML - P03. Delete

DELETE FROM EmployeesDeposits
WHERE DepositID = 9 OR EmployeeID = 3

-- Section 3: Querying - P01. Employees’ Salary

SELECT EmployeeID, HireDate, Salary, BranchID FROM Employees
where Salary > 2000 AND HireDate > '2009-06-15'

-- Section 3: Querying - P02. Customer Age

SELECT FirstName, DateOfBirth, DATEDIFF(YEAR, DateOfBirth, '2016-01-10') as Age FROM Customers
WHERE DATEDIFF(YEAR, DateOfBirth, '2016-01-10') between 40 and 50

-- Section 3: Querying - P03. Customer City

SELECT CustomerID, FirstName, LastName, Gender, c.CityName FROM Customers cs
left join Cities c
on c.CityID = cs.CityID
WHERE (LastName LIKE 'Bu%' OR FirstName LIKE '%a' ) AND LEN(CityName) > 7
ORDER BY CustomerID

-- Section 3: Querying - P04. Employee Accounts

select top 5 e.EmployeeID, e.FirstName, a.AccountNumber from Employees e
join EmployeesAccounts ea
on ea.EmployeeID = e.EmployeeID
join Accounts a
on a.AccountID = ea.AccountID
WHERE YEAR(a.StartDate) > 2012
ORDER BY FirstName DESC

-- Section 3: Querying - P05. Count Cities

SELECT c.CityName, br.Name, COUNT(e.EmployeeID) as EmployeesCount FROM Cities c
JOIN Branches br
on c.CityID = br.CityID
JOIN Employees e
on e.BranchID = br.BranchID
WHERE c.CityID NOT IN (4,5)
GROUP BY c.CityName, br.Name
HAVING COUNT(e.EmployeeID) > 2

-- Section 3: Querying - P06. Loan Statistics

SELECT SUM(Amount) as TotalLoanAmount, MAX(Interest) as MaxInterest, MIN(e.Salary) as MinEmployeeSalary from Loans l
join EmployeesLoans el
on el.LoanID = l.LoanID
join Employees e
on e.EmployeeID = el.EmployeeID

-- Section 3: Querying - P07. Unite People

SELECT TOP 3 FirstName, CityName FROM Employees e
join Branches br
on br.BranchID = e.BranchID
join Cities c
on br.CityID = c.CityID

SELECT TOP 3 FirstName, c.CityName From Customers cs
join Cities c
on c.CityID = cs.CityID


-- Section 3: Querying - P08. Customers w/o Accounts

select cs.CustomerID, cs.Height from Customers cs
left join Accounts a
on a.CustomerID = cs.CustomerID
WHERE (Height BETWEEN 1.74 AND 2.04 ) AND AccountID IS NULL

-- Section 3: Querying - P09. Average Loans

SELECT top 5  cs.CustomerID, l.Amount FROM Customers cs
left join Loans l
on l.CustomerID = cs.CustomerID
WHERE l.Amount > (SELECT AVG(l.Amount) FROM Customers cs
left join Loans l
on l.CustomerID = cs.CustomerID
WHERE Gender = 'M')
ORDER BY cs.LastName 


-- Section 3: Querying - P10. Oldest Account

SELECT TOP 1 cs.CustomerID, cs.FirstName, a.StartDate from Customers cs
join Accounts a
on a.CustomerID = cs.CustomerID
ORDER BY a.StartDate

-- Section 4: Programmability - P01. String Joiner

create function udf_ConcatString (@firstString varchar(max), @secondString varchar(max))
returns varchar(max)
as
begin
declare @result varchar(max) = Concat(REVERSE(@firstString),REVERSE(@secondString))
return @result
end

-- Section 4: Programmability - P02. Inexpired Loans

create procedure usp_CustomersWithUnexpiredLoans(@CustomerID int)
as
begin

Select c.CustomerID, FirstName, LoanID FROM Customers c
left join Loans l
on l.CustomerID = c.CustomerID
where ExpirationDate IS  NULL AND l.CustomerID = @CustomerID


end


-- Section 4: Programmability - P03. Take Loan

create proc usp_TakeLoan (@CustomerID int, @LoanAmount decimal(18,2), @Interest decimal (4,2), @StartDate date )
as
begin tran
if(@LoanAmount NOT BETWEEN 0.01 AND 100000)
begin 
rollback
raiserror('Invalid Loan Amount.',16,1)
return
end
INSERT INTO Loans(CustomerID, Amount, StartDate, Interest)
VALUES (@CustomerID, @LoanAmount, @StartDate, @Interest)
Commit

-- Section 4: Programmability - P04. Hire Employee

CREATE TRIGGER tr_EmployeesLoans ON Employees AFTER INSERT
as
begin
declare @EmployeeID int = (Select EmployeeID from inserted)
UPDATE EmployeesLoans
SET EmployeeID = @EmployeeID
WHERE EmployeeID IN (SELECT TOP 1 EmployeeID FROM EmployeesLoans
ORDER BY EmployeeID DESC)
end

-- Section 5: Bonus - P01. Delete Trigger

CREATE TRIGGER tr_LogDeletedAccounts
ON Accounts
INSTEAD OF DELETE
AS
BEGIN
 
    ALTER TABLE EmployeesAccounts
    DROP CONSTRAINT FK_EmployeesAccounts_Account
 
    ALTER TABLE EmployeesAccounts
    ADD CONSTRAINT FK_EmployeesAccounts_Account
    FOREIGN KEY(AccountID)
    REFERENCES Accounts(AccountID) ON DELETE CASCADE
 
    DELETE FROM Accounts
    WHERE AccountID = (SELECT AccountID FROM deleted)
 
    INSERT INTO AccountLogs
    SELECT * FROM deleted
 
END
