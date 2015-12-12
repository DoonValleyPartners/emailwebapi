IF OBJECT_ID (N'[dbo].[EmailTemplate]', N'U') IS NULL
BEGIN
	CREATE TABLE EmailTemplate
	(
		Id int IDENTITY(1,1) PRIMARY KEY,
		TemplateCode NVARCHAR(50) NULL,
		TemplateDescription NVARCHAR(100) NULL,
		TemplateContent NVARCHAR(MAX) NULL,
		IsDeleted BIT NOT NULL,
		CreatedDateTime DATETIME DEFAULT(GETUTCDATE()) NOT NULL,
		UpdatedDateTime DATETIME DEFAULT(GETUTCDATE()) NOT NULL
	)
END