USE [DbPost]
GO
/****** Object:  StoredProcedure [dbo].[UpdatePostComment]    Script Date: 7/4/2023 3:40:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[UpdatePostComment]
@postid int,
@comment nvarchar(MAX)
AS
Update TblPost set Comment = @comment where ID = @postid
