USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[FindEmailBySSN]    Script Date: 7/4/2023 3:35:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[FindEmailBySSN]
@userssn nvarchar(50)
AS
select Email from TblUser where SSN = @userssn
