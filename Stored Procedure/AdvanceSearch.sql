USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[AdvanceSearch]    Script Date: 7/4/2023 3:34:57 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AdvanceSearch]
@userssn nvarchar(50),
@type nvarchar(50),
@price numeric,
@weight float,
@posttype nvarchar(50)
AS
if(@price = 0 AND @weight = 0)
select * from TblPost where Sender_SSN LIKE @userssn AND Type LIKE @type AND Post_type LIKE @posttype
else if(@price = 0 AND @weight != 0)
select * from TblPost where Sender_SSN LIKE @userssn AND Type LIKE @type AND Weight = @weight AND Post_type LIKE @posttype
else if(@price != 0 AND @weight = 0)
select * from TblPost where Sender_SSN LIKE @userssn AND Type LIKE @type AND Price = @price AND Post_type LIKE @posttype
else if(@price != 0 AND @weight != 0)
select * from TblPost where Sender_SSN LIKE @userssn AND Type LIKE @type AND Price = @price AND Weight = @weight AND Post_type LIKE @posttype
