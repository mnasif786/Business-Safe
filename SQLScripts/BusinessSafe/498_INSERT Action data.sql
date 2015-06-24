use BusinessSafe
go

set identity_insert [BusinessSafe].[dbo].[Action] on

DECLARE @id int = 1

WHILE @id <= 50 BEGIN
    SET @id = @id + 1

	INSERT INTO [BusinessSafe].[dbo].[Action](
			[Id]
			,[Title]
			,[ActionPlanId]
			,[Reference]
			,[SignificantHazardIdentified]
			,[RecommendedImmediateAction]
			,[AreaOfNonCompliance]
			,[ActionRequired]
			,[GuidanceNotes]
			,[TargetTimescale]
			,[AssignedTo]
			,[DueDate]
			,[Status])
			
		  VALUES
		  (
			@id
			,'Action Title ' + CAST(@id as varchar(5))
			,1
			,'Reference '
			,1 										-- [SignificantHazardIdentified]
			,'Immediately do something '			-- [RecommendedImmediateAction]
			,'Compliance area'						-- [AreaOfNonCompliance]
			,' do a thing'        					-- [ActionRequired]
			,'guidance note'						-- [GuidanceNotes]
			,'By Friday'							-- [TargetTimescale]
			,'16AC58FB-4EA4-4482-AC3D-000D607AF67C'	-- [AssignedTo]
			,GETDATE()+@id							-- [DueDate]
			,'the status'							-- [Status] 
		  )
		  
END        
set identity_insert [BusinessSafe].[dbo].[Action] off