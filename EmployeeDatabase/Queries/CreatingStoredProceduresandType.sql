USE EmployeeReport;
GO


-- to get Employees, to get Company of employee 
CREATE PROCEDURE [dbo].[Employee_GetList]
AS
	SELECT
		EmployeeId,
		FirstName,
		LastName,
		JobTitle
	FROM
		[dbo].[Employee]
GO

CREATE PROCEDURE [dbo].[Company_GetList]
AS
	SELECT
		CompanyId,
		CompanyName,
		CompanyPhone
	FROM
		[dbo].[Company]
GO

--create type and user defined data table for upsert to add employees and company
-- our ui/form will be able to pass in this type of table
-- then it will be passed into the upsert procedure and confirm if it needs to be updated
CREATE TYPE [dbo].[EmployeeType] AS TABLE
(
	[EmployeeId] INT NOT NULL
	


);
GO

-- stored procedure for upsert
CREATE PROCEDURE [dbo].[Employee_Upsert]
--query user defined table
	@EmployeeType [EmployeeType] READONLY
	-- @UserId VARCHAR(50)
AS
	-- information pulled from employee table using parameters from user defined table
	MERGE INTO [dbo].[Company] TARGET
	USING
	(
		-- select from user defined table
		SELECT
			[EmployeeId]
			
		FROM
			@EmployeeType

	) 
	-- below is how upsert will match
	AS SOURCE
	ON
	(
			TARGET.[EmployeeId] = SOURCE.[EmployeeId]
	)
	-- when matched just update the ids
	WHEN MATCHED THEN
		UPDATE SET
			TARGET.[EmployeeId] = SOURCE.[EmployeeId]
			
	-- if data is not found in  table then insert new data
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (
			[EmployeeId]
			
		
		)
		VALUES (
			SOURCE.[EmployeeId]
		
		
		);
GO
