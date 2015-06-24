USE [BusinessSafe]
GO

TRUNCATE TABLE [AccidentType]

SET IDENTITY_INSERT [AccidentType] ON
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (1,'Contact with electricity', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (2,'Contact with machinery', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (3,'Drowned or asphyxiated', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (4,'Exposed to explosion', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (5,'Exposed to fire', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (6,'Exposure to harmful substance', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (7,'Fall from height', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (8,'Injured by animal', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (9,'Lifting and handling injuries', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (10,'Physical assault', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (11,'Slip, trip, fall same level', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (12,'Struck against', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (13,'Struck by moving vehicle', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (14,'Struck by object', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (15,'Trapped by something collapsing', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [AccidentType] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (16,'Another kind of accident', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [AccidentType] OFF

--//@UNDO 

DELETE FROM [AccidentType]