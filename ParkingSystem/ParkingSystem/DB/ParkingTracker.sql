use Parking
go

IF NOT EXISTS ( SELECT 1 FROM INFORMATION_SCHEMA.TABLES
	WHERE TABLE_NAME = 'ParkingLogs')
BEGIN
	CREATE TABLE ParkingLogs(
	    Id int primary key identity(1, 1),
		PlateNumber varchar(30),
	    Imagecdn varchar(100),
		INAgentMACID varchar(50),
		OUTAgentMACID varchar(50),
		[Status] varchar(20),
		InGateSessionID varchar(100),
		OutGateSessionID varchar(100),
		InTimeStamp DateTime,
		OutTimeStamp DateTime,
		CONSTRAINT FK_ParkingLogs_UserProfile_INAgentMACID
		FOREIGN KEY (INAgentMACID) REFERENCES UserProfile(UserName),
		CONSTRAINT FK_ParkingLogs_UserProfile_OUTAgentMACID
		FOREIGN KEY (OUTAgentMACID) REFERENCES UserProfile(UserName)
	)
END
GO