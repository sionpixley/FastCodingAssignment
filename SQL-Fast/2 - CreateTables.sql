USE [Fast]
GO

SET ANSI_NULLS ON
GO

BEGIN TRY
	BEGIN TRANSACTION

		CREATE TABLE [dbo].[User](
			[Id] INT IDENTITY NOT NULL,
			[FirstName] VARCHAR(255) NOT NULL,
			[MiddleInitial] CHAR(1) NULL,
			[LastName] VARCHAR(255) NULL,
			[StreetAddress] VARCHAR(512) NULL,
			[City] VARCHAR(255) NULL,
			[State] CHAR(2) NULL,
			[ZipCode] CHAR(5) NULL,
			[Username] VARCHAR(512) NOT NULL,
			[Passwd] CHAR(60) NOT NULL,
			[ModifiedDate] DATETIME NOT NULL DEFAULT CONVERT(VARCHAR(33), GETUTCDATE(), 126),

			CONSTRAINT PK_dbo_User PRIMARY KEY([Id]),
			CONSTRAINT Unique_dbo_User_Username UNIQUE([Username])
		);

		CREATE TABLE [dbo].[Transaction](
			[Id] BIGINT IDENTITY NOT NULL,
			[UserId] INT NOT NULL,
			[Amount] DECIMAL(8,2) NOT NULL DEFAULT 0.0,
			[CreateDate] DATETIME NOT NULL DEFAULT CONVERT(VARCHAR(33), GETUTCDATE(), 126),

			CONSTRAINT PK_dbo_Transaction PRIMARY KEY([Id]),
			CONSTRAINT FK_dbo_Transaction_User FOREIGN KEY([UserId]) REFERENCES [dbo].[User]([Id])
		);

		INSERT INTO [dbo].[User](
			[FirstName],
			[MiddleInitial],
			[LastName],
			[StreetAddress],
			[City],
			[State],
			[ZipCode],
			[Username],
			[Passwd],
			[ModifiedDate]
		)
		VALUES (
			'Sion',
			'M',
			'Pixley',
			'REDACTED',
			'Chesterfield',
			'MO',
			'63017',
			'sionpixley',
			'BCrypt',
			'2021-01-13T19:00:00.000'
		),
		(
			'Tom',
			'O',
			'Wellington',
			'87 W Fischer St',
			'Troy',
			'IN',
			'98124',
			'tomowell',
			'BCrypt',
			'2021-01-14T12:00:00.000'
		),
		(
			'Alice',
			'Y',
			'Rather',
			'9120 S Kingsway Dr',
			'Safe',
			'FL',
			'12096',
			'ARatherNiceDay',
			'BCrypt',
			'2021-01-16T01:23:50.013'
		);

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() AS Error;
	ROLLBACK TRANSACTION
END CATCH
GO
