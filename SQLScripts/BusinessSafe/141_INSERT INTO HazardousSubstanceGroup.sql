USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM [dbo].[HazardousSubstanceGroup] WHERE [Id] < 6)
BEGIN
	SET IDENTITY_INSERT [dbo].[HazardousSubstanceGroup] ON
	INSERT [dbo].[HazardousSubstanceGroup] ([Id], [Code], [CreatedBy], [CreatedOn], [Deleted]) VALUES (1, N'A', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	INSERT [dbo].[HazardousSubstanceGroup] ([Id], [Code], [CreatedBy], [CreatedOn], [Deleted]) VALUES (2, N'B', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	INSERT [dbo].[HazardousSubstanceGroup] ([Id], [Code], [CreatedBy], [CreatedOn], [Deleted]) VALUES (3, N'C', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	INSERT [dbo].[HazardousSubstanceGroup] ([Id], [Code], [CreatedBy], [CreatedOn], [Deleted]) VALUES (4, N'D', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	INSERT [dbo].[HazardousSubstanceGroup] ([Id], [Code], [CreatedBy], [CreatedOn], [Deleted]) VALUES (5, N'E', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0FD0112D6BC AS DateTime), 0)
	SET IDENTITY_INSERT [dbo].[HazardousSubstanceGroup] OFF
END

--//@UNDO 

DELETE FROM [dbo].[HazardousSubstanceGroup] WHERE [Id] < 6
