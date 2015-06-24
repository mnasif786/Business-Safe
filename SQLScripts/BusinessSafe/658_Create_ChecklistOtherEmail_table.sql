USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckChecklistOtherEmails')
BEGIN

CREATE TABLE [dbo].[SafeCheckChecklistOtherEmails](
	[Id] [uniqueidentifier] NOT NULL,
	[ChecklistId] [uniqueidentifier] NOT NULL,	
	[EmailAddress] [varchar](100) NULL,
	[CreatedOn] [datetime] NULL,
	[LastModifiedOn] [datetime] NULL,
 CONSTRAINT [PK_SafeCheckChecklistOtherEmails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
 
GO

IF (OBJECT_ID('FK_SafeCheckChecklistOtherEmails_SafeCheckChecklist', 'F') IS NULL)
BEGIN 
ALTER TABLE [dbo].[SafeCheckChecklistOtherEmails]  WITH CHECK ADD  CONSTRAINT [FK_SafeCheckChecklistOtherEmails_SafeCheckChecklist] FOREIGN KEY([ChecklistId])
REFERENCES [dbo].[SafeCheckCheckList] ([Id])
END
GO

IF (OBJECT_ID('FK_SafeCheckChecklistOtherEmails_SafeCheckChecklist', 'F') IS NULL)
BEGIN 
ALTER TABLE [dbo].[SafeCheckChecklistOtherEmails] CHECK CONSTRAINT [FK_SafeCheckChecklistOtherEmails_SafeCheckChecklist]
END
GO

GRANT SELECT,UPDATE,INSERT ON SafeCheckChecklistOtherEmails TO AllowAll
GRANT SELECT,UPDATE,INSERT ON SafeCheckChecklistOtherEmails TO AllowSelectInsertUpdate
GO