USE [BusinessSafe]
GO

GO
-- BusinessSafe System
DELETE FROM [DocHandlerDocumentType]
GO

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType] ([Id], [DocumentGroupId])
SELECT	136, 1
UNION ALL
SELECT	137, 1
UNION ALL
SELECT	138, 1
UNION ALL
SELECT	139, 1
UNION ALL
SELECT	140, 1
UNION ALL
SELECT	141, 1
UNION ALL
SELECT	142, 1
UNION ALL
SELECT	143, 1
UNION ALL
SELECT	144, 1
UNION ALL
SELECT	124, 2
UNION ALL
SELECT	126, 2
UNION ALL
SELECT	129, 2
UNION ALL
SELECT	130, 2
UNION ALL
SELECT	133, 2
GO

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [DocHandlerDocumentType]
GO

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 125)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 131)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 132)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 127)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 128)

-- Reference Library
INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 124)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 126)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 129)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 130)

INSERT INTO [BusinessSafe].[dbo].[DocHandlerDocumentType]
	([Id], [DocumentGroupId])
VALUES
	(1, 133)
GO