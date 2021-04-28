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

-- to get Employees, to get Company of employee 
CREATE PROCEDURE [dbo].[Profile_GetList]
AS
	SELECT
		ProfilesId,
		EmployeeId,
		CompanyId
	FROM
		[dbo].[Profiles]
GO

--create type and user defined data table for upsert to add employees and company
-- our ui/form will be able to pass in this type of table
-- then it will be passed into the upsert procedure and confirm if it needs to be updated
CREATE TYPE [dbo].[EmployeeType] AS TABLE
(
	[ProfilesId] INT NOT NULL,
	[EmployeeId] INT NOT NULL,
	[CompanyId] INT NOT NULL

);
GO

-- stored procedure for upsert
CREATE PROCEDURE [dbo].[Employee_Upsert]
--query user defined table
	@EmployeeType [EmployeeType] READONLY
	-- @UserId VARCHAR(50)
AS
	-- information pulled from employee table using parameters from user defined table
	MERGE INTO [dbo].[Profiles] TARGET
	USING
	(
		-- select from user defined table
		SELECT
			[ProfilesId],
			[EmployeeId],
			[CompanyId]
			
			
		FROM
			@EmployeeType

	) 
	-- below is how upsert will match
	AS SOURCE
	ON
	(
			TARGET.ProfilesId = SOURCE.ProfilesId
	)
	-- when matched just update the ids
	WHEN MATCHED THEN
		UPDATE SET
			TARGET.[EmployeeId] = SOURCE.[EmployeeId],
			TARGET.[CompanyId] = SOURCE.[CompanyId]
			
	-- if data is not found in  table then insert new data
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (
			[EmployeeId],
			[CompanyId]
			
		
		)
		VALUES (
			SOURCE.[EmployeeId],
			SOURCE.[CompanyId]
		
		);
GO

DROP PROCEDURE Employee_Upsert;
GO
DROP TYPE EmployeeType;
GO

select * FROM Profiles;