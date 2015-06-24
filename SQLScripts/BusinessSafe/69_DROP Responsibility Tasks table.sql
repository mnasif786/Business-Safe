----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the DocumentType table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Responsib__Compl__0519C6AF]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ResponsibilityTask] DROP CONSTRAINT [DF__Responsib__Compl__0519C6AF]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Responsib__Urgen__060DEAE8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ResponsibilityTask] DROP CONSTRAINT [DF__Responsib__Urgen__060DEAE8]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Responsib__Delet__07020F21]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ResponsibilityTask] DROP CONSTRAINT [DF__Responsib__Delet__07020F21]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Responsib__Creat__07F6335A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ResponsibilityTask] DROP CONSTRAINT [DF__Responsib__Creat__07F6335A]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__Responsib__LastM__08EA5793]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ResponsibilityTask] DROP CONSTRAINT [DF__Responsib__LastM__08EA5793]
END

GO

USE [BusinessSafe]
GO

/****** Object:  Table [dbo].[ResponsibilityTask]    Script Date: 09/10/2012 09:35:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ResponsibilityTask]') AND type in (N'U'))
DROP TABLE [dbo].[ResponsibilityTask]
GO



--//@UNDO 

IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE NAME = 'ResponsibilityTask' AND TYPE = 'U')
BEGIN
	print 'Need to add responsibilty task'
END
