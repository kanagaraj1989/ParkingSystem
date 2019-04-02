use Parking
go

IF NOT EXISTS ( SELECT 1 FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'ParkingLogs')
BEGIN
	CREATE TABLE ParkingLogs(
	    Id int primary key identity(1, 1),
		OUTAgentMACID varchar(50),
		INAgentMACID varchar(50),
		SessionID varchar(100),
		PlateNumber varchar(30),
		imagecdn varchar(100),
		[TimeStamp] DateTime
	)
END
GO