USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[FindCharge]    Script Date: 7/4/2023 3:35:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[FindCharge]
@userssn nvarchar(50)
AS
select Wallet from TblUser where SSN = @userssn
