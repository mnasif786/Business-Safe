USE [BusinessSafe]
GO
INSERT [dbo].[Role] ([RoleId], [Description], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy],[ClientId]) VALUES (N'bacf7c01-d210-4dbc-942f-15d8456d3b92', N'User Admin', N'UserAdmin', 0, CAST(0x0000A0860107AC00 AS DateTime), N'16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL,0)
INSERT [dbo].[Role] ([RoleId], [Description], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy],[ClientId]) VALUES (N'952eecb7-2b96-4399-82ae-7e2341d25e51', N'General User', N'GeneralUser', 0, CAST(0x00009FCB00000000 AS DateTime), N'abc55bf0-9a59-47bd-a991-58c5cc2f461a', NULL, NULL,0)
INSERT [dbo].[Role] ([RoleId], [Description], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy],[ClientId]) VALUES (N'1e382767-93dd-47e2-88f2-b3e7f7648642', N'Health and Safety Manager', N'H&SManager', 0, CAST(0x0000A0860107AC00 AS DateTime), N'16ac58fb-4ea4-4482-ac3d-000d607af67c', NULL, NULL,0)



--//@UNDO 
USE [BusinessSafe]
GO

DELETE FROM [Role] WHERE RoleId = '1e382767-93dd-47e2-88f2-b3e7f7648642'
DELETE FROM [Role] WHERE RoleId = 'bacf7c01-d210-4dbc-942f-15d8456d3b92'
DELETE FROM [Role] WHERE RoleId = '952eecb7-2b96-4399-82ae-7e2341d25e51'

