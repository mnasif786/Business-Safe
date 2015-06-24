
IF NOT EXISTS(
SELECT * FROM sys.objects AS o
WHERE o.name = 'PersonalRiskAssessementEmployeeChecklistStatus')
BEGIN

	CREATE TABLE PersonalRiskAssessementEmployeeChecklistStatus (
		Id SMALLINT NOT NULL
		,[Name] nvarchar(50) NOT NULL
		,CONSTRAINT PK_PersonalRiskAssessementEmployeeChecklistStatus PRIMARY KEY (id)
		)
END

GO
GRANT SELECT ON PersonalRiskAssessementEmployeeChecklistStatus TO AllowAll

GO


IF NOT EXISTS(
SELECT * FROM sys.objects AS o
	INNER JOIN sys.columns AS c ON o.object_id = c.object_id
WHERE o.name = 'PersonalRiskAssessment'
AND c.name='PersonalRiskAssessementEmployeeChecklistStatusId')
BEGIN
	ALTER TABLE dbo.PersonalRiskAssessment ADD PersonalRiskAssessementEmployeeChecklistStatusId SMALLINT NULL
	
	ALTER TABLE dbo.PersonalRiskAssessment ADD CONSTRAINT DF_PersonalRiskAssessment_PersonalRiskAssessementEmployeeChecklistStatusId DEFAULT (0) FOR PersonalRiskAssessementEmployeeChecklistStatusId
END

GO

UPDATE dbo.PersonalRiskAssessment
SET PersonalRiskAssessementEmployeeChecklistStatusId = 0
WHERE PersonalRiskAssessementEmployeeChecklistStatusId IS NULL

GO


--//@UNDO 
IF EXISTS(
SELECT * FROM sys.objects AS o
	INNER JOIN sys.columns AS c ON o.object_id = c.object_id
WHERE o.name = 'PersonalRiskAssessment'
AND c.name='PersonalRiskAssessementEmployeeChecklistStatusId')
BEGIN
	ALTER TABLE dbo.PersonalRiskAssessment DROP CONSTRAINT DF_PersonalRiskAssessment_PersonalRiskAssessementEmployeeChecklistStatusId
	ALTER TABLE PersonalRiskAssessment DROP COLUMN PersonalRiskAssessementEmployeeChecklistStatusId
END

GO
IF EXISTS(SELECT * FROM sys.objects AS o
WHERE o.name = 'PersonalRiskAssessementEmployeeChecklistStatus')
BEGIN
	DROP TABLE PersonalRiskAssessementEmployeeChecklistStatus
END
GO

