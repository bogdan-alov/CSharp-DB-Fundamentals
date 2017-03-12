01. Find Names of All Employees by First Name

SELECT FirstName,
		LastName
 FROM Employees

 WHERE LEFT(FirstName, 2) = 'SA'

 02. Find Names of All Employees by Last Name

 SELECT FirstName,
		LastName
 FROM Employees

WHERE CHARINDEX('EI', LastName) > 0

03. Find First Names of All Employess

SELECT FirstName
 FROM Employees
 WHERE DepartmentID IN (3,10) AND YEAR(HireDate) BETWEEN 1995 AND 2005

04. Find All Employees Except Engineers

SELECT FirstName,
		LastName
 FROM Employees
 WHERE JobTitle NOT LIKE '%engineer%'

05. Find Towns with Name 

SELECT Name
 FROM Towns
WHERE LEN(Name) = 5 or LEN(Name) = 6
ORDER BY Name

06. Find Towns Starting With

SELECT TownID,
		Name
 FROM Towns
WHERE Name LIKE 'm%' or Name LIKE 'k%'  or Name LIKE 'b%'  or Name LIKE 'e%' 
ORDER BY Name

07. Find Towns Not Starting With

SELECT TownID,
	Name 
		
 FROM Towns
WHERE LEFT(Name, 1) NOT IN ('R', 'B', 'D')
ORDER BY Name

08. Create View Employees Hired After

CREATE VIEW V_EmployeesHiredAfter2000
AS 
SELECT FirstName,
		LastName
FROM Employees
WHERE YEAR(HireDate) > 2000

09. Length of Last Name

SELECT FirstName,
		LastName
FROM Employees
WHERE LEN(LastName) = 5

10. Countries Holding 'A'

SELECT CountryName, IsoCode AS 'ISO Code' 
FROM Countries
WHERE CountryName LIKE '%a%' + '%a%' + '%a%' 
ORDER BY [ISO Code]

11. Mix of Peak and River Names

SELECT PeakName, RiverName, 
CASE
WHEN RIGHT(PeakName, 1) = LEFT(RiverName,1) THEN LOWER(LEFT(PeakName, LEN(PeakName) - 1)) + LOWER(RiverName)
END AS 'Mix'
FROM Peaks, Rivers
WHERE RIGHT(PeakName, 1) = LEFT(RiverName,1)
ORDER BY PeakName, RiverName

12. Games From 2011 and 2012 Year

SELECT TOP(50) Name,FORMAT(Start,'yyyy-MM-dd') AS Start FROM Games as g
WHERE YEAR(Start) IN (2011,2012)
ORDER BY g.Start, G.Name

13. User Email Providers

SELECT	Username AS Username,
	RTRIM(LTRIM(SUBSTRING(Email,CHARINDEX('@',Email,1)+1,LEN(Email)))) AS [Email Provider]
	FROM Users
ORDER BY [Email Provider],Username

14. Get Users with IPAddress Like Pattern

SELECT
Username, IpAddress
FROM
Users
WHERE
IpAddress LIKE '___.1%.%.___'
ORDER BY Username

15. Show All Games with Duration

SELECT Name,
 CASE 
 WHEN DATEPART(HOUR, Start)>= 0 AND DATEPART(HOUR, Start) < 12 THEN 'Morning'
 WHEN DATEPART(HOUR, Start)>= 12 AND DATEPART(HOUR, Start) < 18 THEN 'Afternoon'
 WHEN DATEPART(HOUR, Start)>= 18 AND DATEPART(HOUR, Start) < 24 THEN 'Evening'
 END as 'Part of the Day',
 CASE 
 WHEN Duration < 3 OR Duration = 3 THEN 'Extra Short'
 WHEN Duration > 3 AND Duration < 7 THEN 'Short'
 WHEN Duration > 6 THEN 'Long'
 WHEN Duration IS NULL THEN 'Extra Long'
 END AS 'Duration'
 FROM Games as g
 
 ORDER BY Name, Duration, [Part of the Day]

 16. Orders Table

 SELECT ProductName, OrderDate, 
DATEADD (DAY, 3 , OrderDate ) AS 'Pay Due',
DATEADD(MONTH, 1, OrderDate) AS 'Deliver Due'
FROM Orders