CREATE TABLE [Сustomer.Persons]
(
	FullName nvarchar(100) PRIMARY KEY NOT NULL,
	Gender nvarchar(1) NOT NULL 
)

CREATE TABLE [Products.Prices]
(
	ProductName nvarchar(100) PRIMARY KEY NOT NULL,
	Price decimal NOT NULL,
)

CREATE TABLE [Products.Measures]
(
	ProductName nvarchar(100) PRIMARY KEY NOT NULL,
	Measure nvarchar(10) NOT NULL,
	FOREIGN KEY (ProductName) REFERENCES [Products.Prices](ProductName)
)

CREATE TABLE [Sales.Cities]
(
	City nvarchar(50) PRIMARY KEY NOT NULL
)

CREATE TABLE [Sales.Addresses]
(
	AddressLine nchar(150) PRIMARY KEY NOT NULL,
	City nvarchar(50) NOT NULL, 
	FOREIGN KEY (City) REFERENCES [Sales.Cities](City)
)


CREATE TABLE [Sales.Order]
(
	OrderID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	FullNameСustomer nvarchar(100) NOT NULL,
	DateOrder date NOT NULL,
	OrderAddress nchar(150) NOT NULL,
	ProductName nvarchar(100),
	Qty nvarchar(4000) NOT NULL, 
	FOREIGN KEY (FullNameСustomer) REFERENCES [Сustomer.Persons](FullName),
	FOREIGN KEY (OrderAddress) REFERENCES [Sales.Addresses](AddressLine),
	FOREIGN KEY (ProductName) REFERENCES [Products.Prices](ProductName),
)

--[Сustomer.Persons]
INSERT INTO [Сustomer.Persons](FullName, Gender)
	VALUES ('Петр Романов','М');
INSERT INTO [Сustomer.Persons](FullName, Gender)
	VALUES ('Софи́я Авгу́ста Фредери́ка А́нгальт-Це́рбстская','Ж');
INSERT INTO [Сustomer.Persons](FullName, Gender)
	VALUES ('Александр Рюрикович','М');

	--[Products.Prices]
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Рама оконная', 3875);
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Платье бальное', 15000);
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Грудка куриная', 180);
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Салат', 52);
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Топор', 500);
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Пила', 450);
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Доска', 4890);
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Брус', 9390);
INSERT INTO [Products.Prices](ProductName, Price)
	VALUES ('Парусина', 182);

--[Products.Measure]
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Рама оконная','шт');
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Платье бальное','шт');
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Грудка куриная', 'кг');
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Салат', 'шт');
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Топор', 'шт');
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Пила', 'шт');
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Доска', 'м3');
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Брус', 'м3');
INSERT INTO [Products.Measures](ProductName, Measure)
	VALUES ('Парусина', 'м.п.');

--[Sales.Cities]
INSERT INTO [Sales.Cities](City)
	VALUES ('СПб');

--[Sales.Addresses]
INSERT INTO [Sales.Addresses](AddressLine, City)
	VALUES ('Сенатская площадь д.1', 'СПб');
INSERT INTO [Sales.Addresses](AddressLine, City)
	VALUES ('площадь Островского д.1', 'СПб');
INSERT INTO [Sales.Addresses](AddressLine, City)
	VALUES ('пл. Александра Невского д.1', 'СПб');
INSERT INTO [Sales.Addresses](AddressLine, City)
	VALUES ('пл. Александра Невского д.2', 'СПб');

--[Sales.Order]
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1703.05.27', 'Сенатская площадь д.1', 'Рама оконная', 1);
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Софи́я Авгу́ста Фредери́ка А́нгальт-Це́рбстская', '1762.06.28', 'площадь Островского д.1', 'Платье бальное', 999);
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Александр Рюрикович', '1242.04.05', 'Сенатская площадь д.1', 'Грудка куриная', 5);
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Александр Рюрикович', '1242.04.06', 'Сенатская площадь д.1', 'Салат', 5);
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Топор', 1);
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Пила', 1);
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Доска', 200);
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Брус', 20);
INSERT INTO [Sales.Order](FullNameСustomer, DateOrder, OrderAddress, ProductName, Qty)
	VALUES ('Петр Романов', '1704.11.05', 'Сенатская площадь д.1', 'Парусина', 100);