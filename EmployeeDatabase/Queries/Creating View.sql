USE EmployeeReport;
GO

--creating view for what we want to put on the worksheet
CREATE VIEW [dbo].[EmployeeReport] AS
	SELECT 
		table1.EmployeeId,
		table1.FirstName,
		table1.LastName,
		table1.JobTitle,
		table1.Department,
		table1.Email,
		table2.CompanyName,
		table2.CompanyPhone,
		table2.City,
		table2."State"
	FROM
		[dbo].[Employee] table1
	INNER JOIN
		[dbo].[Company] table2 ON table2.EmployeeId = table1.EmployeeId
GO