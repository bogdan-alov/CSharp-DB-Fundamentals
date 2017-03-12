01. Records’ Count

SELECT 
    COUNT(DISTINCT Id) AS 'Count'
  FROM WizzardDeposits

02. Longest Magic Wand

SELECT 
    Max([MagicWandSize]) AS 'LongestMagicWand'
  FROM WizzardDeposits

03. Longest Magic Wand per Deposit Groups

SELECT 
    DepositGroup ,
	Max(MagicWandSize) as 'LongestMagicWand'
  FROM WizzardDeposits
  GROUP BY [DepositGroup] 

04. Smallest Deposit Group per Magic Wand Size

SELECT TOP(1) WITH TIES DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize) 

05. Deposits Sum

SELECT DepositGroup, 
		SUM(DepositAmount) AS [Total Sum]
FROM WizzardDeposits
GROUP BY DepositGroup

06. Deposits Sum for Ollivander Family

SELECT DepositGroup, 
		SUM(DepositAmount) AS [Total Sum]
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

07. Deposits Filter

SELECT DepositGroup, 
		SUM(DepositAmount) AS [TotalSum]
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY [TotalSum] DESC

08. Deposit Charge

SELECT DepositGroup,
		MagicWandCreator,
		MIN(DepositCharge) AS 'MinDepositCharge'
FROM WizzardDeposits
GROUP BY DepositGroup, MagicWandCreator
ORDER BY MagicWandCreator, DepositGroup

09. Age Groups

SELECT CASE
WHEN Age >= 0 AND AGE < 11 THEN '[0-10]'
WHEN Age >= 11 AND AGE < 21 THEN '[11-20]'
WHEN Age >= 21 AND AGE < 31 THEN '[21-30]'
WHEN Age >= 31 AND AGE < 41 THEN '[31-40]'
WHEN Age >= 41 AND AGE < 51 THEN '[41-50]'
WHEN Age >= 51 AND AGE < 61 THEN '[51-60]'
WHEN Age > 60 THEN '[61+]'
END AS [AgeGroup]
,
COUNT(Id) AS 'WizardCount'
FROM WizzardDeposits
GROUP BY  CASE
WHEN Age >= 0 AND AGE < 11 THEN '[0-10]'
WHEN Age >= 11 AND AGE < 21 THEN '[11-20]'
WHEN Age >= 21 AND AGE < 31 THEN '[21-30]'
WHEN Age >= 31 AND AGE < 41 THEN '[31-40]'
WHEN Age >= 41 AND AGE < 51 THEN '[41-50]'
WHEN Age >= 51 AND AGE < 61 THEN '[51-60]'
WHEN Age > 60 THEN '[61+]'
END  
ORDER BY AgeGroup

10. First Letter

SELECT  
LEFT(FirstName, 1) AS FirstLetter
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY LEFT(FirstName, 1)
ORDER BY LEFT(FirstName, 1) 

11. Average Interest

SELECT  
DepositGroup,
IsDepositExpired,
AVG(DepositInterest) AS 'AverageInterest'

FROM WizzardDeposits
WHERE DepositStartDate > '01/01/1985'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired

12. Rich Wizard, Poor Wizard

SELECT SUM(SumDiff.SumDifference)  AS SumDifference
FROM
	(SELECT h.DepositAmount - (SELECT DepositAmount FROM WizzardDeposits WHERE Id = h.Id + 1) as SumDifference FROM WizzardDeposits as h) as SumDiff

13. Departments Total Salaries

SELECT DepartmentId, SUM(Salary) as TotalSalary
FROM Employees 
GROUP BY DepartmentId
ORDER BY DepartmentId

14. Employees Minimum Salaries

SELECT DepartmentId, MIN(Salary) as MinimumSalary
FROM Employees 
WHERE DepartmentId IN (2,5,7) AND HireDate > '01/01/2000'
GROUP BY DepartmentId
ORDER BY DepartmentId

15. Employees Average Salaries

SELECT * INTO NewTable
FROM Employees
WHERE Salary > 30000

DELETE FROM NewTable
WHERE ManagerID = 42

UPDATE NewTable
SET Salary +=5000
WHERE DepartmentID =1

SELECT DepartmentID, AVG(Salary) FROM NewTable
GROUP BY DepartmentID


16. Employees Maximum Salaries

SELECT DepartmentId, MAX(Salary) as MinimumSalary
FROM Employees 
GROUP BY DepartmentId
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000
ORDER BY DepartmentId

17. Employees Count Salaries

SELECT COUNT(EmployeeID) AS 'Count'
FROM
	Employees
WHERE ManagerID IS NULL

18. 3rd Highest Salary

SELECT DepartmentID, 
	(SELECT DISTINCT Salary FROM Employees 
	 WHERE DepartmentID = e.DepartmentID 
	 ORDER BY Salary DESC OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY) AS ThirdHighestSalary
FROM Employees e
WHERE (SELECT DISTINCT Salary FROM Employees 
	 WHERE DepartmentID = e.DepartmentID 
	 ORDER BY Salary DESC OFFSET 2 ROWS FETCH NEXT 1 ROWS ONLY) IS NOT NULL
GROUP BY DepartmentID

19. Salary Challenge

SELECT TOP(10)FirstName, 
		LastName,
		DepartmentId
FROM Employees e1
WHERE e1.Salary > ( 
SELECT  AVG(e2.Salary) as 'AvgSalary'
FROM (SELECT Salary, DepartmentId FROM Employees) as e2
WHERE DepartmentId = e1.DepartmentId
)
