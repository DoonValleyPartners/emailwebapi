IF OBJECT_ID (N'[dbo].[APIExceptionLog]', N'U') IS NULL
BEGIN
	CREATE TABLE APIExceptionLog
	(
		Id int IDENTITY(1,1) PRIMARY KEY,
		ClientCode NVARCHAR(50) NULL,
		ClientIPAddress NVARCHAR(100) NULL,
		EmailSenderProvider NVARCHAR(100) NULL,
		ExceptionMessage NVARCHAR(MAX) NULL,
		ExceptionDetail NVARCHAR(MAX) NULL,
		CreatedDateTime DATETIME DEFAULT(GETUTCDATE()) NOT NULL
	)
END