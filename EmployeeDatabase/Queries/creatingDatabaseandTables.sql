--Create Database EmployeeReport
CREATE DATABASE EmployeeReport;

--Use EmployeeReport Database
USE EmployeeReport;

--Employee Info
CREATE TABLE [dbo].[Employee] (
	EmployeeId INT NOT NULL,
	FirstName NVARCHAR NOT NULL,
	LastName NVARCHAR NOT NULL,
	Email NVARCHAR NOT NULL,
	JobTitle NVARCHAR,
	Department VARCHAR(50),
	PRIMARY KEY (EmployeeId)
);

--Company table for Company info relating to employee
CREATE TABLE [dbo].[Company] (
	CompanyId INT IDENTITY( 1, 1)NOT NULL,
	EmployeeId INT NOT NULL,
	CompanyName NVARCHAR(50) NOT NULL,
	CompanyPhone NVARCHAR(15),
	"State" NVARCHAR(50),
	City VARCHAR(50),
	PRIMARY KEY (CompanyId),
	FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId)
);

--Making corrections to tables for data transfer from csv
ALTER TABLE [dbo].[Employee] ALTER COLUMN FirstName NVARCHAR (50) Not Null;
ALTER TABLE [dbo].[Employee] ALTER COLUMN LastName NVARCHAR (50) Not Null;
ALTER TABLE [dbo].[Employee] ALTER COLUMN Email NVARCHAR (50) Not Null;
ALTER TABLE [dbo].[Employee] ALTER COLUMN JobTitle NVARCHAR (50);

ALTER TABLE [dbo].[Company] ALTER COLUMN CompanyName NVARCHAR (50) Not Null;
ALTER TABLE [dbo].[Company] ADD CompanyId INT IDENTITY( 1, 1);
ALTER TABLE [dbo].[Company] ALTER COLUMN "State" NVARCHAR (50) Not Null;

DROP TABLE [dbo].Company;


DELETE FROM Employee;
