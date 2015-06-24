

IF NOT EXISTS(SELECT * FROM sys.columns AS c
	WHERE c.name = 'QaSignedOffDate'
	AND c.object_id = OBJECT_ID('SafeCheckCheckListAnswer'))
BEGIN
	ALTER TABLE dbo.SafeCheckCheckListAnswer ADD QaSignedOffDate DATETIME null
END

Go


SET XACT_ABORT ON

BEGIN TRANSACTION

UPDATE dbo.SafeCheckCheckListAnswer
SET QaSignedOffBy = SUBSTRING(QaSignedOffBy,0,  CHARINDEX('_',QaSignedOffBy)+20 )
FROM dbo.SafeCheckCheckListAnswer AS cla
WHERE QaSignedOffBy IS NOT NULL
AND QaSignedOffBy <> SUBSTRING(QaSignedOffBy,0,  CHARINDEX('_',QaSignedOffBy)+20 )
AND CHARINDEX('_',QaSignedOffBy) > -1

SET DATEFORMAT dmy

UPDATE SafeCheckCheckListAnswer
SET QaSignedOffBy = SUBSTRING(QaSignedOffBy,0,CHARINDEX('_',QaSignedOffBy))
	,QaSignedOffDate = CONVERT(DATETIME, SUBSTRING(QaSignedOffBy,CHARINDEX('_',QaSignedOffBy)+1,20))
WHERE QaSignedOffBy IS NOT NULL
AND CHARINDEX('_',QaSignedOffBy) > -1

COMMIT TRANSACTION


/*

SELECT QaSignedOffBy
	,CHARINDEX('_',QaSignedOffBy)
	,SUBSTRING(QaSignedOffBy,0,  CHARINDEX('_',QaSignedOffBy)+20 )
,*
FROM dbo.SafeCheckCheckListAnswer AS cla
WHERE QaSignedOffBy IS NOT NULL
AND QaSignedOffBy <> SUBSTRING(QaSignedOffBy,0,  CHARINDEX('_',QaSignedOffBy)+20 )
AND CHARINDEX('_',QaSignedOffBy) > -1*/