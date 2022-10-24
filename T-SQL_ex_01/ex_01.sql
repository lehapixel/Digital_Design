/* Задача 2
Вывести общую сумму продаж с разбивкой по годам и месяцам, за все время работы компании */

SELECT YEAR(OrderDate) AS [Year] , Month(OrderDate) AS [Month], SUM(SubTotal) AS [Total monthly sales] 
FROM Sales.SalesOrderHeader
GROUP BY YEAR(OrderDate),Month(OrderDate)
ORDER BY YEAR(OrderDate),Month(OrderDate)

/* Задача 3
Выбрать 10 самых приоритетных городов для следующего магазина
Столбцы: Город | Приоритет
Приоритет определяется как количество покупателей в городе
В городе не должно быть магазина */

SELECT City AS [City], COUNT(DISTINCT BusinessEntityID) AS [Priority]
FROM Sales.vIndividualCustomer
WHERE City NOT IN (SELECT City FROM Sales.vStoreWithAddresses)
GROUP BY City
ORDER BY COUNT(DISTINCT BusinessEntityID) DESC

/* Задача 4
Выбрать покупателей, купивших больше 15 единиц одного и того же продукта за все время работы компании.
Столбцы: Фамилия покупателя | Имя покупателя | Название продукта | Количество купленных экземпляров (за все время) 
Упорядочить по количеству купленных экземпляров по убыванию, затем по полному имени покупателя по возрастанию */

SELECT Person.LastName AS [Last Name], Person.FirstName AS [First Name], Product.Name AS [Product], SalesOrderDetail.OrderQty AS [Orders Quantity]
FROM Person.Person JOIN Sales.SalesOrderHeader ON Person.BusinessEntityID=SalesOrderHeader.SalesPersonID
					JOIN Sales.SalesOrderDetail ON SalesOrderHeader.SalesOrderID = SalesOrderDetail.SalesOrderID
					 JOIN Production.Product ON SalesOrderDetail.ProductID = Product.ProductID
WHERE SalesOrderDetail.OrderQty > 15
GROUP BY Person.LastName, Person.FirstName, Product.Name, SalesOrderDetail.OrderQty
ORDER BY SalesOrderDetail.OrderQty DESC, Person.FirstName ASC

/* Задача 5
Вывести содержимое первого заказа каждого клиента
Столбцы: Дата заказа | Фамилия покупателя | Имя покупателя | Содержимое заказа
Упорядочить по дате заказа от новых к старым
В ячейку содержимого заказа нужно объединить все элементы заказа покупателя в следующем формате:
<Имя товара> Количество: <количество в заказе> шт.
<Имя товара> Количество: <количество в заказе> шт.
<Имя товара> Количество: <количество в заказе> шт.
... */

SELECT SalesOrderHeader.OrderDate AS [Date of first order], Person.LastName AS [Last Name], Person.FirstName AS [First Name], CONCAT ('<', Product.Name, '>', ' Quantity: ', '<', SalesOrderDetail.OrderQty, '>', ' PCS.') AS [Order Contents]
FROM Sales.SalesOrderHeader JOIN Person.Person  ON SalesOrderHeader.SalesPersonID=Person.BusinessEntityID
								JOIN Sales.SalesOrderDetail ON SalesOrderHeader.SalesOrderID = SalesOrderDetail.SalesOrderID
									 JOIN Production.Product ON SalesOrderDetail.ProductID = Product.ProductID
WHERE SalesOrderHeader.OrderDate IN (SELECT MIN(SalesOrderHeader.OrderDate) FROM Sales.SalesOrderHeader JOIN Person.Person ON SalesOrderHeader.SalesPersonID=Person.BusinessEntityID GROUP BY Person.LastName, Person.FirstName)
GROUP BY SalesOrderHeader.OrderDate, Person.LastName, Person.FirstName, Product.Name, SalesOrderDetail.OrderQty
ORDER BY SalesOrderHeader.OrderDate DESC

/* Задача 6
Вывести содержимое сотрудников, непосредственный руководитель которых младше и меньше работает в компании
Столбцы: Имя руководителя | Дата приема руководителя на работу| Дата рождения руководителя |
	| Имя сотрудника | Дата приема сотрудника на работу| Дата рождения сотрудника
Поле имя выводит в формате 'Фамилия И.О.'
Упорядочить по уровню в иерархии от директора вниз к сотрудникам
Внутри одного уровня иерархии упорядочить по фамилии руководителя, затем по фамилии сотрудника */

;WITH Leaders AS (SELECT Employee.BusinessEntityID, Employee.OrganizationLevel AS [Leaders Organization Level], vEmployeeDepartment.Department [Department Leader], 
	vEmployee.LastName AS [Leader Last Name], vEmployee.FirstName AS [Leader First Name], vEmployee.MiddleName AS [Leader Middle Name], 
	Employee.HireDate AS [Leader employment date], Employee.BirthDate AS [Leader Birth Date]
FROM HumanResources.vEmployee JOIN HumanResources.Employee ON vEmployee.BusinessEntityID=Employee.BusinessEntityID
								JOIN HumanResources.vEmployeeDepartment ON Employee.BusinessEntityID=vEmployeeDepartment.BusinessEntityID
WHERE Employee.OrganizationLevel IS NOT NULL
GROUP BY vEmployeeDepartment.Department,Employee.OrganizationLevel, Employee.BusinessEntityID, vEmployee.LastName, vEmployee.FirstName,vEmployee.MiddleName, Employee.HireDate, Employee.BirthDate
HAVING Employee.OrganizationLevel = 1)

SELECT CONCAT(Leaders.[Leader Last Name],' ',SUBSTRING(Leaders.[Leader First Name], 1, 1), '.', SUBSTRING(Leaders.[Leader Middle Name], 1, 1)) AS [Leader initials], Leaders.[Leader employment date], Leaders.[Leader Birth Date],
	CONCAT (vEmployee.LastName,' ',SUBSTRING(vEmployee.FirstName, 1, 1), '.', SUBSTRING(vEmployee.MiddleName, 1, 1)) AS [Employee initials], Employee.HireDate AS [Employee employment date], Employee.BirthDate AS [Employee Birth Date]
FROM HumanResources.Employee RIGHT JOIN  HumanResources.vEmployee ON Employee.BusinessEntityID = vEmployee.BusinessEntityID
								RIGHT JOIN HumanResources.vEmployeeDepartment ON Employee.BusinessEntityID=vEmployeeDepartment.BusinessEntityID
									INNER JOIN Leaders ON vEmployeeDepartment.Department = Leaders.[Department Leader]

WHERE CONCAT (vEmployee.LastName,' ',SUBSTRING(vEmployee.FirstName, 1, 1), '.', SUBSTRING(vEmployee.MiddleName, 1, 1)) != CONCAT(Leaders.[Leader Last Name],' ',SUBSTRING(Leaders.[Leader First Name], 1, 1), '.', SUBSTRING(Leaders.[Leader Middle Name], 1, 1)) 
AND Leaders.[Leader employment date]>Employee.HireDate AND Leaders.[Leader Birth Date]>Employee.BirthDate
GROUP BY vEmployeeDepartment.Department, Employee.OrganizationLevel, Leaders.[Leader Last Name], Leaders.[Leader First Name],Leaders.[Leader Middle Name], Leaders.[Leader employment date], Leaders.[Leader Birth Date], 
vEmployee.LastName, vEmployee.FirstName, vEmployee.MiddleName, Employee.HireDate, Employee.BirthDate
ORDER BY Employee.OrganizationLevel DESC, Leaders.[Leader Last Name] ASC, vEmployee.LastName ASC