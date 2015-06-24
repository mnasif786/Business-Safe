USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'MIGRATION_HroBsoEmployeeMapping')
BEGIN
	CREATE TABLE [dbo].[MIGRATION_HroBsoEmployeeMapping](
		[BsoEmployeeId] [uniqueidentifier] NOT NULL,
		[HroEmployeeId] [bigint] NOT NULL
	) ON [PRIMARY]
END
GO

GRANT INSERT TO [AllowAll]

