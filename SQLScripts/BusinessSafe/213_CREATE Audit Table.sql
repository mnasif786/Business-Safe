USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Audit')
BEGIN
	CREATE TABLE [dbo].[Audit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Type] [varchar](1) NULL,
	[EntityName] [varchar](200) NULL,
	[EntityId] [varchar](200) NULL,
	[FieldName] [varchar](200) NULL,
	[OldValue] [varchar](128) NULL,
	[NewValue] [nvarchar](128) NULL,
	[UpdateDate] [date] NULL,
	[UserName] [nvarchar](128) NULL	)
END
GO

SET ANSI_PADDING OFF
GO


	GRANT SELECT, INSERT, DELETE, UPDATE ON [Audit] TO [AllowAll]
GO


--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Audit')
BEGIN
	DROP TABLE [Audit];
END