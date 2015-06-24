USE [BusinessSafe]
GO

TRUNCATE TABLE [CauseOfAccident]

SET IDENTITY_INSERT [CauseOfAccident] ON
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (1,'Being caught or carried away by something (or by momentum)', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (2,'Breakage, bursting or collapse of material', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (3,'Electrical problem, explosion or fire', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (4,'Kneeling, sitting or leaning on an object', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (5,'Lifting, carrying, standing up', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (6,'Loss of control of machinery, transport or equipment', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (7,'Overflow, leak, vaporisation or emission of liquid, solid or gaseous product', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (8,'Pushing, pulling', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (9,'Putting down, bending down', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (10,'Shock, fright, violence, aggression', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (11,'Slip, stumble or fall', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (12,'Twisting, turning', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (13,'Walking on a sharp object', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
INSERT INTO [CauseOfAccident] ([Id], [Description], [CreatedBy], [CreatedOn], [LastModifiedOn], [LastModifiedBy], [Deleted]) VALUES (14,'Other cause not listed', N'16ac58fb-4ea4-4482-ac3d-000d607af67c', CAST(0x0000A0860107AC00 AS DateTime), NULL, NULL, 0)
SET IDENTITY_INSERT [CauseOfAccident] OFF

--//@UNDO 

DELETE FROM [CauseOfAccident]