IF OBJECT_ID('[dbo].[sp_GetClient]', 'P') IS NOT NULL
	DROP PROC [dbo].[sp_GetClient]
GO
CREATE PROCEDURE [dbo].[sp_GetClient]
(
	@APIKey NVARCHAR(MAX)
)
AS
BEGIN
	SET NOCOUNT OFF;
	SELECT * FROM [Client] WHERE APIKey = @APIKey AND IsActive = 1
	UPDATE [Client] SET LastAccessedDateTime = GETUTCDATE() WHERE APIKey = @APIKey AND IsActive = 1
END