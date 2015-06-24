USE [BusinessSafe]
GO

SET IDENTITY_INSERT [Jurisdiction] ON
INSERT INTO [Jurisdiction] ([Id], [Name], [Order], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (1, 'GB', 1, N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [Jurisdiction] ([Id], [Name], [Order], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (2, 'ROI', 3, N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL,0)
INSERT INTO [Jurisdiction] ([Id], [Name], [Order], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (3, 'NI', 2, N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL,0)
INSERT INTO [Jurisdiction] ([Id], [Name], [Order], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (5, 'Jersey',6,  N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL,0)
INSERT INTO [Jurisdiction] ([Id], [Name], [Order], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (8, 'Guernsey', 5, N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL,0)
INSERT INTO [Jurisdiction] ([Id], [Name], [Order], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (9, 'IOM', 4, N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL,0)
SET IDENTITY_INSERT [Jurisdiction] OFF

--//@UNDO 

DELETE FROM [Jurisdiction]