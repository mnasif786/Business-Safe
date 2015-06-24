----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the DocumentType table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'RiskAssessmentDocument' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].RiskAssessmentDocument(
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[RiskAssessmentId] [bigint] NULL,
		[DocumentLibraryId] [bigint] NOT NULL,
		[Filename] [nvarchar](500) NULL,
		[Extension] [nvarchar](10) NULL,
		[FilesizeByte] [bigint] NULL,
		[Description] [nvarchar](255) NULL,
		[DocumentTypeId] [bigint] NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NOT NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL
	) ON [PRIMARY]

	
	ALTER TABLE [dbo].[RiskAssessmentDocument] ADD  DEFAULT ((0)) FOR [Deleted]
	
END
GO

GRANT SELECT, INSERT,DELETE, UPDATE ON [RiskAssessmentDocument] TO [AllowAll]
GO

--//@UNDO 

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'RiskAssessmentDocument' AND TYPE = 'U')
BEGIN
	DROP TABLE [RiskAssessmentDocument]
END
