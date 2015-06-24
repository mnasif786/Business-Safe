USE [BusinessSafe]
GO

SET IDENTITY_INSERT [SourceOfFuel] ON

IF NOT EXISTS(SELECT * FROM [dbo].[SourceOfFuel] WHERE [Name] = 'Oxygen Ventilated')
BEGIN
	INSERT [SourceOfFuel] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (100, 'Oxygen Ventilated', 0, GETDATE(), '790D8CC9-04F8-4643-90EE-FAED4BA711EC')
END

IF NOT EXISTS(SELECT * FROM [dbo].[SourceOfFuel] WHERE [Name] = 'Oxygen Natural')
BEGIN
	INSERT [SourceOfFuel] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy]) VALUES (101, 'Oxygen Natural', 0, GETDATE(), '790D8CC9-04F8-4643-90EE-FAED4BA711EC')
END

SET IDENTITY_INSERT [SourceOfFuel] OFF

--//@UNDO 
USE [BusinessSafe]
GO

IF EXISTS(SELECT * FROM [dbo].[SourceOfFuel] WHERE [Id] = 100)
BEGIN
	DELETE FROM [SourceOfFuel] WHERE [Id] = 100
END

IF EXISTS(SELECT * FROM [dbo].[SourceOfFuel] WHERE [Id] = 101)
BEGIN
	DELETE FROM [SourceOfFuel] WHERE [Id] = 101
END