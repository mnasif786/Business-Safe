SET DATEFORMAT YMD

SET IDENTITY_INSERT [ResponsibilityCategory] ON;

IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 12)
BEGIN			
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
			(12, 'Care', 0, '2013-06-25 11:00:00.000', null, null, '2013-06-25 12:00:00.000') 
END

SET IDENTITY_INSERT [ResponsibilityCategory] OFF;

IF EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 12)
BEGIN	
	DELETE FROM ResponsibilityCategory WHERE Id = 12
END

GO