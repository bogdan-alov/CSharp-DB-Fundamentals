01. Employees with Salary Above 35000

CREATE PROC usp_GetEmployeesSalaryAbove35000
as
SELECT FirstName, LastName FROM Employees
WHERE Salary > 35000

02. Employees with Salary Above Number

CREATE PROC usp_GetEmployeesSalaryAboveNumber  (@Salary money)
as
SELECT FirstName, LastName FROM Employees
WHERE Salary >= @Salary

03. Town Names Starting With

CREATE PROC usp_GetTownsStartingWith  (@Text varchar(50))
as
SELECT t.Name FROM Towns t
WHERE t.Name LIKE @Text + '%'

04. Employees from Town

CREATE PROC usp_GetEmployeesFromTown  (@Town varchar(50))
as
SELECT e.FirstName, e.LastName FROM Employees e 
JOIN Addresses a
ON e.AddressID = a.AddressID
JOIN Towns t
ON a.TownID = t.TownID
WHERE @Town = t.Name

05. Salary Level Function

CREATE FUNCTION ufn_GetSalaryLevel(@Salary MONEY)
returns varchar(50)
begin

if(@Salary < 30000)
begin 

return 'Low'
end

if(@Salary > 50000)
begin 

return 'High'
end
return 'Average'

end

06. Employees by Salary Level

CREATE PROC usp_EmployeesBySalaryLevel  (@Level varchar(50))
as

if(@Level = 'low')
BEGIN
SELECT FirstName, LastName FROM Employees
WHERE Salary < 30000
end

if(@Level = 'high')
BEGIN
SELECT FirstName, LastName FROM Employees
WHERE Salary > 50000
end
if(@Level = 'average')
BEGIN
SELECT FirstName, LastName FROM Employees
WHERE Salary > 30000 AND Salary < 50000
end

07. Define Function

CREATE FUNCTION ufn_IsWordComprised(@setOfLetters varchar(max), @word	varchar(max))
returns bit 
as
begin
declare @lenght int = len(@word)
declare @index int = 1;
while (@index <= @lenght)
begin
declare @letter char(1) = SUBSTRING(@word, @index, 1)
if(CHARINDEX(@letter, @setOfLetters) <= 0)
begin
return 0
end
SET @index = @index + 1
end
return 1 
end

08. Delete Employees and Departments

alter table Departments  --smenqme id to na managerite da moje da bude null
alter column ManagerId int null

select e.EmployeeID from Employees as e  
inner join Departments as d on d.DepartmentID = e.DepartmentID
where d.Name in ('Production', 'Production Control')

delete from EmployeesProjects
where EmployeeID in (
					select e.EmployeeID from Employees as e  
					inner join Departments as d on d.DepartmentID = e.DepartmentID
					where d.Name in ('Production', 'Production Control')
					)

update Employees  --updatevame tablicata s menidjari da budat null
set ManagerID = null
where ManagerID in (
				select e.EmployeeID from Employees as e  
				inner join Departments as d on d.DepartmentID = e.DepartmentID
				where d.Name in ('Production', 'Production Control')
)

update Departments  -- promqna na managerite v departamentite s NULL
set ManagerID = null
where ManagerID in(
				select e.EmployeeID from Employees as e  
				inner join Departments as d on d.DepartmentID = e.DepartmentID
				where d.Name in ('Production', 'Production Control')
)

--select * from Departments
delete from Employees  --iztriti totalno veche stapka po stapka
where EmployeeID in (
				select e.EmployeeID from Employees as e  
				inner join Departments as d on d.DepartmentID = e.DepartmentID
				where d.Name in ('Production', 'Production Control')
)

delete from Departments  --iztrivane departamentite
where name in ('Production', 'Production Control');

09. Employees with Three Projects

create proc usp_AssignProject(@employeeId int, @projectId int)
as
BEGIN TRANSACTION 
INSERT INTO EmployeesProjects(EmployeeID, ProjectID)
VALUES (@employeeId, @projectId)
IF(
(SELECT COUNT(ProjectID) as Projects FROM EmployeesProjects
WHERE EmployeeID = @employeeId
GROUP BY EmployeeID) > 3 )
BEGIN 
ROLLBACK
RAISERROR('The employee has too many projects!', 16,1)
RETURN
end
COMMIT

10. Find Full Name

CREATE PROC usp_GetHoldersFullName
as
SELECT FirstName + ' '  + LastName as [Full Name] from AccountHolders
 
11. People with Balance Higher Than

 CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan (@suppliedNumber MONEY)
AS
SELECT ah.FirstName, ah.LastName
FROM [dbo].[AccountHolders] AS ah
JOIN [dbo].[Accounts] AS a
ON ah.Id = a.AccountHolderId
GROUP BY ah.FirstName, ah.LastName
HAVING SUM(a.Balance) > @suppliedNumber

12. Future Value Function

  CREATE FUNCTION ufn_CalculateFutureValue(@P money, @R float, @T int)
  RETURNS decimal(15,4)
  as
  begin
  DECLARE @N int = 1;
  DECLARE @A decimal(15,4) = @P * POWER((1 +( @R / @N)), @T)
  RETURN @A
  end

13. Calculating Interest

create PROC usp_CalculateFutureValueForAccount(@accountID int, @interestRate float)
as
begin
SELECT TOP(1) ah.Id as 'Account Id', ah.FirstName as 'First Name', ah.LastName as 'Last Name', u.Balance as 'Current Balance', dbo.ufn_CalculateFutureValue(u.Balance, @interestRate, 5) as 'Balance in 5 years'   from AccountHolders ah
 LEFT JOIN Accounts u
 ON u.AccountHolderId = ah.Id
 where @accountID = u.AccountHolderId
 end

 14. Deposit Money Procedure

 CREATE PROC usp_DepositMoney(@AccountId int, @moneyAmount money)
as
BEGIN TRANSACTION
UPDATE Accounts 
SET Balance += @moneyAmount
WHERE  Id = @AccountId
IF @@ROWCOUNT <> 1 
BEGIN 
	ROLLBACK
	RAISERROR('Invalid account!', 16, 1)
	RETURN
end
COMMIT

15. Withdraw Money Procedure

CREATE PROC usp_WithdrawMoney(@AccountId int, @moneyAmount money)
as
BEGIN TRANSACTION
UPDATE Accounts 
SET Balance -= @moneyAmount
WHERE  Id = @AccountId
IF @@ROWCOUNT <> 1 
BEGIN 
	ROLLBACK
	RAISERROR('Invalid account!', 16, 1)
	RETURN
end
COMMIT

16. Money Transfer

CREATE PROC usp_TransferMoney(@senderId int , @receiverId int, @amount money)
as

BEGIN TRANSACTION
UPDATE Accounts
SET Balance -= @amount
WHERE Id = @senderId

IF (@amount < 0)
BEGIN
ROLLBACK
RAISERROR('The amount couldnt be negative number!', 16,1)
RETURN
end
UPDATE Accounts
SET Balance += @amount
where Id = @receiverId
IF @@ROWCOUNT <> 1 
BEGIN 
	ROLLBACK
	RAISERROR('Invalid account!', 16, 1)
	RETURN
end

COMMIT

17. Create Table Logs

CREATE TRIGGER tr_Logs ON Accounts AFTER UPDATE
as
begin
INSERT INTO Logs(AccountId, OldSum, NewSum)
SELECT i.Id, d.Balance, i.Balance FROM inserted i
join deleted d
on d.Id = i.Id
end


18. Create Table Emails

CREATE TRIGGER tr_LogToEmail ON Logs AFTER INSERT
as
begin
INSERT INTO NotificationEmails(Recipient, Subject, Body)
SELECT AccountId,'Balance change for account: ' + CONVERT(varchar(10), AccountId ), 'On ' + CONVERT(varchar(30), GETDATE()) + ' your balance was changed from ' + CONVERT(varchar(30), OldSum ) + ' to ' + CONVERT(varchar(30), NewSum ) from Logs 
end

19. *Cash in User Games Odd Rows

CREATE FUNCTION ufn_CashInUsersGames(@gameName nvarchar(50))
returns @Result table 
(
SumCash money
)
as 
begin 
INSERT INTO @Result
SELECT SUM(sc.Cash) as SumCash
	FROM
		(SELECT Cash,
		ROW_NUMBER() OVER(ORDER BY Cash DESC) AS RowNumber
		FROM UsersGames ug
		RIGHT JOIN Games g
		ON ug.GameId = g.Id
		WHERE g.Name = @gameName) sc
	WHERE RowNumber % 2 != 0
	RETURN
end

22. Number of Users for Email Provider

SELECT	
	RTRIM(LTRIM(SUBSTRING(Email,CHARINDEX('@',Email,1)+1,LEN(Email)))) AS 'Email Provider', COUNT(RTRIM(LTRIM(SUBSTRING(Email,CHARINDEX('@',Email,1)+1,LEN(Email))))) as 'Number Of Users'
	FROM Users
GROUP BY RTRIM(LTRIM(SUBSTRING(Email,CHARINDEX('@',Email,1)+1,LEN(Email))))
ORDER BY [Number Of Users] DESC, [Email Provider]

23. All Users in Games

SELECT g.Name as Game, gt.Name as 'Game Type', u.Username, ug.Level, ug.Cash, c.Name as Character from Games g
JOIN  GameTypes gt
on g.GameTypeId = gt.Id
JOIN  UsersGames ug
ON ug.GameId = g.Id
JOIN  Users u
ON u.Id = ug.UserId
JOIN  Characters c
on c.Id = ug.CharacterId
ORDER BY Level DESC, Username, g.Name

24. Users in Games with Their Items

select u.Username, g.Name as Game, COUNT(i.Id) as 'Items Count', SUM(i.Price) as 'Items Price' from Users u
join UsersGames ug
on ug.UserId = u.Id
join Games g
on g.Id = ug.GameId
join UserGameItems ugi
on ugi.UserGameId = ug.Id
join Items i
on i.Id = ugi.ItemId
GROUP BY u.Username, g.Name
HAVING COUNT(i.Id) >= 10
ORDER BY [Items Count] desc , [Items Price] desc, u.Username
--WHERE u.Username = 'skippingside' and g.Name = 'Rose Fire & Ice'

25. * User in Games with Their Statistics

SELECT u.Username, g.Name AS Game, MAX(c.Name) AS Character,
SUM(its.Strength) + MAX(gts.Strength) + MAX(cs.Strength) AS Strength,
SUM(its.Defence) + MAX(gts.Defence) + MAX(cs.Defence) AS Defence,
SUM(its.Speed) + MAX(gts.Speed) + MAX(cs.Speed) as Speed,
SUM(its.Mind) + MAX(gts.Mind) + MAX(cs.Mind) AS Mind,
SUM(its.Luck) + MAX(gts.Luck) + MAX(cs.Luck) AS Luck
FROM Users u
JOIN UsersGames ug
ON u.Id = ug.UserId
JOIN Games g
ON ug.GameId = g.Id
JOIN GameTypes gt
ON gt.Id = g.GameTypeId
JOIN [dbo].[Statistics] gts
ON gts.Id = gt.BonusStatsId
JOIN Characters c
ON ug.CharacterId = c.Id
JOIN [dbo].[Statistics] cs
ON cs.Id = c.StatisticId
JOIN UserGameItems ugi
ON ugi.UserGameId = ug.Id
JOIN Items i
ON i.Id = ugi.ItemId
JOIN [dbo].[Statistics] its
ON its.Id = i.StatisticId
GROUP BY u.Username, g.Name
ORDER BY Strength DESC, Defence DESC, Speed DESC, Mind DESC, Luck DESC

26. All Items with Greater than Average Statistics

SELECT i.Name, i.Price, i.MinLevel, s.Strength, s.Defence, s.Speed, s.Luck, s.Mind FROM Items i
join [dbo].[Statistics] s
on s.Id = i.StatisticId
WHERE s.Speed > (SELECT avg(AvgGroup) as AverageSpeed FROM (SELECT  AVG(s.Speed) as AvgGroup FROM Items i
join [dbo].[Statistics] s
on s.Id = i.StatisticId
group by i.Name) e) and s.Luck > (SELECT avg(AvgGroup) as AverageLuck FROM (SELECT  AVG(s.Luck) as AvgGroup FROM Items i
join [dbo].[Statistics] s
on s.Id = i.StatisticId
group by i.Name) e
) and s.Mind > (SELECT avg(AvgGroup) as AverageMind FROM (SELECT  AVG(s.Mind) as AvgGroup FROM Items i
join [dbo].[Statistics] s
on s.Id = i.StatisticId
group by i.Name) e
)
ORDER BY i.Name

27. Display All Items about Forbidden Game Type

SELECT i.Name, i.Price, i.MinLevel, gt.Name as 'Forbidden Game Type' FROM Items i
LEFT JOIN GameTypeForbiddenItems gtfi
on gtfi.ItemId = i.Id
left JOIN GameTypes gt
on gt.Id = gtfi.GameTypeId
ORDER BY gt.Name DESC, i.Name

28. Buy Items for User in Game

DECLARE @userId INT =
(
    SELECT ug.Id
      FROM Users AS u
     INNER JOIN UsersGames AS ug
        ON u.Id = ug.UserId
     INNER JOIN Games AS g
        ON g.Id = ug.GameId
     WHERE u.Username = 'Alex' AND g.Name = 'Edinburgh'
)
 
INSERT INTO UserGameItems (ItemId, UserGameId)
SELECT i.Id, @userId
  FROM Items AS i
 WHERE i.Name IN ('Blackguard', 'Bottomless Potion of Amplification',
                  'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin',
                  'Golden Gorget of Leoric', 'Hellfire Amulet')
 
UPDATE UsersGames
   SET Cash -=
(
    SELECT SUM(i.Price)
      FROM Items AS i
     WHERE i.Name IN ('Blackguard', 'Bottomless Potion of Amplification',
                  'Eye of Etlich (Diablo III)', 'Gem of Efficacious Toxin',
                  'Golden Gorget of Leoric', 'Hellfire Amulet')
)
 WHERE Id =
(
    SELECT ug.Id
      FROM Users AS u
     INNER JOIN UsersGames AS ug
        ON u.Id = ug.UserId
     INNER JOIN Games AS g
        ON g.Id = ug.GameId
     WHERE u.Username = 'Alex' AND g.Name = 'Edinburgh'
)
 
SELECT u.Username, g.Name, ug.Cash, i.Name AS [Item Name]
  FROM Users AS u
 INNER JOIN UsersGames AS ug
    ON u.Id = ug.UserId
 INNER JOIN Games AS g
    ON ug.GameId = g.Id
 INNER JOIN UserGameItems AS ugi
    ON ugi.UserGameId = ug.Id
 INNER JOIN Items AS i
    ON ugi.ItemId = i.Id
 WHERE g.Name = 'Edinburgh'
 ORDER BY i.Name

 29. Peaks and Mountains

 SELECT p.PeakName, m.MountainRange as Mountain, p.Elevation FROM Mountains m
JOIN Peaks p
ON  p.MountainId = m.Id
ORDER BY p.Elevation DESC, p.PeakName

30. Peaks with Mountain, Country and Continent

SELECT p.PeakName, m.MountainRange as Mountain, c.CountryName, cc.ContinentName  FROM Mountains m
JOIN Peaks p
ON  p.MountainId = m.Id
JOIN MountainsCountries mc
ON mc.MountainId = p.MountainId
JOIN Countries c
ON mc.CountryCode = c.CountryCode 
JOIN Continents cc
ON c.ContinentCode = cc.ContinentCode
ORDER BY p.PeakName, c.CountryName

31. Rivers by Country

SELECT c.CountryName, cc.ContinentName ,COUNT(cr.RiverId) as RiversCount, ISNULL(SUM(r.Length), 0) as TotalLenght FROM Countries c 
LEFT JOIN CountriesRivers cr
ON cr.CountryCode = c.CountryCode
LEFT JOIN Rivers r
ON cr.RiverId = r.Id
LEFT JOIN Continents cc
ON c.ContinentCode = cc.ContinentCode
GROUP BY c.CountryName, cc.ContinentName
ORDER BY RiversCount DESC , TotalLenght DESC, c.CountryName

32. Count of Countries by Currency

SELECT cr.CurrencyCode , cr.Description as Currency, COUNT(c.CurrencyCode) as NumberOfCountries FROM Currencies cr
LEFT JOIN Countries c
ON cr.CurrencyCode = c.CurrencyCode
GROUP BY cr.CurrencyCode, cr.Description
ORDER BY NumberOfCountries DESC, Currency

33. Population and Area by Continent

SELECT ContinentName,SUM(CONVERT(bigint, AreaInSqKm)) as CountriesArea ,SUM(CONVERT(bigint, Population)) as CountriesPopulation FROM Continents cs
JOIN 
(select CountryName, ContinentCode, Population, AreaInSqKm from Countries) as c
ON cs.ContinentCode = c.ContinentCode
GROUP BY ContinentName
ORDER BY CountriesPopulation DESC

34. Monasteries by Country

CREATE TABLE Monasteries
(
Id int primary key identity(1,1),
Name varchar(max) not null,
CountryCode char(2) not null
CONSTRAINT FK_CountryCode
FOREIGN KEY (CountryCode) REFERENCES Countries(CountryCode)
)
INSERT INTO Monasteries(Name, CountryCode) VALUES
('Rila Monastery “St. Ivan of Rila”', 'BG'), 
('Bachkovo Monastery “Virgin Mary”', 'BG'),
('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
('Kopan Monastery', 'NP'),
('Thrangu Tashi Yangtse Monastery', 'NP'),
('Shechen Tennyi Dargyeling Monastery', 'NP'),
('Benchen Monastery', 'NP'),
('Southern Shaolin Monastery', 'CN'),
('Dabei Monastery', 'CN'),
('Wa Sau Toi', 'CN'),
('Lhunshigyia Monastery', 'CN'),
('Rakya Monastery', 'CN'),
('Monasteries of Meteora', 'GR'),
('The Holy Monastery of Stavronikita', 'GR'),
('Taung Kalat Monastery', 'MM'),
('Pa-Auk Forest Monastery', 'MM'),
('Taktsang Palphug Monastery', 'BT'),
('Sümela Monastery', 'TR')
UPDATE Countries 
SET isDeleted = 1
WHERE CountryCode IN
(SELECT c.CountryCode as RiversCount from Countries c 
LEFT JOIN CountriesRivers cr
ON c.CountryCode = cr.CountryCode 
GROUP BY c.CountryCode
HAVING COUNT(cr.RiverId) > 3 ) 

SELECT m.Name as Monastery, c.CountryName as Country
 FROM Monasteries m
join Countries c
on c.CountryCode = m.CountryCode
WHERE isDeleted = 0
order by m.Name

35. Monasteries by Continents and Countries

UPDATE Countries
SET CountryName = 'Burma'
WHERE CountryName = 'Myanmar'

insert into Monasteries(Name, CountryCode)
select 
    'Hanga Abbey', c.CountryCode
from Countries c 
where CountryName = 'Tanzania'

insert into Monasteries(Name, CountryCode)
select 
    'Myin-Tin- Daik', c.CountryCode
from Countries c 
where CountryName = 'Myanmar'


SELECT cc.ContinentName, c.CountryName, COUNT(e.Id) as MonasteriesCount  FROM Continents cc
LEFT JOIN Countries c
ON c.ContinentCode = cc.ContinentCode
LEFT JOIN Monasteries e
on e.CountryCode = c.CountryCode
WHERE c.isDeleted = 0
GROUP BY cc.ContinentName, c.CountryName
ORDER BY MonasteriesCount DESC, c.CountryName


