USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SourceOfFuel')
BEGIN
	CREATE TABLE [dbo].[SourceOfFuel](
		[Id] bigint IDENTITY(1,1) NOT NULL,
		[Name] nvarchar (200) NOT NULL,
		[RiskAssessmentId] [bigint] NULL,
		[CompanyId] [bigint] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_SourceOfFuel] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [SourceOfFuel] TO AllowAll
END
GO

SET IDENTITY_INSERT [dbo].[SourceOfFuel] ON

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES 
           (1, 'Textiles', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (2, 'Wood', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (3, 'Paper', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (4, 'Card', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (5, 'Plastics', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)
 
INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (6, 'Rubber', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (7, 'PU foam', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)
 
INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (8, 'Furniture', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (9, 'Fixtures and fittings', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)


INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (10, 'Packaging', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (11, 'Waste materials, etc', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (12, 'Flammable liquids: solvents (petrol, white spirit, methylated spirits, paraffin, thinners etc), paints, varnish, adhesives etc. ', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (13, 'Flammable gases: LPG, acetylene', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (14, 'Fuel oil', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfFuel] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (15, 'Coal', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

GO



SET IDENTITY_INSERT [dbo].[SourceOfFuel] OFF
GO


USE [BusinessSafe]
GO

print 'Create [FireRiskAssessmentSourceOfFuels]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'FireRiskAssessmentSourceOfFuels' AND TYPE = 'U')
BEGIN
	CREATE TABLE [FireRiskAssessmentSourceOfFuels](
		[RiskAssessmentId] [bigint] NOT NULL,
		[FireRiskAssessmentSourceOfFuelId] [bigint] NOT NULL,
	 CONSTRAINT [PK_FireRiskAssessmentSourceOfFuels] PRIMARY KEY CLUSTERED 
	(
		[RiskAssessmentId] ASC,
		[FireRiskAssessmentSourceOfFuelId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [FireRiskAssessmentSourceOfFuels] TO [AllowAll]

GO






--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SourceOfFuel')
BEGIN
	DROP TABLE [dbo].[SourceOfFuel]
END
GO


DELETE FROM [BusinessSafe].[dbo].[SourceOfFuel] WHERE ID >= 1 AND ID <=15
GO


IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'FireRiskAssessmentSourceOfFuels' AND TYPE = 'U')
BEGIN
	DROP TABLE [FireRiskAssessmentSourceOfFuels]
END
GO