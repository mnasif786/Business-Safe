use BusinessSafe

set identity_insert actionplan on

DECLARE @id int = 0
DECLARE @max int = 0
select @id = MAX(id) from ActionPlan
set @max = @id + 10

WHILE @id < @max BEGIN
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
		  ,[CompanyId]
		  ,[AreasVisited]
		  ,[AreasNotVisited])
		  VALUES
		  (
			@id,'Plan' + CAST(@id as varchar(5)), 378, GETDATE()-@id, 'Consultant'  + CAST(@id as varchar(5)), GETDATE(),'943E5179-9B6E-4C11-BC7E-171B0D1D69A2',GETDATE(),'943E5179-9B6E-4C11-BC7E-171B0D1D69A2', GETDATE(),0,33748,'Area1, Area2','Area 3, Area 4'
		  )
END        
set identity_insert actionplan off