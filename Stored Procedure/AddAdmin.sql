CREATE PROCEDURE AddAdmin
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
