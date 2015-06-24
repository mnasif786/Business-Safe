use BusinessSafe
go

set identity_insert actionplan on

DECLARE @id int = 1

WHILE @id <= 50 BEGIN
    SET @id = @id + 1
	INSERT INTO [BusinessSafe].[dbo].[ActionPlan](
		   [Id]
		  ,[Title]
		  ,[SiteId]
		  ,[DateOfVisit]
		  ,[VisitBy]
		  ,[SubmittedOn]
		  ,[CreatedBy]
		  ,[CreatedOn]
		  ,[LastModifiedBy]
		  ,[LastModifiedOn]
		  ,[Deleted]
		  ,[CompanyId])
		  VALUES
		  (
			@id,'Plan' + CAST(@id as varchar(5)), 378, GETDATE()-@id, 'Consultant1', GETDATE(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C',GETDATE(),'16AC58FB-4EA4-4482-AC3D-000D607AF67C', GETDATE(),0,55881
		  )
END        
set identity_insert actionplan off