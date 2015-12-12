IF OBJECT_ID('[dbo].[sp_AddAPILog]', 'P') IS NOT NULL
	DROP PROC [dbo].[sp_AddAPILog]
GO
CREATE PROCEDURE [dbo].[sp_AddAPILog]
(
	@ClientCode NVARCHAR(50),
	@TemplateCode NVARCHAR(50),
	@EmailContent NVARCHAR(MAX),
	@EmailRecipient NVARCHAR(MAX),
	@AttachmentContent BINARY,
	@EmailSenderProvider NVARCHAR(100),
	@ActionType NVARCHAR(50),
	@Token NVARCHAR(MAX),
	@ClientIPAddress NVARCHAR(100),
	@CustomMessage NVARCHAR(MAX)
)
AS
BEGIN
	INSERT INTO APIActivityLog
				(
					ClientCode,
					TemplateCode,
					EmailContent,
					EmailRecipient,
					AttachmentContent,
					EmailSenderProvider,
					ActionType,
					Token,
					ClientIPAddress,
					CustomMessage,
					CreatedDateTime
				)
				VALUES
				(
					@ClientCode,
					@TemplateCode,
					@EmailContent,
					@EmailRecipient,
					@AttachmentContent,
					@EmailSenderProvider,
					@ActionType,
					@Token,
					@ClientIPAddress,
					@CustomMessage,
					GETUTCDATE()
				)
END