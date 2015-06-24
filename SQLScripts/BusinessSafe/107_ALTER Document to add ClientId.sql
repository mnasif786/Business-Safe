USE [BusinessSafe]
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [Document] ADD [ClientId] bigint NULL
END
GO

-- COPY ADDED DOCUMENT CLIENT ID
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'ClientId')
BEGIN
	DECLARE @Id as bigint,
			@ClientId as bigint

	DECLARE curDoc CURSOR FOR
		SELECT [Id],
			   [ClientId]
		FROM [AddedDocument]
		
	OPEN curDoc
	FETCH NEXT FROM curDoc INTO @Id, @ClientId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		UPDATE [Document] 
		SET ClientId = @ClientId
		WHERE Id = @Id
			    
		FETCH NEXT FROM curDoc INTO @Id, @ClientId

	END

	CLOSE curDoc
	DEALLOCATE curDoc
END
GO

-- COPY RA DOCUMENT CLIENT ID
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'ClientId')
BEGIN
	DECLARE @Id as bigint,
			@ClientId as bigint

	DECLARE curDoc CURSOR FOR
		SELECT [RiskAssessmentDocument].[Id],
			   [ClientId]
		FROM [RiskAssessmentDocument]
		INNER JOIN [RiskAssessment] ON [RiskAssessment].[Id] = [RiskAssessmentDocument].[RiskAssessmentId]
		
	OPEN curDoc
	FETCH NEXT FROM curDoc INTO @Id, @ClientId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		UPDATE [Document] 
		SET ClientId = @ClientId
		WHERE Id = @Id
			    
		FETCH NEXT FROM curDoc INTO @Id, @ClientId

	END

	CLOSE curDoc
	DEALLOCATE curDoc
END
GO

-- COPY TASK DOCUMENT CLIENT ID
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'ClientId')
BEGIN
	DECLARE @Id as bigint,
			@ClientId as bigint

	DECLARE curDoc CURSOR FOR
		SELECT [TaskDocument].[Id],
			   [ClientId]
		FROM [TaskDocument]
		INNER JOIN [Task] ON [Task].[Id] = [TaskDocument].[TaskId]
		INNER JOIN [RiskAssessmentHazards] ON [Task].[RiskAssessmentHazardId] = [RiskAssessmentHazards].[Id]
		INNER JOIN [RiskAssessment] ON [RiskAssessment].[Id] = [RiskAssessmentHazards].[RiskAssessmentId]
		
	OPEN curDoc
	FETCH NEXT FROM curDoc INTO @Id, @ClientId

	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		UPDATE [Document] 
		SET ClientId = @ClientId
		WHERE Id = @Id
			    
		FETCH NEXT FROM curDoc INTO @Id, @ClientId

	END

	CLOSE curDoc
	DEALLOCATE curDoc
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [Document] ALTER COLUMN [ClientId] bigint NOT NULL
END
GO

--//@UNDO 
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Document' AND COLUMN_NAME = 'ClientId')
BEGIN
	ALTER TABLE [Document] DROP COLUMN [ClientId]
END