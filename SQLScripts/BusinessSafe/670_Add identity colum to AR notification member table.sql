SET XACT_ABORT ON
BEGIN TRANSACTION

ALTER TABLE dbo.AccidentRecordNotificationMember DROP CONSTRAINT PK_AccidentRecordNotificationMember

ALTER TABLE dbo.AccidentRecordNotificationMember DROP COLUMN Id

ALTER TABLE dbo.AccidentRecordNotificationMember ADD Id INT IDENTITY (1,1)

ALTER TABLE [dbo].[AccidentRecordNotificationMember] ADD CONSTRAINT [PK_AccidentRecordNotificationMember] PRIMARY KEY CLUSTERED ([Id] ASC)


COMMIT TRAN