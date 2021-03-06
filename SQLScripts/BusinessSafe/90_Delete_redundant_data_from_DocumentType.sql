USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM [DocumentType] WHERE [Name] = 'Document Type3')
BEGIN
	DELETE FROM [DocumentType]
	WHERE [Name] = 'Document Type3'
END
GO

--//@UNDO
IF NOT EXISTS (SELECT * FROM [DocumentType] WHERE [Name] = 'Document Type3')
BEGIN
	SET IDENTITY_INSERT [DocumentType] ON;
	Go

	INSERT INTO [BusinessSafe].[dbo].[DocumentType]
           ([ID]
           ,[Name] 
           ,[Deleted]
           ,[CreatedOn]
           ,[CreatedBy])
     VALUES
           (3
           ,'Document Type3'
           ,0           
           ,getdate()
           ,null)
     GO

	SET IDENTITY_INSERT [DocumentType] OFF;
	GO
END
GO