CREATE PROCEDURE FindSSNByUsername
@username nvarchar(50)
AS
select SSN from TblUser where Username = @username
go