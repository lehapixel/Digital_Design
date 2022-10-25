
CREATE TABLE Сustomers
(
	FullName nvarchar(100) PRIMARY KEY NOT NULL,
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
	AddressLine nchar(150) PRIMARY KEY NOT NULL,
	City nvarchar(50) NOT NULL, 
)

CREATE TABLE Orders
(
	OrderID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FullNameСustomer nvarchar(100) NOT NULL,
	DateOrder date NOT NULL,
	OrderAddress nchar(150) NOT NULL,
	ProductName nvarchar(100),
	Qty nvarchar(4000) NOT NULL, 
	FOREIGN KEY (FullNameСustomer) REFERENCES Сustomers(FullName),
	FOREIGN KEY (OrderAddress) REFERENCES Addresses(AddressLine),
	FOREIGN KEY (ProductName) REFERENCES Products(ProductName),
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

--[Sales.Order]
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1703.05.27', 'Сенатская площадь д.1', 'Рама оконная', 1);
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Софи́я Авгу́ста Фредери́ка А́нгальт-Це́рбстская', '1762.06.28', 'площадь Островского д.1', 'Платье бальное', 999);
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Александр Рюрикович', '1242.04.05', 'Сенатская площадь д.1', 'Грудка куриная', 5);
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Александр Рюрикович', '1242.04.06', 'Сенатская площадь д.1', 'Салат', 5);
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Топор', 1);
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Пила', 1);
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Доска', 200);
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Брус', 20);
INSERT INTO Orders(FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Парусина', 100);