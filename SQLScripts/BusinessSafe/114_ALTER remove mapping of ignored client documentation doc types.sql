USE [BusinessSafe]
GO

GO
-- BusinessSafe System
DELETE FROM [DocHandlerDocumentType]
WHERE id in (143, 144)
GO


--//@UNDO 
USE [BusinessSafe]
GO

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType] ([Id], [DocumentGroupId])
SELECT	143, 1
UNION ALL
SELECT	144, 1
GO
