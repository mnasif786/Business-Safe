----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* Drop the HazardType table - replaced by enum */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'HazardType' AND TYPE = 'U')
BEGIN
	DROP TABLE [HazardType]
END

--//@UNDO 
IF NOT EXISTS (SELECT * FROM sys.objects WHERE NAME = 'HazardType' AND TYPE = 'U')
BEGIN
	CREATE TABLE [dbo].[HazardType](
		[Id] [bigint] IDENTITY(1,1) NOT NULL,
		[Name] [nvarchar](250) NULL,
		[Deleted] [bit] NOT NULL,
		[CreatedOn] [datetime] NOT NULL,
		[CreatedBy] [uniqueidentifier] NULL,
		[LastModifiedOn] [datetime] NULL,
		[LastModifiedBy] [uniqueidentifier] NULL
	) ON [PRIMARY]
END
GO

GRANT SELECT, INSERT, DELETE, UPDATE ON [HazardType] TO [AllowAll]
GO

SET IDENTITY_INSERT [BusinessSafe].[dbo].[HazardType] ON

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (1, 'General', 0, GETDATE(), NULL, NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (2, 'Personal', 0, GETDATE(), NULL, NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (3, 'Hazardous Substance', 0, GETDATE(), NULL, NULL, NULL)

SET IDENTITY_INSERT [BusinessSafe].[dbo].[HazardType] OFF