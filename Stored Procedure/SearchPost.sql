CREATE PROCEDURE SearchPost
@postid int,
@result int OUTPUT

AS
if(NOT EXISTS(Select * from TblPost where ID = @postid))
set @result = 1
else
begin
Select * from TblPost where ID = @postid
set @result = 2
end