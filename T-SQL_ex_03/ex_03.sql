--SET STATISTICS IO ON
--SET STATISTICS TIME ON


-- Задача 1
DROP INDEX IX_WebLog_SessionStart ON Marketing.WebLog
--
CREATE INDEX IX_WebLog_SessionStart ON Marketing.WebLog (SessionStart ASC, ServerID ASC, SessionID, UserName)
--
DECLARE @StartTime datetime2 = '2010-08-30 16:27';

SELECT TOP(5000) wl.SessionID, wl.ServerID, wl.UserName 
FROM Marketing.WebLog AS wl
WHERE wl.SessionStart >= @StartTime 

GO


/* Создан покрывающий индекс для таблицы [Marketing.WebLog](атрибуты: SessionStart, ServerID, SessionID, UserName).
   Удалена сортировка по атрибутам SessionStart и ServerID, т.к. она производится при записи в индекс. */


-- Задача 2
CREATE INDEX IX_PostalCode ON Marketing.PostalCode(PostalCode)
INCLUDE (Country, StateCode)
--
SELECT PostalCode, Country
FROM Marketing.PostalCode 
WHERE StateCode = 'KY'
GO

/* Создан покрывающий индекс для таблицы [Marketing.PostalCode](атрибуты: PostalCode, Country, StateCode). 
   Удалена бесмысленная сортировка, т.к. результат без неё аналогичный. */


-- Задача 3	
CREATE INDEX IX_Prospect ON Marketing.Prospect (LastName ASC, FirstName ASC)
INCLUDE (ProspectID, MiddleName, CellPhoneNumber, HomePhoneNumber, WorkPhoneNumber, Demographics, LatestContact, EmailAddress)
--
CREATE INDEX IX_SalespersonFullName ON Marketing.Salesperson (LastName ASC, FirstName ASC)
--
DECLARE @Counter INT = 0;
WHILE @Counter < 350
BEGIN
  SELECT p.LastName, p.FirstName 
  FROM Marketing.Prospect AS p
  INNER JOIN Marketing.Salesperson AS sp
  ON p.LastName = sp.LastName
  
  SELECT * 
  FROM Marketing.Prospect AS p
  WHERE p.LastName = 'Smith';
  SET @Counter += 1;
END;

/* Созданы покрывающие индексы для таблицы [Marketing.Prospect](атрибуты: LastName, FirstName, ProspectID, MiddleName, CellPhoneNumber, 
                                                                          HomePhoneNumber, WorkPhoneNumber, Demographics, LatestContact, EmailAddress)
										 и [Marketing.Salesperson](атрибуты: LastName, FirstName)
   Удалена сортировка по атрибутам LastName и FirstName таблицы [Marketing.Prospect], т.к. она производится при записи в индекс. 
   Так же руки тянутся удалить ужасный цикл. Если необходимо выводить одно и то же 350 раз, 
   то возможно стоит оформить данный код в пользовательскую процедуру. */


  -- Задача 4
CREATE INDEX IX_Product ON Marketing.Product (ProductModelID, ProductID, SubcategoryID)
CREATE INDEX IX_ProductModel ON Marketing.ProductModel (ProductModelID, ProductModel)
CREATE INDEX IX_Subcategory ON Marketing.Subcategory (CategoryID, SubcategoryName)
CREATE INDEX IX_Category ON Marketing.Category (CategoryID, CategoryName)

SELECT
	c.CategoryName,
	sc.SubcategoryName,
	pm.ProductModel,
	COUNT(p.ProductID) AS ModelCount
FROM Marketing.ProductModel pm
	JOIN Marketing.Product p
		ON p.ProductModelID = pm.ProductModelID
	JOIN Marketing.Subcategory sc
		ON sc.SubcategoryID = p.SubcategoryID
	JOIN Marketing.Category c
		ON c.CategoryID = sc.CategoryID
GROUP BY c.CategoryName,
	sc.SubcategoryName,
	pm.ProductModel
HAVING COUNT(p.ProductID) > 1

/*
Созданы покрывающие индексы для таблиц: [Marketing.Product](атрибуты: ProductModelID, ProductID, SubcategoryID)
									    [Marketing.ProductModel](атрибуты: ProductModelID, ProductModel)
										[Marketing.Subcategory](атрибуты: CategoryID, SubcategoryName)
										[Marketing.Category](атрибуты: CategoryID, CategoryName)

*/