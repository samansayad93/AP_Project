CREATE PROCEDURE UpdatePostStatus
@postid int,
@status nvarchar(50)
AS
update TblPost set State = @status where ID = @postid
go