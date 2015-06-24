SET XACT_ABORT ON
BEGIN TRANSACTION

ALTER TABLE dbo.EmployeeTaskNotification DROP CONSTRAINT PK_EmployeeTaskNotification

ALTER TABLE dbo.EmployeeTaskNotification DROP COLUMN Id

ALTER TABLE dbo.EmployeeTaskNotification ADD Id BIGINT IDENTITY (1,1) NOT NULL

ALTER TABLE [dbo].[EmployeeTaskNotification] ADD CONSTRAINT [PK_EmployeeTaskNotification] PRIMARY KEY CLUSTERED ([Id] ASC)


COMMIT TRAN