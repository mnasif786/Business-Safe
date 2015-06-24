use BusinessSafe
go

delete from [BusinessSafe].[dbo].[Action]

set identity_insert [BusinessSafe].[dbo].[Action] on

DECLARE @id int = 100

-- 10 actions with Red status
WHILE @id <= 110 BEGIN
    SET @id = @id + 1
	
	INSERT INTO [BusinessSafe].[dbo].[Action]
           ([Id]
           ,[Title]
           ,[ActionPlanId]
           ,[Reference]
           ,[AreaOfNonCompliance]
           ,[ActionRequired]
           ,[GuidanceNotes]
           ,[TargetTimescale]
           ,[AssignedTo]
           ,[DueDate]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[LastModifiedBy]
           ,[LastModifiedOn]
           ,[Deleted]
           ,[Category]
           ,[QuestionStatus])
                        
		  VALUES
		  (
			@id
			,'Action Title ' + CAST(@id as varchar(5))
			,2
			,'Reference '		
			,'Compliance area'						-- [AreaOfNonCompliance]
			,' do a thing'        					-- [ActionRequired]
			,'guidance note'						-- [GuidanceNotes]
			,'By Friday'							-- [TargetTimescale]
			,'D2122FFF-1DCD-4A3C-83AE-E3503B394EB4'	-- [AssignedTo]
			,GETDATE()+@id							-- [DueDate]
			, '16AC58FB-4EA4-4482-AC3D-000D607AF67C'--[CreatedBy]
			, GETDATE()								--[CreatedOn]
			, '16AC58FB-4EA4-4482-AC3D-000D607AF67C'--[LastModifiedBy]
			, GETDATE()								--[LastModifiedOn]
			, 0										--[Deleted]
			, 0										--[Category]											
			, 0										-- [Status] 
		  )
		  
END     


-- 10 actions with Amber status
WHILE @id <= 120 BEGIN
    SET @id = @id + 1
	
	INSERT INTO [BusinessSafe].[dbo].[Action]
           ([Id]
           ,[Title]
           ,[ActionPlanId]
           ,[Reference]
           ,[AreaOfNonCompliance]
           ,[ActionRequired]
           ,[GuidanceNotes]
           ,[TargetTimescale]
           ,[AssignedTo]
           ,[DueDate]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[LastModifiedBy]
           ,[LastModifiedOn]
           ,[Deleted]
           ,[Category]
           ,[QuestionStatus])
                        
		  VALUES
		  (
			@id
			,'Action Title ' + CAST(@id as varchar(5))
			,2
			,'Reference '		
			,'Compliance area'						-- [AreaOfNonCompliance]
			,' do a thing'        					-- [ActionRequired]
			,'guidance note'						-- [GuidanceNotes]
			,'By Friday'							-- [TargetTimescale]
			,'D2122FFF-1DCD-4A3C-83AE-E3503B394EB4'	-- [AssignedTo]
			,GETDATE()+@id							-- [DueDate]
			, '16AC58FB-4EA4-4482-AC3D-000D607AF67C'--[CreatedBy]
			, GETDATE()								--[CreatedOn]
			, '16AC58FB-4EA4-4482-AC3D-000D607AF67C'--[LastModifiedBy]
			, GETDATE()								--[LastModifiedOn]
			, 0										--[Deleted]
			, 0										--[Category]											
			, 1										-- [Status] 
		  )
		  
END     


-- 10 actions with Green status
WHILE @id <= 130 BEGIN
    SET @id = @id + 1
	
	INSERT INTO [BusinessSafe].[dbo].[Action]
           ([Id]
           ,[Title]
           ,[ActionPlanId]
           ,[Reference]
           ,[AreaOfNonCompliance]
           ,[ActionRequired]
           ,[GuidanceNotes]
           ,[TargetTimescale]
           ,[AssignedTo]
           ,[DueDate]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[LastModifiedBy]
           ,[LastModifiedOn]
           ,[Deleted]
           ,[Category]
           ,[QuestionStatus])
                        
		  VALUES
		  (
			@id
			,'Action Title ' + CAST(@id as varchar(5))
			,2
			,'Reference '		
			,'Compliance area'						-- [AreaOfNonCompliance]
			,' do a thing'        					-- [ActionRequired]
			,'guidance note'						-- [GuidanceNotes]
			,'By Friday'							-- [TargetTimescale]
			,'D2122FFF-1DCD-4A3C-83AE-E3503B394EB4'	-- [AssignedTo]
			,GETDATE()+@id							-- [DueDate]
			, '16AC58FB-4EA4-4482-AC3D-000D607AF67C'--[CreatedBy]
			, GETDATE()								--[CreatedOn]
			, '16AC58FB-4EA4-4482-AC3D-000D607AF67C'--[LastModifiedBy]
			, GETDATE()								--[LastModifiedOn]
			, 0										--[Deleted]
			, 0										--[Category]											
			, 2										-- [Status] 
		  )
		  
END     


-- 10 Immediate risk notification actions
WHILE @id <= 140 BEGIN
    SET @id = @id + 1
	
	INSERT INTO [BusinessSafe].[dbo].[Action]
           ([Id]
           ,[Title]
           ,[ActionPlanId]
           ,[Reference]
           ,[AreaOfNonCompliance]
           ,[ActionRequired]
           ,[GuidanceNotes]
           ,[TargetTimescale]
           ,[AssignedTo]
           ,[DueDate]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[LastModifiedBy]
           ,[LastModifiedOn]
           ,[Deleted]
           ,[Category]
           ,[QuestionStatus])
                        
		  VALUES
		  (
			@id
			,'IRN Action Title ' + CAST(@id as varchar(5))
			,2
			,'IRN Reference '		
			,'IRN Compliance area'						-- [AreaOfNonCompliance]
			,'IRN do a thing'        					-- [ActionRequired]
			,'IRN guidance note'						-- [GuidanceNotes]
			,'IRN By Friday'							-- [TargetTimescale]
			,'D2122FFF-1DCD-4A3C-83AE-E3503B394EB4'		-- [AssignedTo]
			,GETDATE()+@id								-- [DueDate]
			, '16AC58FB-4EA4-4482-AC3D-000D607AF67C'	--[CreatedBy]
			, GETDATE()									--[CreatedOn]
			, '16AC58FB-4EA4-4482-AC3D-000D607AF67C'	--[LastModifiedBy]
			, GETDATE()									--[LastModifiedOn]
			, 0											--[Deleted]
			, 1											--[Category]											
			, 0											-- [Status] 
		  )
		  
END     

set identity_insert [BusinessSafe].[dbo].[Action] off