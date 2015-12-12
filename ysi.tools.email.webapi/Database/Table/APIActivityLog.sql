IF OBJECT_ID (N'[dbo].[APIActivityLog]', N'U') IS NULL
BEGIN
	CREATE TABLE APIActivityLog
	(
		Id int IDENTITY(1,1) PRIMARY KEY,
		ClientCode NVARCHAR(50) NULL,
		TemplateCode NVARCHAR(50) NULL,
		EmailContent NVARCHAR(MAX) NULL,
		EmailRecipient NVARCHAR(MAX) NULL,
		AttachmentContent BINARY NULL,
		EmailSenderProvider NVARCHAR(100) NULL,
		ActionType NVARCHAR(50) NULL,
		Token NVARCHAR(MAX) NULL,
		ClientIPAddress NVARCHAR(100) NULL,
		CustomMessage NVARCHAR(MAX) NULL,
		CreatedDateTime DATETIME DEFAULT(GETUTCDATE()) NOT NULL
	)
END