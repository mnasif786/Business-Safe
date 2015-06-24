USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SourceOfIgnition')
BEGIN
	CREATE TABLE [dbo].[SourceOfIgnition](
		[Id] bigint IDENTITY(1,1) NOT NULL,
		[Name] nvarchar (200) NOT NULL,
		[RiskAssessmentId] [bigint] NULL,
		[CompanyId] [bigint] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_SourceOfIgnition] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [SourceOfIgnition] TO AllowAll
END
GO

SET IDENTITY_INSERT [dbo].[SourceOfIgnition] ON

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES 
           (1, 'Electrical equipment', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (2, 'Naked flames', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (3, 'Smokers materials', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (4, 'Pilot lights', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (5, 'Gas and oil fired equipment', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)
 
INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (6, 'Welding', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (7, 'Kitchen and catering equipment', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)
 
INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (8, 'Hot surfaces ', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (9, 'Grinding', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)


INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (10, 'Flame cutting', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (11, 'Friction: drive belts, worn bearings etc', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (12, 'Sparks and static electricity', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[SourceOfIgnition] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (13, 'Arson', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)


GO



SET IDENTITY_INSERT [dbo].[SourceOfIgnition] OFF
GO


USE [BusinessSafe]
GO

print 'Create [FireRiskAssessmentSourceOfIgnitions]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'FireRiskAssessmentSourceOfIgnitions' AND TYPE = 'U')
BEGIN
	CREATE TABLE [FireRiskAssessmentSourceOfIgnitions](
		[RiskAssessmentId] [bigint] NOT NULL,
		[FireRiskAssessmentSourceOfIgnitionId] [bigint] NOT NULL,
	 CONSTRAINT [PK_FireRiskAssessmentSourceOfIgnitions] PRIMARY KEY CLUSTERED 
	(
		[RiskAssessmentId] ASC,
		[FireRiskAssessmentSourceOfIgnitionId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [FireRiskAssessmentSourceOfIgnitions] TO [AllowAll]

GO






--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SourceOfIgnition')
BEGIN
	DROP TABLE [dbo].[SourceOfIgnition]
END
GO


DELETE FROM [BusinessSafe].[dbo].[SourceOfIgnition] WHERE ID >= 1 AND ID <=13
GO


IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'FireRiskAssessmentSourceOfIgnitions' AND TYPE = 'U')
BEGIN
	DROP TABLE [FireRiskAssessmentSourceOfIgnitions]
END
GO