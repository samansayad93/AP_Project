CREATE PROCEDURE FindEmailBySSN
@userssn nvarchar(50)
AS
select Email from TblUser where SSN = @userssn
go