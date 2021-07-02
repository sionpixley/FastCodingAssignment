USE [Fast]
GO

SET ANSI_NULLS ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_User_Get]
	@Id AS INT = 0
AS 
BEGIN

	SET NOCOUNT ON;

	IF @Id <> 0
	BEGIN
		SELECT *
		FROM [dbo].[User] AS [u]
		WHERE [u].[Id] = @Id;
	END

	ELSE 
	BEGIN
		SELECT DISTINCT *
		FROM [dbo].[User] AS [u]
		ORDER BY [u].[Id] ASC;
	END

END
GO
