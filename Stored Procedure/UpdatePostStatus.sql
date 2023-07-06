USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePostStatus]    Script Date: 7/4/2023 3:40:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdatePostStatus]
@postid int,
@status nvarchar(50)
AS
update TblPost set State = @status where ID = @postid
