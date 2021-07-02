USE [Fast]
GO

SET ANSI_NULLS ON
GO

CREATE OR ALTER PROCEDURE [dbo].[sp_Transaction_Get]
	@Id AS BIGINT = 0,
	@Username AS VARCHAR(512) = '',
	@AfterDate AS DATETIME = '2000-01-01T00:00:00.000',
	@BeforeDate AS DATETIME = '2000-01-01T00:00:00.000'
AS
BEGIN
	SET NOCOUNT ON;

	IF @Id <> 0
	BEGIN
		SELECT *
		FROM [dbo].[Transaction] AS [t]
		WHERE [t].[Id] = @Id;
	END

	ELSE IF @Username <> ''
	BEGIN
		SELECT DISTINCT *
		FROM [dbo].[Transaction] AS [t]
		INNER JOIN [dbo].[User] AS [u] ON [u].[Id] = [t].[UserId]
		WHERE [u].[Username] = @Username
		ORDER BY [t].[Id] ASC;
	END

	ELSE IF @AfterDate <> '2000-01-01T00:00:00.000' AND @BeforeDate <> '2000-01-01T00:00:00.000'
	BEGIN
		SELECT DISTINCT *
		FROM [dbo].[Transaction] AS [t]
		WHERE [t].[CreateDate] BETWEEN @AfterDate AND @BeforeDate
		ORDER BY [t].[Id] ASC;
	END

	ELSE 
	BEGIN
		SELECT DISTINCT *
		FROM [dbo].[Transaction] AS [t]
		ORDER BY [t].[Id];
	END

END 
GO
