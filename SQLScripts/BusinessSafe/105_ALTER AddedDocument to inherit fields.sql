USE [BusinessSafe]
GO

-------------------------------------------------------------------------------------------------------------
-- RENAME EXISTING AddedDocument TABLE
-------------------------------------------------------------------------------------------------------------
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AddedDocument')
BEGIN
	EXEC sp_rename 'AddedDocument', 'PREVIOUS_AddedDocument'
END

-------------------------------------------------------------------------------------------------------------
-- CREATE NEW AddedDocument TABLE
-------------------------------------------------------------------------------------------------------------
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AddedDocument')
BEGIN
	/****** Object:  Table [dbo].[AddedDocument]    Script Date: 10/16/2012 12:31:13 ******/
	CREATE TABLE [dbo].[AddedDocument](
		[SiteId] [bigint] NULL,
		[Id] [bigint] NOT NULL
	) ON [PRIMARY]
END
GO
	
GRANT SELECT, INSERT,DELETE, UPDATE ON [AddedDocument] TO [AllowAll]
GO

-------------------------------------------------------------------------------------------------------------
-- COPY DATA FROM PREVIOUS_AddedDocument TO AddedDocument AND Document TABLES
-------------------------------------------------------------------------------------------------------------
DECLARE @DocumentLibraryId as bigint
DECLARE @Filename as nvarchar(500)
DECLARE @Extension as nvarchar(10)
DECLARE @FilesizeByte as bigint
DECLARE @Title as nvarchar(200)
DECLARE @Description as nvarchar(255)
DECLARE @DocumentTypeId as bigint
DECLARE @SiteId as bigint
DECLARE @Deleted as bit
DECLARE @CreatedBy as uniqueidentifier
DECLARE @CreatedOn as datetime
DECLARE @LastModifiedOn as datetime
DECLARE @LastModifiedBy as uniqueidentifier
DECLARE @DocumentId as bigint

DECLARE curAddedDocument CURSOR FOR
	SELECT [DocumentLibraryId]
      ,[Filename]
      ,[Extension]
      ,[FilesizeByte]
      ,[Title]
      ,[Description]
      ,[DocumentTypeId]
      ,[SiteId]
      ,[Deleted]
      ,[CreatedOn]
      ,[CreatedBy]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
  FROM [PREVIOUS_AddedDocument]
	
OPEN curAddedDocument
FETCH NEXT FROM curAddedDocument INTO @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Title, @Description, @DocumentTypeId, 
									  @SiteId, @Deleted, @CreatedOn, @CreatedBy, @LastModifiedOn, @LastModifiedBy

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
		[CreatedOn],
		[LastModifiedOn],
		[LastModifiedBy],
		[Title]
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
		@CreatedOn,
		@LastModifiedOn, 
		@LastModifiedBy,
		@Title
	)
	
	SET @DocumentId = SCOPE_IDENTITY()
	
	INSERT INTO [AddedDocument]
	(
		[Id],
		[SiteId]
	)
	VALUES
	(
		@DocumentId,
		@SiteId
    )
    
	FETCH NEXT FROM curAddedDocument INTO @DocumentLibraryId, @Filename, @Extension, @FilesizeByte, @Title, @Description, @DocumentTypeId, 
									      @SiteId, @Deleted, @CreatedBy, @CreatedOn, @LastModifiedBy, @LastModifiedOn

END

CLOSE curAddedDocument
DEALLOCATE curAddedDocument
GO

-------------------------------------------------------------------------------------------------------------
--//@UNDO 
-------------------------------------------------------------------------------------------------------------

DELETE FROM Document WHERE Id IN (
	SELECT Id FROM AddedDocument
)

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'AddedDocument')
BEGIN
	DROP TABLE [AddedDocument] 
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PREVIOUS_AddedDocument')
BEGIN
	EXEC sp_rename 'PREVIOUS_AddedDocument', 'AddedDocument'
END