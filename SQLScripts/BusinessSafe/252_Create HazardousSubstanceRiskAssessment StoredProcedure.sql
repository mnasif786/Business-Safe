USE [BusinessSafe]
GO

	SET ANSI_NULLS ON
	GO

	SET QUOTED_IDENTIFIER OFF
	GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO006_HRA]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO006_HRA]
GO

	CREATE PROCEDURE [dbo].[SPRPT_BSO006_HRA] (@RAID int) AS

	BEGIN




	SELECT  

	hra.id

	,[ControlMeasureNumber] = cast(DENSE_RANK() OVER (PARTITIOn BY ra.ID ORDER BY cm.Id) AS varchar(10)) +'. '
	--,CAST(ROW_NUMBER() OVER (PARTITION BY t.Id ORDER BY t.ID) AS VARCHAR(10)) + '. ' as 
	,[RiskAssessmentNumber] = ra.Reference
	,[HazardousSubstance] = h.Name
	,[ExistingControlMeasures] = cm.ControlMeasure

	,[FurthercontrolMeasuresRequiredNumber] = cast(DENSE_RANK() OVER (PARTITIOn BY ra.ID ORDER BY t.Id) AS varchar(10)) +'. ' 
	,[FurtherControlMeasuresRequired] = t.Description 
	,[ActionAllocatedTo] = ta.Forename + ' ' + ta.Surname
	,[ForCompletionBy] = t.TaskCompletionDueDate
	,[Status] = CASE WHEN t.TaskStatusID = 0 THEN 'For action' WHEN t.TaskStatusId = 1 THEN 'Completed' END
	,cm.Id

	FROM [HazardousSubstanceRiskAssessment] hra
	INNER JOIN [RiskAssessment] ra ON ra.Id = hra.Id
	LEFT JOIN MultiHazardRiskAssessmentHazard rah ON rah.RiskAssessmentID = ra.Id
	LEFT JOIN HazardousSubstanceRiskAssessmentControlMeasure cm ON cm.HazardousSubstanceRiskAssessmentId = hra.Id
	LEFT JOIN HazardousSubstance h ON h.Id = hra.HazardousSubstanceId
	---[Further Control Measures Required]        [Action column]
	LEFT JOIN Task t ON t.HazardousSubstanceRiskAssessmentId = ra.Id
	LEFT JOIN Employee ta ON ta.Id = t.TaskAssignedToId

	WHERE ra.Id = @RAID

	ORDER BY ControlMeasureNumber, FurthercontrolMeasuresRequiredNumber

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO006_HRAHeader]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO006_HRAHeader]
GO

	CREATE PROCEDURE [dbo].[SPRPT_BSO006_HRAHeader] (@RAID int) AS

	BEGIN

		
	SELECT  

	hra.id

	,[ControlMeasureNumber] = cast(DENSE_RANK() OVER (PARTITIOn BY ra.ID ORDER BY cm.Id) AS varchar(10)) +'. '
	--,CAST(ROW_NUMBER() OVER (PARTITION BY t.Id ORDER BY t.ID) AS VARCHAR(10)) + '. ' as 
	,[RiskAssessmentNumber] = ra.Reference
	,[HazardousSubstance] = h.Name
	,[ExistingControlMeasures] = cm.ControlMeasure

	,[FurthercontrolMeasuresRequiredNumber] = cast(DENSE_RANK() OVER (PARTITIOn BY ra.ID ORDER BY t.Id) AS varchar(10)) +'. ' 
	,[FurtherControlMeasuresRequired] = t.Description 
	,[ActionAllocatedTo] = ta.Forename + ' ' + ta.Surname
	,[ForCompletionBy] = t.TaskCompletionDueDate
	,[Status] = CASE WHEN t.TaskStatusID = 0 THEN 'For action' WHEN t.TaskStatusId = 1 THEN 'Completed' END
	,cm.Id

	FROM [HazardousSubstanceRiskAssessment] hra
	INNER JOIN [RiskAssessment] ra ON ra.Id = hra.Id
	LEFT JOIN MultiHazardRiskAssessmentHazard rah ON rah.RiskAssessmentID = ra.Id
	LEFT JOIN HazardousSubstanceRiskAssessmentControlMeasure cm ON cm.HazardousSubstanceRiskAssessmentId = hra.Id
	LEFT JOIN HazardousSubstance h ON h.Id = hra.HazardousSubstanceId
	---[Further Control Measures Required]        [Action column]
	LEFT JOIN Task t ON t.HazardousSubstanceRiskAssessmentId = ra.Id
	LEFT JOIN Employee ta ON ta.Id = t.TaskAssignedToId

	WHERE ra.Id = @RAID

	ORDER BY ControlMeasureNumber, FurthercontrolMeasuresRequiredNumber

END
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO006_HRAPictogram]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO006_HRAPictogram]
GO

	CREATE PROCEDURE [dbo].[SPRPT_BSO006_HRAPictogram] (@RAID int) AS

	BEGIN


	
	SELECT  

	hra.id

	,[ControlMeasureNumber] = cast(DENSE_RANK() OVER (PARTITIOn BY ra.ID ORDER BY cm.Id) AS varchar(10)) +'. '
	--,CAST(ROW_NUMBER() OVER (PARTITION BY t.Id ORDER BY t.ID) AS VARCHAR(10)) + '. ' as 
	,[RiskAssessmentNumber] = ra.Reference
	,[HazardousSubstance] = h.Name
	,[ExistingControlMeasures] = cm.ControlMeasure

	,[FurthercontrolMeasuresRequiredNumber] = cast(DENSE_RANK() OVER (PARTITIOn BY ra.ID ORDER BY t.Id) AS varchar(10)) +'. ' 
	,[FurtherControlMeasuresRequired] = t.Description 
	,[ActionAllocatedTo] = ta.Forename + ' ' + ta.Surname
	,[ForCompletionBy] = t.TaskCompletionDueDate
	,[Status] = CASE WHEN t.TaskStatusID = 0 THEN 'For action' WHEN t.TaskStatusId = 1 THEN 'Completed' END
	,cm.Id

	FROM [HazardousSubstanceRiskAssessment] hra
	INNER JOIN [RiskAssessment] ra ON ra.Id = hra.Id
	LEFT JOIN MultiHazardRiskAssessmentHazard rah ON rah.RiskAssessmentID = ra.Id
	LEFT JOIN HazardousSubstanceRiskAssessmentControlMeasure cm ON cm.HazardousSubstanceRiskAssessmentId = hra.Id
	LEFT JOIN HazardousSubstance h ON h.Id = hra.HazardousSubstanceId
	---[Further Control Measures Required]        [Action column]
	LEFT JOIN Task t ON t.HazardousSubstanceRiskAssessmentId = ra.Id
	LEFT JOIN Employee ta ON ta.Id = t.TaskAssignedToId

	WHERE ra.Id = @RAID

	ORDER BY ControlMeasureNumber, FurthercontrolMeasuresRequiredNumber

END
GO

GRANT EXECUTE ON [dbo].[SPRPT_BSO006_HRA] TO AllowAll
GRANT EXECUTE ON [dbo].[SPRPT_BSO006_HRAHeader] TO AllowAll
GRANT EXECUTE ON [dbo].[SPRPT_BSO006_HRAPictogram] TO AllowAll


--//@UNDO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO_GRA]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO_GRA]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO006_HRAHeader]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO006_HRAHeader]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SPRPT_BSO006_HRAPictogram]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[SPRPT_BSO006_HRAPictogram]
GO

