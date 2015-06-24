USE [BusinessSafe]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_CATALOG = 'BusinessSafe' AND TABLE_NAME = 'SafeCheckMaintenanceUser')
BEGIN

CREATE TABLE [dbo].[SafeCheckMaintenanceUser](
	[Id] [uniqueidentifier] NOT NULL,
	[Username] [varchar](100) NOT NULL,
	[Forename] [varchar](100) NOT NULL,
	[Surname] [varchar](100) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Deleted] [bit] NOT NULL,	
	CONSTRAINT [PK_SafeCheckMaintenanceUser] PRIMARY KEY CLUSTERED ([Id] ASC)
	)
 END
GO

GRANT SELECT,UPDATE,INSERT ON SafeCheckMaintenanceUser TO AllowAll
GRANT SELECT,UPDATE,INSERT ON SafeCheckMaintenanceUser TO AllowSelectInsertUpdate
GO

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'MuhammadNauman.Asif','Muhammad','Asif','MuhammadNauman.Asif@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Jimmy.Tse','Jimmy','Tse','Jimmy.Tse@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Lucie.Rybinova','Lucie','Rybinova','Lucie.Rybinova@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Gareth.Wilby','Gareth','Wilby','Gareth.Wilby@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Alastair.Polden','Alastair','Polden','Alastair.Polden@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Scott.Gilhooly','Scott','Gilhooly','Scott.Gilhooly@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Robert.Ball','Robert','Ball','Robert.Ball@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'joanne.lodge','joanne','lodge','joanne.lodge@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Martin.Tagg','Martin','Tagg','Martin.Tagg@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Tony.Trenear','Tony','Trenear','Tony.Trenear@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'Alla.Brizland','Alla','Brizland','Alla.Brizland@peninsula-uk.com',0)

INSERT INTO [dbo].[SafeCheckMaintenanceUser] ([Id],[Username],[Forename],[Surname],[Email],[Deleted])
VALUES (NEWID(),'David.Brierley','David','Brierley','David.Brierley@peninsula-uk.com ',0)
