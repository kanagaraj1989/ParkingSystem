use Parking
go

IF NOT EXISTS (SELECT 1 FROM UserProfile WHERE UserName = 'admin')
BEGIN
insert into UserProfile
(UserName, Password, IsActive, UserType, CreatedBy)
values
('admin', 'password', 1, 1, NULL)
END
GO
