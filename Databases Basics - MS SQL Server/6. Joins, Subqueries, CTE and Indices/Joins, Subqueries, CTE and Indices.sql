01. Employee Address

SELECT TOP(5) e.EmployeeID, e.JobTitle, e.AddressID, a.AddressText
FROM Employees as e
FULL OUTER JOIN Addresses as a
ON e.AddressID = a.AddressID

02. Addresses with Towns

SELECT TOP(50)  e.FirstName, e.LastName, t.Name , a.AddressText
FROM Employees as e
FULL OUTER JOIN Addresses as a
ON e.AddressID = a.AddressID
FULL OUTER JOIN Towns as t
ON a.TownID= t.TownID
WHERE FirstName IS NOT NULL AND LastName IS NOT NULL
ORDER BY e.FirstName, e.LastName

03. Sales Employees

SELECT  e.EmployeeID,e.FirstName, e.LastName, d.Name
FROM Employees as e
FULL OUTER JOIN Departments as d
ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY e.EmployeeID

04. Employee Departments

SELECT TOP(5)  e.EmployeeID,e.FirstName, e.Salary, d.Name
FROM Employees as e
FULL OUTER JOIN Departments as d
ON e.DepartmentID = d.DepartmentID
WHERE e.Salary > 15000
ORDER BY e.DepartmentID

05. Employees Without Projects

SELECT top 3 e.EmployeeID, e.FirstName
 from Employees e
FULL outer join EmployeesProjects ep
on ep.EmployeeID = e.EmployeeID
FULL join Projects p
on p.ProjectID = ep.ProjectID
WHERE p.ProjectID IS NULL
ORDER BY EmployeeID

06. Employees Hired After

SELECT e.FirstName,  e.LastName, e.HireDate, d.Name as 'DeptName'
 from Employees e
 join Departments d
 on e.DepartmentID = d.DepartmentID
 WHERE d.Name IN ('Sales', 'Finance') and
 e.HireDate > '19990101'

 07. Employees With Project

 SELECT TOP 5 e.EmployeeID,  e.FirstName, p.Name as ProjectName
 from Employees e
 join EmployeesProjects ep
 on e.EmployeeID = ep.EmployeeID
 join Projects p
 on ep.ProjectID = p.ProjectID
 where p.StartDate >= '20020813' AND p.EndDate IS NULL
ORDER BY e.EmployeeID

08. Employee 24

SELECT e.EmployeeID, e.FirstName, ProjectName FROM (SELECT CASE
WHEN YEAR(p.StartDate) >= '2005' THEN NULL
ELSE p.Name
END AS 'ProjectName', ProjectID, StartDate
from Projects p) as projects 
FULL outer join EmployeesProjects ep
on ep.ProjectID = projects.ProjectID
FULL outer join Employees e
on ep.EmployeeID = e.EmployeeID
WHERE e.EmployeeID = 24

09. Employee Manager

SELECT EmployeeID, FirstName, ManagerID, ManagerName FROM(SELECT e1.EmployeeID, e1.FirstName, e1.ManagerID, e2.FirstName as ManagerName FROM Employees e1
FULL OUTER JOIN Employees e2
ON e1.ManagerID = e2.EmployeeID
WHERE e1.ManagerID IN (3,7)) as managers
ORDER BY EmployeeID 

10. Employees Summary

SELECT TOP 50 EmployeeID, EmployeeName, ManagerName, DepartmentName FROM(SELECT e1.EmployeeID, e1.FirstName + ' ' + e1.LastName as EmployeeName, e1.ManagerID, e2.FirstName + ' ' + e2.LastName as ManagerName, d.Name as DepartmentName FROM Employees e1
LEFT OUTER JOIN Employees e2
ON e1.ManagerID = e2.EmployeeID
FULL OUTER JOIN Departments d
ON e1.DepartmentID = d.DepartmentID
) as managers
ORDER BY EmployeeID 

11. Min Average Salary

SELECT MIN(Salary) as MinAverageSalary FROM(SELECT e1.DepartmentID, avg(e1.Salary) as Salary FROM Employees e1
FULL OUTER JOIN Departments d
ON e1.DepartmentID = d.DepartmentID
GROUP BY e1.DepartmentID
) as managers

12. Highest Peaks in Bulgaria

SELECT * FROM (SELECT mc.CountryCode,MountainRange,PeakName, Elevation FROM Peaks p
JOIN Mountains m
ON m.Id = p.MountainId
JOIN MountainsCountries mc
ON mc.MountainId = m.Id
JOIN Countries c
ON c.CountryCode = mc.CountryCode
WHERE c.CountryCode = 'BG' AND p.Elevation > 2835) as peaks
ORDER BY Elevation DESC

13. Count Mountain Ranges

SELECT * FROM (SELECT  CountryCode, COUNT(*) AS 'MountainRanges'
  FROM MountainsCountries mc
  JOIN Mountains m
  ON m.Id = mc.MountainId
  WHERE CountryCode IN ('BG', 'RU', 'US')
  GROUP BY CountryCode 
) as peaks

14. Countries With or Without Rivers

SELECT top 5 c.CountryName, r.RiverName FROM Continents cc
 LEFT OUTER JOIN Countries c
ON   cc.ContinentCode = c.ContinentCode
LEFT outer JOIN CountriesRivers cr
ON cr.CountryCode = c.CountryCode
LEFT outer JOIN Rivers r
ON r.Id = cr.RiverId
WHERE cc.ContinentCode = 'AF'
ORDER BY C.CountryName

15. *Continents and Currencies

SELECT usages.ContinentCode, usages.CurrencyCode, usages.CurrencyUsage FROM
(SELECT ContinentCode, CurrencyCode, COUNT(*) AS CurrencyUsage FROM Countries AS c
GROUP BY ContinentCode, CurrencyCode
HAVING COUNT(*) > 1
) usages
INNER JOIN
(
SELECT u.ContinentCode, MAX(u.Usage) as CurrencyUsage FROM (SELECT ContinentCode, CurrencyCode, COUNT(*) AS Usage FROM Countries AS c
GROUP BY ContinentCode, CurrencyCode
) as u
GROUP BY u.ContinentCode) maxUsages ON usages.ContinentCode = maxUsages.ContinentCode and maxUsages.CurrencyUsage = usages.CurrencyUsage

16. Countries Without any Mountains

SELECT COUNT(*) as CountryCode FROM Countries c
LEFT OUTER JOIN MountainsCountries mc
ON mc.CountryCode = c.CountryCode
WHERE mc.MountainId IS NULL

17. Highest Peak and Longest River by Country

SELECT top 5 c.CountryName, MAX(p.Elevation) as HighestPeakElevation, MAX(r.Length) as LongestRiverLenght FROM Countries c 
FULL OUTER JOIN MountainsCountries mc
ON c.CountryCode = mc.CountryCode
FULL OUTER JOIN Mountains m
ON mc.MountainId = m.Id
FULL OUTER JOIN Peaks p
ON p.MountainId = m.Id
FULL OUTER JOIN CountriesRivers cr
ON cr.CountryCode = c.CountryCode
FULL OUTER JOIN Rivers r
ON r.Id = cr.RiverId
GROUP BY c.CountryName
ORDER BY HighestPeakElevation DESC, LongestRiverLenght DESC, c.CountryName 


18. *Highest Peak Name and Elevation by Country

SELECT top 5 c.CountryName,
ISNULL(p.PeakName, '(no highest peak)') as HighestPeakName,
ISNULL(MAX(p.Elevation), 0) as HighestPeakElevation,
ISNULL(m.MountainRange, '(no mountain)') as Mountain
FROM Countries c
FULL OUTER JOIN MountainsCountries mc
ON c.CountryCode = mc.CountryCode
LEFT OUTER JOIN Mountains m
ON mc.MountainId = m.Id
LEFT OUTER JOIN Peaks p
ON p.MountainId = m.Id
LEFT OUTER JOIN CountriesRivers cr
ON cr.CountryCode = c.CountryCode
LEFT OUTER JOIN Rivers r
ON r.Id = cr.RiverId
GROUP BY c.CountryName, p.PeakName, p.Elevation, m.MountainRange
ORDER BY c.CountryName, p.PeakName

