USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImpressionType')
	BEGIN
		CREATE TABLE [dbo].[SafeCheckImpressionType](
			[Id] [uniqueidentifier] NOT NULL,
			[Title] [varchar](50) NOT NULL,
			[Comments] [varchar](2000) NULL,
		 CONSTRAINT [PK_SafeCheckImpressionType] PRIMARY KEY CLUSTERED 
		(
			[Id] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
		) ON [PRIMARY]
		
		GRANT SELECT, INSERT, UPDATE, DELETE ON [SafeCheckImpressionType] TO AllowAll
		GRANT SELECT, INSERT, UPDATE ON [SafeCheckImpressionType] TO AllowSelectInsertUpdate
	
	END
GO

--//@UNDO 
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'SafeCheckImpressionType')
BEGIN
	DROP TABLE [dbo].[SafeCheckImpressionType]
END
GO

