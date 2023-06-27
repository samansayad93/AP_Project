CREATE PROCEDURE UpdatePostComment
@postid int,
@comment nvarchar(MAX)
AS
Update TblPost set Comment = @comment where ID = @postid
go