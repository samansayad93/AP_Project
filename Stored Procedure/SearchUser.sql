USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[SearchUser]    Script Date: 7/4/2023 3:39:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[SearchUser]
@SSN nvarchar(50),
@result int output
AS
if(EXISTS(select * from TblUser where SSN = @SSN))
set @result = 1
else
set @result = 2