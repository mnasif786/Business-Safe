USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM [RiskAssessmentDocument] WHERE DocumentId IS NULL)
BEGIN
	
	DECLARE @RiskAssessmentDocumentId as bigint
	DECLARE @DocumentLibraryId as bigint
	DECLARE @Filename as nvarchar(500)
	DECLARE @Extension as nvarchar(10)
	DECLARE @FilesizeByte as bigint
	DECLARE @Description as nvarchar(255)
	DECLARE @DocumentTypeId as bigint
	DECLARE @Deleted as bit
	DECLARE @CreatedBy as uniqueidentifier
	DECLARE @CreatedOn as datetime
	DECLARE @DocumentId as bigint
	
	DECLARE curRiskAssessmentDocument CURSOR FOR
		SELECT 
		[RiskAssessmentDocumentId],
		[DocumentLibraryId],
		[Filename],
		[Extension],
		[FilesizeByte],
		[Description],
		[DocumentTypeId],
		[Deleted],
		[CreatedBy],
		[CreatedOn]
		FROM [RiskAssessmentDocument] 
		WHERE DocumentId IS NULL
		
	OPEN curRiskAssessmentDocument
	FETCH NEXT FROM curRiskAssessmentDocument INTO @RiskAssessmentDocumentId, @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Description, @DocumentTypeId, @Deleted, @CreatedBy, @CreatedOn
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		INSERT INTO [Document] 
		(
			[DocumentLibraryId], 
			[Filename], 
			[Extension], 
			[FilesizeByte], 
			[Description], 
			[DocumentTypeId], 
			[Deleted], 
			[CreatedBy], 
			[CreatedOn]
		)
		VALUES 
		(
			@DocumentLibraryId, 
			@Filename, 
			@Extension, 
			@FilesizeByte, 
			@Description, 
			@DocumentTypeId, 
			@Deleted, 
			@CreatedBy, 
			@CreatedOn
		)
		
		SET @DocumentId = SCOPE_IDENTITY()
		
		UPDATE [RiskAssessmentDocument] SET [DocumentId] = @DocumentId WHERE [Id] = @RiskAssessmentDocumentId
		FETCH NEXT FROM curRiskAssessmentDocument INTO @RiskAssessmentDocumentId, @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Description, @DocumentTypeId, @Deleted, @CreatedBy, @CreatedOn
	
	END
	
	CLOSE curRiskAssessmentDocument
	DEALLOCATE curRiskAssessmentDocument
END
GO

--//@UNDO 

IF EXISTS (SELECT * FROM [RiskAssessmentDocument] WHERE DocumentId IS NOT NULL)
BEGIN
	DECLARE @DocumentId as bigint
	DECLARE @DocumentLibraryId as bigint
	DECLARE @Filename as nvarchar(500)
	DECLARE @Extension as nvarchar(10)
	DECLARE @FilesizeByte as bigint
	DECLARE @Description as nvarchar(255)
	DECLARE @DocumentTypeId as bigint
	DECLARE @Deleted as bit
	DECLARE @CreatedBy as uniqueidentifier
	DECLARE @CreatedOn as datetime
	DECLARE @RiskAssessmentDocumentId as bigint
	
	DECLARE curRiskAssessmentDocument CURSOR FOR
		SELECT 
		[DocumentId],
		[DocumentLibraryId],
		[Filename],
		[Extension],
		[FilesizeByte],
		[Description],
		[DocumentTypeId],
		[Deleted],
		[CreatedBy],
		[CreatedOn]
		FROM [Document] 
		WHERE DocumentId IN (SELECT DocumentId FROM [RiskAssessmentDocument] WHERE DocumentId IS NOT NULL)
		
	OPEN curDocument
	FETCH NEXT FROM curDocument INTO @DocumentId, @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Description, @DocumentTypeId, @Deleted, @CreatedBy, @CreatedOn
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		
		UPDATE [RiskAssessmentDocument]
		SET [DocumentLibraryId] = @DocumentLibraryId,
		[Filename] = @Filename,
		[Extension] = @Extension,
		[FilesizeByte] = @FilesizeByte,
		[Description] = @Description,
		[DocumentTypeId] = @DocumentTypeId,
		[DocumentId] = NULL
		WHERE DocumentId = @DocumentId
		
		DELETE FROM [DocumentType] WHERE @DocumentId = @DocumentId

		FETCH NEXT FROM db_cursor INTO @RiskAssessmentDocumentId, @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Description, @DocumentTypeId, @DocumentId 
	END
	
	CLOSE curRiskAssessmentDocument
	DEALLOCATE curRiskAssessmentDocument 
	
END
GO