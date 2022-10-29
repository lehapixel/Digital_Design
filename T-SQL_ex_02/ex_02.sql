CREATE TABLE Сustomers
(
	СustomerID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FullName nvarchar(100) NOT NULL,
	Gender nvarchar(1) NOT NULL 
)

CREATE TABLE Products
(
	ProductName nvarchar(100) PRIMARY KEY NOT NULL,
	Price decimal NOT NULL,
	Measure nvarchar(10) NOT NULL,
)

CREATE TABLE Addresses
(
	AddressesID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	AddressLine nchar(150) NOT NULL,
	City nvarchar(50) NOT NULL, 
)

CREATE TABLE OrderDetail
(
	OrderID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	DateOrder date NOT NULL,
	AddressesID int NOT NULL,
	СustomerID int NOT NULL,
	FOREIGN KEY (AddressesID) REFERENCES Addresses(AddressesID),
	FOREIGN KEY (СustomerID) REFERENCES Сustomers(СustomerID)
)

CREATE TABLE ProductOrder
(
	ProductOrderID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	ProductName nvarchar(100),
	Qty nvarchar(4000) NOT NULL,
	OrderID int NOT NULL,
	FOREIGN KEY (OrderID) REFERENCES OrderDetail(OrderID),		
	FOREIGN KEY (ProductName) REFERENCES Products(ProductName)
)


--[Сustomers]
INSERT INTO Сustomers(FullName, Gender)
	VALUES ('Петр Романов','М');
INSERT INTO Сustomers(FullName, Gender)
	VALUES ('Софи́я Авгу́ста Фредери́ка А́нгальт-Це́рбстская','Ж');
INSERT INTO Сustomers(FullName, Gender)
	VALUES ('Александр Рюрикович','М');

--[Products]
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Рама оконная', 3875,'шт');
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Платье бальное', 15000,'шт');
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Грудка куриная', 180, 'кг');
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Салат', 52, 'шт');
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Топор', 500, 'шт');
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Пила', 450, 'шт');
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Доска', 4890, 'м3');
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Брус', 9390, 'м3');
INSERT INTO Products(ProductName, Price, Measure)
	VALUES ('Парусина', 182, 'м.п.');

--[Addresses]
INSERT INTO Addresses(AddressLine, City)
	VALUES ('Сенатская площадь д.1', 'СПб');
INSERT INTO Addresses(AddressLine, City)
	VALUES ('площадь Островского д.1', 'СПб');
INSERT INTO Addresses(AddressLine, City)
	VALUES ('пл. Александра Невского д.1', 'СПб');
INSERT INTO Addresses(AddressLine, City)
	VALUES ('пл. Александра Невского д.2', 'СПб');


--[OrderDetail]--
INSERT INTO OrderDetail(DateOrder, AddressesID, СustomerID)
	VALUES ('1703.05.27', 1, 1)
INSERT INTO OrderDetail(DateOrder, AddressesID, СustomerID)
	VALUES ('1762.06.28', 2, 2)
INSERT INTO OrderDetail(DateOrder, AddressesID, СustomerID)
	VALUES ('1242.04.05', 3, 3)
INSERT INTO OrderDetail(DateOrder, AddressesID, СustomerID)
	VALUES ('1242.04.06', 4, 3)
INSERT INTO OrderDetail(DateOrder, AddressesID, СustomerID)
	VALUES ('1704.11.05', 1, 1)

--[ProductOrder]
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Рама оконная', 1, 1);
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Платье бальное', 999, 2);
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Грудка куриная', 5, 3);
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Салат', 5, 4);
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Топор', 1, 5);
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Пила', 1, 5);
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Доска', 200, 5);
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Брус', 20, 5);
INSERT INTO ProductOrder(ProductName, Qty, OrderID)
	VALUES ('Парусина', 100, 5);