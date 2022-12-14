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

SELECT TOP 10 City AS [City], COUNT(DISTINCT BusinessEntityID) AS [Priority]
FROM Sales.vIndividualCustomer
WHERE City NOT IN (SELECT City FROM Sales.vStoreWithAddresses)
GROUP BY City
ORDER BY COUNT(DISTINCT BusinessEntityID) DESC

/* Задача 7
Написать хранимую процедуру, с тремя параметрами и результирующим набором данных 
входные параметры - две даты, с и по 
выходной параметр - количество найденных записей 
Результирующий набор содержит записи всех холостых мужчин-сотрудников, родившихся в диапазон указанных дат */

CREATE PROCEDURE HumanResources.SingleMenInAgeRange (
	@TimeStart date,
	@TimeEnd date,
	@CountFoundEmployees int OUTPUT
)
AS
BEGIN
	SELECT @CountFoundEmployees = COUNT(Employee.BusinessEntityID) 
	FROM  HumanResources.Employee 
	WHERE Employee.Gender = 'M' AND 
		Employee.MaritalStatus = 'S' AND 
			Employee.BirthDate >= @TimeStart AND
			Employee.BirthDate <= @TimeEnd
	SELECT Employee.BusinessEntityID, Employee.NationalIDNumber, Employee.LoginID, 
		Employee.OrganizationNode, Employee.OrganizationLevel,Employee.JobTitle,Employee.BirthDate,Employee.MaritalStatus,Employee.Gender,Employee.HireDate, 
		Employee.SalariedFlag,Employee.VacationHours,Employee.SickLeaveHours,Employee.CurrentFlag,Employee.rowguid,Employee.ModifiedDate
		FROM  HumanResources.Employee 
	WHERE Employee.Gender = 'M' AND 
		Employee.MaritalStatus = 'S' AND 
			Employee.BirthDate >= @TimeStart AND
			Employee.BirthDate <= @TimeEnd
	GROUP BY Employee.BusinessEntityID, Employee.NationalIDNumber, Employee.LoginID, 
		Employee.OrganizationNode, Employee.OrganizationLevel,Employee.JobTitle,Employee.BirthDate,Employee.MaritalStatus,Employee.Gender,Employee.HireDate, 
		Employee.SalariedFlag,Employee.VacationHours,Employee.SickLeaveHours,Employee.CurrentFlag,Employee.rowguid,Employee.ModifiedDate;
END