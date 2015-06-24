IF NOT EXISTS(SELECT * FROM sys.objects AS o
WHERE o.name = 'AccidentRecordPersonInvolvedType')
BEGIN
	CREATE TABLE AccidentRecordPersonInvolvedType(
		Id TINYINT NOT NULL
		,[Description] VARCHAR(50) NOT NULL
		,CONSTRAINT PK_AccidentRecordPersonInvolvedType PRIMARY KEY (id)
	)
END
GO

IF NOT EXISTS (SELECT * FROM AccidentRecordPersonInvolvedType)
BEGIN
	INSERT INTO AccidentRecordPersonInvolvedType 
	SELECT 1, 'Employee'
	UNION SELECT 2, 'Visitor'
	UNION SELECT 3, 'Person at work (not employee)'
	UNION SELECT 4, 'Other'
END
GO

IF NOT EXISTS(SELECT * FROM sys.objects AS o
WHERE o.name = 'AccidentRecordSeverityOfInjuryType')
BEGIN
	CREATE TABLE AccidentRecordSeverityOfInjuryType(
		Id TINYINT NOT NULL
		,[Description] VARCHAR(50) NOT NULL
		,CONSTRAINT PK_AccidentRecordSeverityOfInjuryType PRIMARY KEY (id)
	)
END
GO
IF NOT EXISTS (SELECT * FROM AccidentRecordSeverityOfInjuryType)
BEGIN
	INSERT INTO AccidentRecordSeverityOfInjuryType 
	SELECT 1, 'Fatal'
	UNION SELECT 2, 'Major'
	UNION SELECT 3, 'Minor'
	UNION SELECT 4, 'No Apparent Injury'
END
GO