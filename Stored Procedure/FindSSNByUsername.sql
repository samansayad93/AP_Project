USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[FindSSNByUsername]    Script Date: 7/4/2023 3:37:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[FindSSNByUsername]
@username nvarchar(50)
AS
select SSN from TblUser where Username = @username
