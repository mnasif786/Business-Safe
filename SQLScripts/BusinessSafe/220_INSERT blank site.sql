USE [BusinessSafe]

SET IDENTITY_INSERT [dbo].[SiteStructureElement] ON
GO

IF NOT EXISTS (SELECT * FROM [SiteStructureElement] WHERE [Id] = 0)
BEGIN
	INSERT [dbo].[SiteStructureElement] ([Id], [SiteId], [ParentId], [ClientId], [Name], [Reference], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy], [SiteType]) 
	VALUES (0, 0, NULL, 0, N'Blank Site', N'Blank Site Reference', 0, CAST(0x0000A04E00CD79B4 AS DateTime), NULL, NULL, NULL, 1)
END

SET IDENTITY_INSERT [dbo].[SiteStructureElement] OFF
GO

--//@UNDO 

IF EXISTS (SELECT * FROM [SiteStructureElement] WHERE [Id] = 0)
BEGIN
	DELETE FROM [SiteStructureElement] WHERE [Id] = 0
END
