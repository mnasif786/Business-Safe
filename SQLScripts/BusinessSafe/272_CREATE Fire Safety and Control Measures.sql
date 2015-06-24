USE [BusinessSafe]

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireSafetyControlMeasure')
BEGIN
	CREATE TABLE [dbo].[FireSafetyControlMeasure](
		[Id] bigint IDENTITY(1,1) NOT NULL,
		[Name] nvarchar (200) NOT NULL,
		[RiskAssessmentId] [bigint] NULL,
		[CompanyId] [bigint] NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[LastModifiedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[Deleted] [bit]	NOT NULL,
	CONSTRAINT [PK_FireSafetyControlMeasures] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
	GRANT SELECT, INSERT, DELETE, UPDATE ON [FireSafetyControlMeasure] TO AllowAll
END
GO

SET IDENTITY_INSERT [dbo].[FireSafetyControlMeasure] ON

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES 
           (1, 'Fire fighting equipment', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (2, 'Fire sprinkler system', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (3, 'Fire detection equipment', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (4, 'Fire alarms', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (5, 'Protected fire exits', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)
 
INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (6, 'Emergency lighting', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (7, 'Fire exit signs', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)
 
INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (8, 'Fire and emergency procedures', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (9, 'Trained staff', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)


INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (10, 'Fire Drills', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (11, 'No smoking policy', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)

INSERT INTO [BusinessSafe].[dbo].[FireSafetyControlMeasure] ([Id] ,[Name] ,[CreatedBy] ,[CreatedOn] ,[Deleted]) 
     VALUES
           (12, 'Secure premises', '16ac58fb-4ea4-4482-ac3d-000d607af67c', GetDate(), 0)


GO



SET IDENTITY_INSERT [dbo].[FireSafetyControlMeasure] OFF
GO


USE [BusinessSafe]
GO

print 'Create [FireRiskAssessmentFireSafetlyControlMeasures]'
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND TYPE = 'U')
BEGIN
	CREATE TABLE [FireRiskAssessmentFireSafetlyControlMeasures](
		[RiskAssessmentId] [bigint] NOT NULL,
		[FireSafetyControlMeasureId] [bigint] NOT NULL,
	 CONSTRAINT [PK_FireRiskAssessmentFireSafetlyControlMeasures] PRIMARY KEY CLUSTERED 
	(
		[RiskAssessmentId] ASC,
		[FireSafetyControlMeasureId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [FireRiskAssessmentFireSafetlyControlMeasures] TO [AllowAll]

GO






--//@UNDO 

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'FireSafetyControlMeasure')
BEGIN
	DROP TABLE [dbo].[FireSafetyControlMeasure]
END
GO


DELETE FROM [BusinessSafe].[dbo].[FireSafetyControlMeasure] WHERE ID >= 1 AND ID <=12
GO


IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'FireRiskAssessmentFireSafetlyControlMeasures' AND TYPE = 'U')
BEGIN
	DROP TABLE [FireRiskAssessmentFireSafetlyControlMeasures]
END
GO