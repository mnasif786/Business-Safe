USE [BusinessSafe]
GO

DECLARE @RiskAssessmentId as bigint
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
	[RiskAssessmentId],
	[DocumentLibraryId],
	[Filename],
	[Extension],
	[FilesizeByte],
	[Description],
	[DocumentTypeId],
	[Deleted],
	[CreatedBy],
	[CreatedOn]
	FROM [PREVIOUS_RiskAssessmentDocument] 
	
OPEN curRiskAssessmentDocument
FETCH NEXT FROM curRiskAssessmentDocument INTO @RiskAssessmentId, @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Description, @DocumentTypeId, @Deleted, @CreatedBy, @CreatedOn

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
	
	INSERT INTO [RiskAssessmentDocument]
	(
		[Id],
		[RiskAssessmentId]
	)
	VALUES
	(
		@DocumentId,
		@RiskAssessmentId
    )
    
	FETCH NEXT FROM curRiskAssessmentDocument INTO @RiskAssessmentId, @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Description, @DocumentTypeId, @Deleted, @CreatedBy, @CreatedOn

END

CLOSE curRiskAssessmentDocument
DEALLOCATE curRiskAssessmentDocument
GO

DECLARE @FurtherControlMeasureId as bigint
DECLARE @DocumentOriginTypeId as smallint
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

DECLARE curFurtherControlMeasureDocument CURSOR FOR
	SELECT 
	[FurtherControlMeasureId],
	[DocumentOriginTypeId],
	[DocumentLibraryId],
	[Filename],
	[Extension],
	[FilesizeByte],
	[Description],
	[DocumentTypeId],
	[Deleted],
	[CreatedBy],
	[CreatedOn]
	FROM [PREVIOUS_FurtherControlMeasureDocument] 
	
OPEN curFurtherControlMeasureDocument
FETCH NEXT FROM curFurtherControlMeasureDocument INTO @FurtherControlMeasureId, @DocumentOriginTypeId, @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Description, @DocumentTypeId, @Deleted, @CreatedBy, @CreatedOn

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
	
	INSERT INTO [FurtherControlMeasureDocument]
	(
		[Id],
		[FurtherControlMeasureId],
		[DocumentOriginTypeId]
	)
	VALUES
	(
		@DocumentId,
		@FurtherControlMeasureId,
		@DocumentOriginTypeId
    )
    
	FETCH NEXT FROM curFurtherControlMeasureDocument INTO @FurtherControlMeasureId, @DocumentOriginTypeId, @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Description, @DocumentTypeId, @Deleted, @CreatedBy, @CreatedOn

END

CLOSE curFurtherControlMeasureDocument
DEALLOCATE curFurtherControlMeasureDocument
GO

--//@UNDO 

DELETE FROM FurtherControlMeasureDocument
DELETE FROM RiskAssessmentDocument
DELETE FROM Document