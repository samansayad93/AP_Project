USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[LOGIN]    Script Date: 7/4/2023 3:37:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[LOGIN]
@username nvarchar(50),
@password nvarchar(50),
@result int output
AS
if(exists(select * from TblAdmin where Username = @username AND Password = @password))
set @result = 1
else if(exists(select * from TblUser where Username = @username AND Password = @password))
set @result = 2
else
set @result = 3
