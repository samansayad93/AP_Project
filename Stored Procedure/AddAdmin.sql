USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[AddAdmin]    Script Date: 7/4/2023 3:32:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[AddAdmin]
@ID int,
@name nvarchar(50),
@lastname nvarchar(50),
@username nvarchar(50),
@password nvarchar(50),
@email nvarchar(50),
@result int output

AS
if(EXISTS(select * from TblAdmin where ID = @ID))
set @result = 1
else
begin
insert into TblAdmin (ID,Name,LastName,Username,Password,Email) values (@ID,@name,@lastname,@username,@password,@email)
set @result = 0
end
