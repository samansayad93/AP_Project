CREATE PROCEDURE UpdateWallet
@userssn nvarchar(50),
@amount numeric
AS
Update TblUser set Wallet = Wallet + @amount where SSN = @userssn
GO