

IF EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('ActionPlan') 
AND c.name = 'AreasVisited'
AND c.max_length = 400)
BEGIN
	ALTER TABLE dbo.ActionPlan ALTER COLUMN AreasVisited NVARCHAR(500) NULL
END

IF EXISTS (SELECT * FROM sys.columns AS c
WHERE c.object_id = OBJECT_ID('ActionPlan') 
AND c.name = 'AreasNotVisited'
AND c.max_length = 400)
BEGIN
	ALTER TABLE dbo.ActionPlan ALTER COLUMN AreasNotVisited NVARCHAR(500) NULL
END
