CREATE PROCEDURE SearchUser
@SSN nvarchar(50),
@result int output
AS
if(EXISTS(select * from TblUser where SSN = @ssn))
set @result = 1
else
set @result = 2