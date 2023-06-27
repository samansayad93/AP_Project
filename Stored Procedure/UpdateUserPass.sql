CREATE PROCEDURE UpdateUserPass
@userssn nvarchar(50),
@username nvarchar(50),
@password nvarchar(50)
AS
Update TblUser set Username = @username,Password = @password where SSN = @userssn
go