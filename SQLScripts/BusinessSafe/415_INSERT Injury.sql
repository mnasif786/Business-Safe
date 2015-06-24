USE [BusinessSafe]
GO

SET IDENTITY_INSERT [Injury] ON
INSERT INTO [Injury] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (1, 'Test Injury 1', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [Injury] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (2, 'Test Injury 2', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [Injury] OFF

--//@UNDO 

DELETE FROM [Injury]