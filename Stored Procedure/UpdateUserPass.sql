USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[UpdateUserPass]    Script Date: 7/4/2023 3:40:50 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateUserPass]
@userssn nvarchar(50),
@username nvarchar(50),
@password nvarchar(50)
AS
Update TblUser set Username = @username,Password = @password where SSN = @userssn
