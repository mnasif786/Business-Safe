IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Action' AND COLUMN_NAME = 'Category')
BEGIN
	ALTER TABLE	[dbo].[Action] ADD Category [int] NULL DEFAULT  0
END
GO











	
	
	