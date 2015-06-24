 
IF NOT EXISTS ( SELECT  *
                FROM    sys.objects AS o
                WHERE   o.name = 'EmployeeTaskNotification' ) 
    BEGIN
        CREATE TABLE EmployeeTaskNotification
            (
             Id INT NOT NULL
            ,TaskId BIGINT NOT NULL REFERENCES dbo.Task ( Id )
            ,EmployeeId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Employee ( Id )
            ,[Deleted] [bit] NOT NULL
            ,[CreatedOn] [datetime] NOT NULL
            ,[CreatedBy] [uniqueidentifier] NULL
            ,[LastModifiedOn] [datetime] NULL
            ,[LastModifiedBy] [uniqueidentifier] NULL
            ,CONSTRAINT PK_EmployeeTaskNotification PRIMARY KEY ( Id )
            )
	
    END


GRANT SELECT, UPDATE,INSERT ON EmployeeTaskNotification TO AllowAll
GRANT SELECT, UPDATE,INSERT ON EmployeeTaskNotification TO AllowSelectInsertUpdate

