CREATE PROCEDURE SearchPostUser
@postid int,
@userssn nvarchar(50),
@result int OUTPUT
AS
if(NOT EXISTS(select * from TblPost where ID = @postid AND Sender_SSN = @userssn))
set @result = 1
else
begin
select * from TblPost where ID = @postid AND Sender_SSN = @userssn
set @result = 2
end