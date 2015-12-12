IF OBJECT_ID('[dbo].[sp_AddAPIExceptionLog]', 'P') IS NOT NULL
	DROP PROC [dbo].[sp_AddAPIExceptionLog]
GO
CREATE PROCEDURE [dbo].[sp_AddAPIExceptionLog]
(
	@ClientCode NVARCHAR(50),
	@EmailSenderProvider NVARCHAR(100),
	@ClientIPAddress NVARCHAR(100),
	@ExceptionMessage NVARCHAR(MAX),
	@ExceptionDetail NVARCHAR(MAX)
)
AS
BEGIN
	INSERT INTO APIExceptionLog
				(
					ClientCode,
					EmailSenderProvider,
					ClientIPAddress,
					ExceptionMessage,
					ExceptionDetail,
					CreatedDateTime
				)
				VALUES
				(
					@ClientCode,
					@EmailSenderProvider,
					@ClientIPAddress,
					@ExceptionMessage,
					@ExceptionDetail,
					GETUTCDATE()
				)
END