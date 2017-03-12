--Section 1: DDL

CREATE TABLE Towns (
	TownID INT,
	TownName VARCHAR(30) NOT NULL,
	CONSTRAINT PK_Towns PRIMARY KEY(TownID)
)

CREATE TABLE Airports (
	AirportID INT,
	AirportName VARCHAR(50) NOT NULL,
	TownID INT NOT NULL,
	CONSTRAINT PK_Airports PRIMARY KEY(AirportID),
	CONSTRAINT FK_Airports_Towns FOREIGN KEY(TownID) REFERENCES Towns(TownID)
)

CREATE TABLE Airlines (
	AirlineID INT,
	AirlineName VARCHAR(30) NOT NULL,
	Nationality VARCHAR(30) NOT NULL,
	Rating INT DEFAULT(0),
	CONSTRAINT PK_Airlines PRIMARY KEY(AirlineID)
)

CREATE TABLE Customers (
	CustomerID INT,
	FirstName VARCHAR(20) NOT NULL,
	LastName VARCHAR(20) NOT NULL,
	DateOfBirth DATE NOT NULL,
	Gender VARCHAR(1) NOT NULL CHECK (Gender='M' OR Gender='F'),
	HomeTownID INT NOT NULL,
	CONSTRAINT PK_Customers PRIMARY KEY(CustomerID),
	CONSTRAINT FK_Customers_Towns FOREIGN KEY(HomeTownID) REFERENCES Towns(TownID)
)



CREATE TABLE Flights
(
FlightID int PRIMARY KEY ,
DepartureTime datetime NOT NULL,
ArrivalTime datetime NOT NULL,
Status varchar(9)
CONSTRAINT chk_Status CHECK (Status IN ('Departing', 'Delayed', 'Arrived', 'Cancelled')),
OriginAirportID INT,
DestinationAirportID int,
AirlineID int,
CONSTRAINT FK_OriginAirportID FOREIGN KEY(OriginAirportID) REFERENCES Airports(AirportID),
CONSTRAINT FK_DestinationAirportID FOREIGN KEY(DestinationAirportID) REFERENCES Airports(AirportID),
CONSTRAINT FK_AirlineID FOREIGN KEY(AirlineID) REFERENCES Airlines(AirlineID)
)

CREATE TABLE Tickets
(
TicketID int PRIMARY KEY,
Price decimal (8,2) not null,
Class varchar(6) 
CONSTRAINT chk_Class CHECK (Class IN ('First', 'Second', 'Third')),
Seat varchar(5) NOT NULL,
CustomerID int,
FlightID int,
CONSTRAINT FK_CustomerID
FOREIGN KEY(CustomerID) REFERENCES Customers(CustomerID),
CONSTRAINT FK_FlightID
FOREIGN KEY(FlightID) REFERENCES Flights(FlightID)
)

--Section 2: DML - 01. Data Insertion

INSERT INTO Flights(FlightID,DepartureTime, ArrivalTime, Status, OriginAirportID, DestinationAirportID, AirlineID)
VALUES (1, '20161013 06:00', '20161013 10:00', 'Delayed', 1, 4, 1),
(2, '20161012 12:00', '20161013 12:01', 'Departing', 1, 3, 2),
(3, '20161014 15:00', '20161020 04:00', 'Delayed', 4, 2, 4),
(4, '20161012 13:24', '20161012 16:31', 'Departing', 3, 1, 3),
(5, '20161012 08:11', '20161012 23:22', 'Departing', 4, 1, 1),
(6, '19950621 12:30', '20161013 20:30', 'Arrived', 2, 3, 5),
(7, '20161012 23:34', '20161013 03:00', 'Departing', 2, 4, 2),
(8, '20161111 13:00', '20161112 22:00', 'Delayed', 4, 3, 1),
(9, '20151001 12:00', '20151201 01:00', 'Arrived', 1, 2, 1),
(10, '20161012 19:30', '20161013 12:30', 'Departing', 2 , 1, 7)


INSERT INTO Tickets(TicketID, Price, Class, Seat, CustomerID, FlightID)
VALUES (1, 3000, 'First', '233-A', 3, 8),
(2, 1788.90, 'Second', '123-D', 1, 1),
(3, 1200.50, 'Second', '12-Z', 2, 5),
(4, 410.68, 'Third', '45-Q', 2, 8),
(5, 560, 'Third', '201-R', 4, 6),
(6, 2100, 'Second', '13-T', 1, 9),
(7, 5500, 'First', '98-O', 2, 7)

--Section 2: DML - 02. Update Flights

UPDATE Flights
SET AirlineID = 1
WHERE Status IN ( 'Arrived')

--Section 2: DML - 03. Update Tickets

UPDATE Tickets 
SET Price += (Price / 2)
WHERE FlightID IN (SELECT FlightID from Flights
where AirlineID IN (SELECT TOP 1  a.AirlineID FROM Flights f  
JOIN Airlines a
on a.AirlineID = f.AirlineID
JOIN Tickets T
on t.FlightID = f.FlightID
ORDER BY Rating DESC) )

--Section 2: DML - 04. Table Creation

CREATE TABLE CustomerReviews
(
ReviewID int PRIMARY KEY,
ReviewContent varchar(255) NOT NULL,
ReviewGrade int
CONSTRAINT chk_ReviewGrade
CHECK (ReviewGrade BETWEEN 0 AND 10),
AirlineID int,
CustomerID int,
CONSTRAINT FK_CustomerReviews_Airlines
FOREIGN KEY (AirlineID) REFERENCES Airlines(AirlineID),
CONSTRAINT FK_CustomerReviews_Customers
FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
)
 
 
CREATE TABLE CustomerBankAccounts
(
AccountID int PRIMARY KEY,
AccountNumber varchar(10) NOT NULL UNIQUE,
Balance decimal(10,2) NOT NULL,
CustomerID int
CONSTRAINT FK_CustomerBankAccounts_Customers
FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
)

--Section 2: DML - 05. Fillin New Tables

INSERT INTO CustomerReviews(ReviewID, ReviewContent, ReviewGrade, AirlineID, CustomerID)
VALUES (1, 'Me is very happy. Me likey this airline. Me good.', 10, 1,1),
(2, 'Ja, Ja, Ja... Ja, Gut, Gut, Ja Gut! Sehr Gut!', 10, 1,4),
(3, 'Meh...', 5, 4,3),
(4, 'Well Ive seen better, but Ive certainly seen a lot worse...', 7, 3,5)

INSERT INTO CustomerBankAccounts (AccountID, AccountNumber,
	Balance, CustomerID)
VALUES
	(1, '123456790', 2569.23, 1),
	(2, '18ABC23672', 14004568.23, 2),
	(3, 'F0RG0100N3', 19345.20, 5)
		

--Section 3: Querying - 01. Extract All Tickets

select TicketID,Price,Class,Seat from Tickets
order by TicketID

--Section 3: Querying - 02. Extract All Customers

select CustomerID, FirstName + ' ' + LastName as FullName, Gender from Customers
order by FullName, CustomerID

--Section 3: Querying - 03. Extract Delayed Flights

SELECT FlightID, DepartureTime,ArrivalTime FROM Flights
WHERE Status = 'Delayed'

--Section 3: Querying - 04. Top 5 Airlines

Select top 5 a.AirlineID, a.AirlineName, a.Nationality,a.Rating from Airlines a
JOIN (SELECT a.AirlineName, Count(f.FlightID) AS Counts FROM Airlines a
JOIN Flights f on f.AirlineID = a.AirlineID
GROUP BY AirlineName) e2 ON e2.AirlineName = a.AirlineName
ORDER BY a.Rating DESC, a.AirlineID

-- Section 3: Querying - 05. All Tickets Below 5000

SELECT t.TicketID, air.AirportName, c.FirstName + ' ' + c.LastName as CustomerName FROM Tickets t
JOIN Customers c on c.CustomerID = t.CustomerID
JOIN Flights f
on f.FlightID = t.FlightID
JOIN Airports air
on air.AirportID = f.DestinationAirportID
WHERE t.Class = 'First' and t.Price < 5000
ORDER BY TicketID

-- Section 3: Querying - 06. Customers From Home

SELECT c.CustomerID, c.FirstName + ' ' + c.LastName as FullName, t2.TownName FROM Customers c
join Tickets t
on t.CustomerID = c.CustomerID
join Flights f
on f.FlightID = t.FlightID
join Airports air
on air.AirportID = f.OriginAirportID
join Towns t2
on t2.TownID = air.TownID
WHERE t2.TownID = c.HomeTownID AND f.Status = 'Departing'

-- Section 3: Querying - 07. Customers who will fly

SELECT DISTINCT c.CustomerID, c.FirstName + ' ' + c.LastName as FullName , 2016 - YEAR(DateOfBirth) as Age FROM Customers c
JOIN Tickets t
on t.CustomerID = c.CustomerID
JOIN Flights f
on t.FlightID = f.FlightID
WHERE f.Status = 'Departing' 
ORDER BY Age, c.CustomerID

-- Section 3: Querying - 08. Top 3 Customers Delayed

SELECT TOP 3 c.CustomerID, c.FirstName + ' ' + c.LastName as FullName , t.Price as TicketPrice, air.AirportName as Destination FROM Customers c
JOIN Tickets t
on t.CustomerID = c.CustomerID
JOIN Flights f
on t.FlightID = f.FlightID
JOIN Airports air
on air.AirportID = f.DestinationAirportID
WHERE f.Status = 'Delayed'
ORDER BY TicketPrice DESC, c.CustomerID 

-- Section 3: Querying - 09. Last 5 Departing Flights

select FlightID, DepartureTime, ArrivalTime, Origin, Destination from (SELECT distinct top 5 f.FlightID, f.DepartureTime, f.ArrivalTime,air2.AirportName as Origin, air.AirportName as Destination FROM Flights f
JOIN Airports air
on air.AirportID = f.DestinationAirportID
JOIN Airports air2
on air2.AirportID = f.OriginAirportID
WHERE f.Status = 'Departing'
ORDER BY f.DepartureTime DESC) as eho
ORDER BY DepartureTime, FlightID

-- Section 3: Querying - 10. Customers Below 21

SELECT distinct c.CustomerID, c.FirstName + ' ' + c.LastName as FullName, 2016 - YEAR(c.DateOfBirth) as Age FROM Tickets t
JOIN Customers c
on c.CustomerID = t.CustomerID
join Flights f
on f.FlightID = t.FlightID
WHERE f.Status = 'Arrived' and 2016 - YEAR(c.DateOfBirth) < 21
ORDER BY Age DESC, CustomerID

-- Section 3: Querying - 11. AIrports and Passengers

select air.AirportID,air.AirportName, Passengers from Airports air
join 
(Select air.AirportName, Count(c.FirstName + ' ' + c.LastName) as Passengers from Flights f
join Airports air
on air.AirportID = f.OriginAirportID
join Tickets t
on t.FlightID = f.FlightID
join Customers c 
on c.CustomerID = t.CustomerID
WHERE f.Status = 'Departing'
GROUP BY air.AirportName) e2
on e2.AirportName = air.AirportName
ORDER BY AirportID

-- Section 4: Programmibility - 01. Submit Review

create PROC usp_SubmitReview(@CustomerID int, @ReviewContent varchar(255), @ReviewGrade int, @AirlineName varchar(30))
as

declare @index int;
if((Select count(*) from CustomerReviews) < 1)
begin 
set @index = 0
end
else
begin
set @index =(Select MAX(ReviewID) from CustomerReviews) + 1
end

declare @airlineId varchar(30) = (Select AirlineID from Airlines where AirlineName = @AirlineName)

INSERT INTO CustomerReviews(ReviewID, ReviewContent, ReviewGrade, AirlineID, CustomerID)
values(@index, @ReviewContent, @ReviewGrade, @airlineId, @CustomerID )
if( @AirlineName NOT IN (select AirlineName from Airlines))
begin 
raiserror('Airline does not exist.', 16, 1)
rollback
end

-- Section 4: Programmibility - 02. Ticket Purchase

create proc usp_PurchaseTicket(@CustomerID int, @FlightID int, @TicketPrice decimal(8,2), @Class varchar(6), @Seat varchar(5))
as
begin tran
declare @customerBalance money = (select Balance from Customers c
left join CustomerBankAccounts cba
on cba.CustomerID = c.CustomerID
where c.CustomerID = @CustomerID)
IF (@customerBalance IS NULL)
begin
set @customerBalance = 0
end
if (@TicketPrice > @customerBalance )
begin 
rollback
raiserror('Insufficient bank account balance for ticket purchase.', 16,1)
return
end
UPDATE CustomerBankAccounts
SET Balance -= @TicketPrice
WHERE @CustomerID = CustomerID

declare @index int;
if((Select count(*) from Tickets) < 1)
begin 
set @index = 0
end
else
begin
set @index =(Select MAX(TicketID) from Tickets) + 1
end

insert into Tickets(TicketID ,Price, Class, Seat, CustomerID, FlightID)
values (@index,@TicketPrice, @Class, @Seat, @CustomerID, @FlightID)
commit

-- BONUS Section 5: Update Trigger

CREATE TRIGGER tr_ArrivedFlights ON Flights AFTER UPDATE 
as
begin 
INSERT INTO ArrivedFlights(FlightID, ArrivalTime, Origin, Destination, Passengers)
SELECT i.FlightID,i.ArrivalTime ,air.AirportName, air2.AirportName, Passengers from inserted i
join
(select f.FlightID, COUNT(CustomerID) as Passengers  from Flights f
left join Tickets t
on f.FlightID = t.FlightID
WHERE Status = 'Arrived'
GROUP BY f.FlightID) f2
on f2.FlightID = i.FlightID
JOIN Airports air
on air.AirportID = i.OriginAirportID
JOIN Airports air2
on air2.AirportID = i.DestinationAirportID

end


