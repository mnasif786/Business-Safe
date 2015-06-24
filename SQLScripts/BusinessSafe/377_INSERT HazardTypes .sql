USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT top  1 * FROM [BusinessSafe].[dbo].[HazardType] WHERE ID = 3)
BEGIN

	INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name])
	VALUES (3, 'Fire')
END

--//@UNDO
IF EXISTS (SELECT top  1 * FROM [BusinessSafe].[dbo].[HazardType] WHERE ID = 3)
BEGIN
	DELETE
	FROM [BusinessSafe].[dbo].[HazardType]
	WHERE id = 3
END



