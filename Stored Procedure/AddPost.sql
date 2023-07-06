USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[AddPost]    Script Date: 7/4/2023 3:33:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddPost]
@sendSSN nvarchar(50),
@sendloc nvarchar(50),
@receiveloc nvarchar(50),
@valueable bit,
@type nvarchar(50),
@posttype nvarchar(50),
@weight float,
@phone nvarchar(50),
@price numeric,
@result int output
AS
if(exists(select * from TblUser where SSN = @sendSSN AND Wallet < @price))
set @result = 1
else
begin
insert into TblPost (Sender_SSN,Sender_location,Receiver_location,Type,Valuable,Weight,Post_type,MobileNumber,Price) values (@sendSSN,@sendloc,@receiveloc,@type,@valueable,@weight,@posttype,@phone,@price)
update TblUser set Wallet = Wallet-@price where SSN = @sendSSN
set @result = 2
end