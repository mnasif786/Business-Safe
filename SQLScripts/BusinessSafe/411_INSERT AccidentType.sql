USE [BusinessSafe]
GO

SET IDENTITY_INSERT [AccidentType] ON
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (1, 'Test Accident Type 1', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (2, 'Test Accident Type 2', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [AccidentType] OFF

--//@UNDO 

DELETE FROM [AccidentType]