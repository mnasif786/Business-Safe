USE [BusinessSafe]
GO


DELETE FROM [ClientDocumentType]
GO

-- BusinessSafe System
INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 125)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 131)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 132)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 127)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 128)

-- Reference Library
INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(2, 124)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(2, 126)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(2, 129)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(2, 130)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(2, 133)
GO

--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [ClientDocumentType]
GO

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 125)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 131)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 132)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 127)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 128)

-- Reference Library
INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 124)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 126)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 129)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 130)

INSERT INTO [BusinessSafe].[dbo].[ClientDocumentType]
	( [DocumentGroupId], [Id])
VALUES
	(1, 133)
GO