USE [BusinessSafe]
GO

SET IDENTITY_INSERT [BodyPart] ON
INSERT INTO [BodyPart] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (1, 'Test Body Part 1', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [BodyPart] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (2, 'Test Body Part 2', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [BodyPart] OFF

--//@UNDO 

DELETE FROM [BodyPart]