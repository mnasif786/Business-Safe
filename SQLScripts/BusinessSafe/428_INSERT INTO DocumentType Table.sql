SET DATEFORMAT YMD

SET IDENTITY_INSERT [DocumentType] ON;

IF NOT EXISTS(SELECT * FROM [dbo].DocumentType WHERE [Id] = 17)
BEGIN			
	INSERT INTO [BusinessSafe].[dbo].[DocumentType] ([Id], [Name],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedOn],[LastModifiedBy]) VALUES
			(17, 'Accident Record', 0, '2013-08-12 11:00:00.000', null, null, null) 
END

SET IDENTITY_INSERT [DocumentType] OFF;
GO