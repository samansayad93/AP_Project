USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[SearchPostUser]    Script Date: 7/4/2023 3:38:32 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SearchPostUser]
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