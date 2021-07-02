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

		INSERT INTO [dbo].[Transaction](
			[UserId],
			[Amount],
			[CreateDate]
		)
		VALUES (
			1,
			45.23,
			'2021-01-02T13:00:00.000'
		),
		(
			1,
			10.00,
			'2021-03-02T13:00:00.000'
		),
		(
			1,
			80.88,
			'2021-03-03T13:00:00.000'
		),
		(
			1,
			90.00,
			'2021-04-03T13:00:00.000'
		),
		(
			1,
			120.00,
			'2021-05-02T13:00:00.000'
		),
		(
			1,
			35.00,
			'2021-05-08T13:00:00.000'
		),
		(
			1,
			50.00,
			'2021-06-02T13:00:00.000'
		),
		(
			1,
			61.30,
			'2021-06-10T13:00:00.000'
		),
		(
			1,
			70.00,
			'2021-06-15T13:00:00.000'
		),
		(
			1,
			120.00,
			'2021-07-01T13:00:00.000'
		),
		(
			2,
			70.00,
			'2021-01-02T13:00:00.000'
		),
		(
			2,
			10.00,
			'2021-03-02T13:00:00.000'
		),
		(
			2,
			80.88,
			'2021-03-03T13:00:00.000'
		),
		(
			2,
			90.00,
			'2021-04-03T13:00:00.000'
		),
		(
			2,
			120.00,
			'2021-05-02T13:00:00.000'
		),
		(
			2,
			67.00,
			'2021-05-08T13:00:00.000'
		),
		(
			2,
			50.00,
			'2021-06-02T13:00:00.000'
		),
		(
			2,
			21.30,
			'2021-06-10T13:00:00.000'
		),
		(
			2,
			75.03,
			'2021-06-15T13:00:00.000'
		),
		(
			2,
			120.00,
			'2021-07-01T13:00:00.000'
		),
		(
			3,
			12.50,
			'2021-01-02T13:00:00.000'
		),
		(
			3,
			10.00,
			'2021-03-02T13:00:00.000'
		),
		(
			3,
			12.90,
			'2021-03-03T13:00:00.000'
		),
		(
			3,
			80.23,
			'2021-04-03T13:00:00.000'
		),
		(
			3,
			62.12,
			'2021-05-02T13:00:00.000'
		),
		(
			3,
			158.00,
			'2021-05-08T13:00:00.000'
		),
		(
			3,
			15.98,
			'2021-06-02T13:00:00.000'
		),
		(
			3,
			51.20,
			'2021-06-10T13:00:00.000'
		),
		(
			3,
			75.03,
			'2021-06-15T13:00:00.000'
		),
		(
			3,
			120.00,
			'2021-07-01T13:00:00.000'
		);

	COMMIT TRANSACTION
END TRY
BEGIN CATCH
	SELECT ERROR_MESSAGE() AS Error;
	ROLLBACK TRANSACTION
END CATCH
GO
