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

/* Созданы покрывающие индексы для таблиц: 
   [Marketing.Prospect](атрибуты: LastName, FirstName, ProspectID, MiddleName, CellPhoneNumber, HomePhoneNumber, WorkPhoneNumber, Demographics, LatestContact, EmailAddress)
   [Marketing.Salesperson](атрибуты: LastName, FirstName)
  
   Удалена сортировка по атрибутам LastName и FirstName таблицы [Marketing.Prospect], т.к. она производится при записи в индекс. 
   Так же руки тянутся удалить ужасный цикл. Если необходимо выводить одно и то же 350 раз, 
   то возможно стоит оформить данный код в пользовательскую процедуру. */