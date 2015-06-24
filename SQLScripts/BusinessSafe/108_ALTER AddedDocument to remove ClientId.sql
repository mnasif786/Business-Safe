USE [BusinessSafe]
GO

-- create backup table
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_AddedDocument_With_ClientId')
BEGIN
	CREATE TABLE [dbo].[PREVIOUS_AddedDocument_With_ClientId](
		[SiteId] [bigint] NULL,
		[Id] [bigint] NOT NULL,
		[ClientId] [bigint] NOT NULL
	) ON [PRIMARY]
END
GO

-- copy table data
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_AddedDocument_With_ClientId')
BEGIN
	INSERT INTO [PREVIOUS_AddedDocument_With_ClientId]
	SELECT * FROM [AddedDocument]
END
GO

-- remove client id
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [AddedDocument] DROP COLUMN [ClientId]
END
GO

--//@UNDO 

-- add client id null
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [AddedDocument] ADD [ClientId] bigint null
END
GO

-- copy table data back
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	DECLARE @Id as bigint,
			@ClientId as bigint

	DECLARE curDoc CURSOR FOR
		SELECT [Id],
			   [ClientId]
		FROM [PREVIOUS_AddedDocument_With_ClientId]
		
	OPEN curDoc
	FETCH NEXT FROM curDoc INTO @Id, @ClientId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		UPDATE [AddedDocument] 
		SET ClientId = @ClientId
		WHERE Id = @Id
			    
		FETCH NEXT FROM curDoc INTO @Id, @ClientId

	END

	CLOSE curDoc
	DEALLOCATE curDoc
END
GO

-- set client id not null
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AddedDocument' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [AddedDocument] ALTER COLUMN [ClientId] bigint not null
END
GO

-- remove backup table
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_AddedDocument_With_ClientId')
BEGIN
	DROP TABLE [dbo].[PREVIOUS_AddedDocument_With_ClientId]
END
GO