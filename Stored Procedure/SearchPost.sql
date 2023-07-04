USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[SearchPost]    Script Date: 7/4/2023 3:39:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SearchPost]
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