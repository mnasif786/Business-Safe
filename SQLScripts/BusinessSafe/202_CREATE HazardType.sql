USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardType')
BEGIN
	CREATE TABLE [dbo].[HazardType](
		[ID] [int] NOT NULL,
		[Name] [nvarchar](50) NOT NULL,
	 CONSTRAINT [PK_HazardType] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]

END
	GRANT SELECT, INSERT, DELETE, UPDATE ON [HazardType] TO [AllowAll]
GO
	INSERT INTO [BusinessSafe].[dbo].[HazardType]
           ([ID]
           ,[Name])
     VALUES
           (1
           ,'General')
GO
     INSERT INTO [BusinessSafe].[dbo].[HazardType]
           ([ID]
           ,[Name])
     VALUES
           (2
           ,'Personal')

GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'HazardType')
BEGIN
	DROP TABLE [HazardType];
END