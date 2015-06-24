IF NOT EXISTS ( SELECT  *
                FROM    sys.objects AS o
                WHERE   o.name = 'RiskAssessmentStatus' ) 
    BEGIN
        CREATE TABLE RiskAssessmentStatus
            (
             Id TINYINT NOT NULL
            ,[Description] VARCHAR(50)
            ,CONSTRAINT PK_RiskAssessmentStatus PRIMARY KEY ( Id )
            )
        
    END
GO

GRANT SELECT, UPDATE,INSERT ON RiskAssessmentStatus TO AllowAll
GRANT SELECT, UPDATE,INSERT ON RiskAssessmentStatus TO AllowSelectInsertUpdate

GO

IF NOT EXISTS (SELECT * FROM dbo.RiskAssessmentStatus)
BEGIN
	INSERT INTO dbo.RiskAssessmentStatus
			( Id, Description )
	SELECT 0,'Draft'
	UNION SELECT 1,'Live'
	UNION SELECT 2,'Archived'

END

GO


