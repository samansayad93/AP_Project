CREATE PROCEDURE FindCharge
@userssn nvarchar(50)
AS
select Wallet from TblUser where SSN = @userssn
go