use BusinessSafe
go

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'SupportingEvidence')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse		
	ALTER COLUMN SupportingEvidence nvarchar(500) NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'ActionRequired')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse		
	ALTER COLUMN ActionRequired nvarchar(500) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'GuidanceNote')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse		
	ADD GuidanceNotes nvarchar(250) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'TimescaleId')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse		
	ADD TimescaleId bigint NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckQuestionResponse' AND COLUMN_NAME = 'Comment')
BEGIN
	ALTER TABLE	SafeCheckQuestionResponse
	DROP COLUMN Comment
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'SupportingEvidence')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ALTER COLUMN SupportingEvidence nvarchar(500) NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'ActionRequired')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ALTER COLUMN ActionRequired nvarchar(500) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'GuidanceNote')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ADD GuidanceNotes nvarchar(250) NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'TimescaleId')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	ADD TimescaleId bigint NULL
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckCheckListAnswer' AND COLUMN_NAME = 'Comment')
BEGIN
	ALTER TABLE	SafeCheckCheckListAnswer		
	DROP  COLUMN Comment
END

GO
CREATE TABLE [dbo].[Timescale](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Timescale] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

GRANT SELECT, INSERT, UPDATE ON [Timescale] TO AllowAll
GRANT SELECT, INSERT, UPDATE ON [Timescale] TO AllowSelectInsertUpdate
	
INSERT INTO Timescale (ID,Name) VALUES('0','None')
INSERT INTO Timescale (ID,Name) VALUES('1','Mone Month')
INSERT INTO Timescale (ID,Name) VALUES('2','Three Months')
INSERT INTO Timescale (ID,Name) VALUES('3','Six Months')
INSERT INTO Timescale (ID,Name) VALUES('4','Urgent Action Required')
GO
