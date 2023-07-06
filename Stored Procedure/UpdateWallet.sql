USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[UpdateWallet]    Script Date: 7/4/2023 3:41:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdateWallet]
@userssn nvarchar(50),
@amount numeric
AS
Update TblUser set Wallet = Wallet + @amount where SSN = @userssn
