IF NOT EXISTS(SELECT * FROM sys.objects AS o WHERE o.name = 'SafeCheckLastQaAdvisorAssigned')
BEGIN

CREATE TABLE SafeCheckLastQaAdvisorAssigned
    (
     Id INT NOT NULL
    ,QaAdvisorId UNIQUEIDENTIFIER NOT NULL
    ,CONSTRAINT PK_SafeCheckLastQaAdvisorAssigned PRIMARY KEY ( Id )
    )
    
END 
GO

GRANT SELECT,UPDATE,INSERT ON SafeCheckLastQaAdvisorAssigned TO AllowAll
GRANT SELECT,UPDATE,INSERT ON SafeCheckLastQaAdvisorAssigned TO AllowSelectInsertUpdate