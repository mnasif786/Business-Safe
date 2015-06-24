SET DATEFORMAT YMD

SET IDENTITY_INSERT [ResponsibilityCategory] ON;

IF NOT EXISTS(SELECT * FROM [dbo].[ResponsibilityCategory] WHERE [Id] = 13)
BEGIN			
	INSERT INTO [BusinessSafe].[dbo].[ResponsibilityCategory] ([Id], [Category],[Deleted],[CreatedOn],[CreatedBy],[LastModifiedBy],[LastModifiedOn]) VALUES
			(13, 'Transport', 0, '2013-07-24 11:00:00.000', null, null, '2013-07-24 12:00:00.000') 
END

SET IDENTITY_INSERT [ResponsibilityCategory] OFF;
GO