USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[AddUser]    Script Date: 7/4/2023 3:33:48 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddUser]
@name nvarchar(50),
@lastname nvarchar(50),
@ssn nvarchar(50),
@mobile nvarchar(50),
@email nvarchar(50),
@username nvarchar(50),
@password nvarchar(50),
@result int output
AS
if(EXISTS(select * from TblUser where SSN = @ssn))
set @result = 1
else if(EXISTS(select * from TblUser where Username = @username))
set @result = 2
else
begin
insert into TblUser (Name,LastName,SSN,Mobile,Email,Username,Password) values (@name,@lastname,@ssn,@mobile,@email,@username,@password)
set @result = 3
end