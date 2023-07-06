USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[FindPostIDBySSN]    Script Date: 7/4/2023 3:36:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[FindPostIDBySSN]
@userssn nvarchar(50),
@price numeric
AS
select ID from TblPost where Sender_SSN = @userssn AND Price = @price
