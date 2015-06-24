 
IF NOT EXISTS ( SELECT  *
                FROM    sys.objects AS o
                WHERE   o.name = 'AccidentRecordNotificationMember' ) 
    BEGIN
        CREATE TABLE AccidentRecordNotificationMember
            (
             Id INT NOT NULL
            ,SiteId BIGINT NOT NULL REFERENCES dbo.SiteStructureElement ( Id )
            ,EmployeeId UNIQUEIDENTIFIER NOT NULL REFERENCES dbo.Employee ( Id )
            ,[Deleted] [bit] NOT NULL
            ,[CreatedOn] [datetime] NOT NULL
            ,[CreatedBy] [uniqueidentifier] NULL
            ,[LastModifiedOn] [datetime] NULL
            ,[LastModifiedBy] [uniqueidentifier] NULL
            ,CONSTRAINT PK_AccidentRecordNotificationMember PRIMARY KEY ( Id )
            )
	
    END


GRANT SELECT, UPDATE,INSERT ON AccidentRecordNotificationMember TO AllowAll
GRANT SELECT, UPDATE,INSERT ON AccidentRecordNotificationMember TO AllowSelectInsertUpdate