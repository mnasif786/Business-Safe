USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckFavouriteChecklists')
BEGIN

CREATE TABLE [dbo].[SafeCheckFavouriteChecklists](
	[Id] [uniqueidentifier] NOT NULL,
	[ChecklistId] [uniqueidentifier] NOT NULL,
	[MarkedByUser] [varchar](100) NOT NULL,
	[Deleted] [bit] NOT NULL,
 CONSTRAINT [PK_SafeCheckFavouriteChecklists] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END 
GO

IF (OBJECT_ID('FK_SafeCheckFavouriteChecklists_SafeCheckCheckList', 'F') IS NULL)
BEGIN 
	ALTER TABLE [dbo].[SafeCheckFavouriteChecklists]  WITH CHECK ADD  CONSTRAINT [FK_SafeCheckFavouriteChecklists_SafeCheckCheckList] FOREIGN KEY([ChecklistId])
	REFERENCES [dbo].[SafeCheckCheckList] ([Id])
END
GO

IF (OBJECT_ID('FK_SafeCheckFavouriteChecklists_SafeCheckCheckList', 'F') IS NULL)
BEGIN
ALTER TABLE [dbo].[SafeCheckFavouriteChecklists] CHECK CONSTRAINT [FK_SafeCheckFavouriteChecklists_SafeCheckCheckList]
END
GO

ALTER TABLE [dbo].[SafeCheckFavouriteChecklists] ADD  CONSTRAINT [DF_SafeCheckFavouriteChecklists_Deleted]  DEFAULT ((0)) FOR [Deleted]
GO


GRANT SELECT,UPDATE,INSERT,DELETE ON SafeCheckFavouriteChecklists TO AllowAll
GRANT SELECT,UPDATE,INSERT,DELETE ON SafeCheckFavouriteChecklists TO AllowSelectInsertUpdate
GO
