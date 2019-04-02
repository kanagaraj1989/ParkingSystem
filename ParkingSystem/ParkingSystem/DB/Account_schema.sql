USE master;

IF NOT EXISTS (SELECT 1 FROM sys.databases WHERE name = N'Parking')
BEGIN
CREATE DATABASE Parking;
END
GO


use Parking
GO


IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME ='UserProfile')
BEGIN
	Create Table UserProfile  
	(  
		UserId int primary key identity(1, 1),  
		UserName varchar(50),  
		Password varchar(50),  
		IsActive bit ,
		UserType int,
		CreatedBy varchar(50)
	) 
END
