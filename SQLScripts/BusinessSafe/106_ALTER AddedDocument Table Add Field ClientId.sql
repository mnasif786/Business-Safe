USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [AddedDocument]
	ADD [ClientId] BIGINT NULL
	
	DECLARE @currSiteId int,
		@nextSiteId int,
		@clientId int
		
	SELECT @nextSiteId = MIN([SiteId])
	FROM [PREVIOUS_AddedDocument]

	WHILE @currSiteId IS NOT NULL
	BEGIN
		SELECT @currSiteId = @nextSiteId
		
		SELECT @clientId = [ClientId]
		FROM [Site]
		WHERE [Id] = @currSiteId
		
		SELECT @nextSiteId = MIN([SiteId])
		FROM [PREVIOUS_AddedDocument]
		WHERE [SiteId] > @currSiteId
	END
	
	ALTER TABLE [AddedDocument] ALTER COLUMN [ClientId] BIGINT NOT NULL
END
GO

--//@UNDO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [AddedDocument]
	DROP COLUMN [ClientId]
END
GO