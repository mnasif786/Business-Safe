-- BSO UAT Reports - Do not deploy to live
USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO_FRAChecklist]    Script Date: 07/15/2013 15:38:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SPRPT_BSO_FRAChecklist] (@RAID int) AS


-------- GRANT EXEC ON dbo.SPRPT_BSO_FRAChecklist TO ALLOWALL

-------- SPRPT_BSO_FRAChecklist 496


--------FOR TESTING
--declare @RAID int		-- risk assessment ID
--set @RAID = 472
-----------------------

--select * from MultiHazardRiskAssessmentHazard


------MAIN QUERY
SELECT  
[QNum] = q.ListOrder
,[QuestionText] = q.Text
,[Answer] = CASE WHEN a.YesNoNotApplicableResponse = 1 THEN 'Yes' WHEN a.YesNoNotApplicableResponse = 2 Then 'No' WHEN a.YesNoNotApplicableResponse = 3 THEN 'N/A' END
,[Comments] = a.AdditionalInfo
,[Section] = s.Title
,[SectionOrder] = s.ListOrder 

FROM [Answer] a
INNER JOIN FireRiskAssessmentChecklist cl ON cl.Id = a.FireRiskAssessmentChecklistId AND cl.Deleted = 0
JOIN Question q ON q.Id = a.QuestionID AND q.Deleted =0
JOIN Section s ON s.Id = q.SectionId AND s.Deleted =0 
where cl.FireRiskAssessmentId = @RAID
AND a.Deleted = 0


--GO

GO
USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO_GRA]    Script Date: 07/15/2013 15:38:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SPRPT_BSO_GRA] (@RAID int) AS


---- GRANT EXEC ON dbo.SPRPT_BSO_GRA TO ALLOWALL

---- SPRPT_BSO_GRA 295


------FOR TESTING
--declare @RAID int		-- risk assessment ID
--set @RAID = 295
-----------------------

------MAIN QUERY
SELECT  DISTINCT

[RAID] = ra.Id 
,[RAID_HAZARD] = CAST(ra.ID as varchar(99)) + CAST(rah.Id as varchar(99))			--proxy ID for Risk Assessment + Hazard grouping purposes
,[RAID_HAZARD_CM] = CAST(ra.ID as varchar(99)) + CAST(rah.Id as varchar(99)) + CAST(cm.Id AS varchar(99))      -- proxy ID for Risk Assessment + Hazard + Control Measure grouping purposes
,[RAID_HAZARD_CM_FCM] = CAST(ra.ID as varchar(99)) + CAST(rah.Id as varchar(99)) + CAST(cm.Id AS varchar(99)) + CAST(t.Id as VARCHAR(99))      -- proxy ID for Risk Assessment + Hazard + Control Measure + Further Control Measure grouping purposes

,[RiskAssessmentNumber] = ra.Reference
,[PeopleAtRisk] = REPLACE(dbo.fn_GetPeopleAtRiskString(@RAID),',,',',')
,[HazardsIdentified] =h.Name
,[HazardsIdentifiedDescription] = rah.Description
,[ControlMeasuresInPlace] = cm.ControlMeasure
,[ControlMeasureNumber] = cast(DENSE_RANK() OVER (PARTITION BY h.ID ORDER BY cm.Id) AS int)
,[FurtherControlMeasuresRequired] = t.Description 
,[FurthercontrolMeasuresRequiredNumber] = CASE WHEN t.Description is not null THEN cast(DENSE_RANK() OVER (PARTITIOn BY cm.Id ORDER BY t.Id) AS int) END
,[ActionAllocatedTo] = ta.Forename + ' ' + ta.Surname
,[ForCompletionBy] = t.TaskCompletionDueDate
,[Status] = CASE WHEN t.TaskStatusID  =0 THEN 'Outstanding' ELSE 'Complete' END 



FROM [GeneralRiskAssessment] gra
INNER JOIN [RiskAssessment] ra ON ra.Id = gra.Id AND ra.Deleted=0
LEFT JOIN MultiHazardRiskAssessmentHazard rah ON rah.RiskAssessmentID = ra.Id AND rah.Deleted=0
LEFT JOIN Hazard h ON h.Id = rah.HazardId --get description of Hazard 
LEFT JOIN MultiHazardRiskAssessmentControlMeasure cm ON cm.MultiHazardRiskAssessmentHazardId = rah.Id AND cm.Deleted=0
LEFT JOIN RiskAssessmentPeopleAtRisk par ON par.RiskAssessmentId = ra.Id 
LEFT JOIN PeopleAtRisk pr ON pr.Id = par.PeopleAtRiskId AND pr.Deleted=0
---[Further Control Measures Required]        [Action column]
LEFT JOIN Task t ON t.MultiHazardRiskAssessmentHazardId = rah.Id AND t.Deleted=0
LEFT JOIN Employee ta ON ta.Id = t.TaskAssignedToId AND ta.Deleted=0


WHERE ra.Id = @RAID
ORDER BY HazardsIdentified, FurthercontrolMeasuresRequiredNumber


--GO

GO

USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO_RAHeader]    Script Date: 07/15/2013 15:38:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[SPRPT_BSO_RAHeader] (@RAID int) AS

-- [dbo].[SPRPT_BSO_RAHeader]  1818

BEGIN
SELECT  
[RiskAssessmentNumber] = ra.Reference
,[CompanyName] = c.CompanyName
,[DateOfAssessment] = ra.AssessmentDate
,[Title] = ra.Title
,[Description] = mhra.TaskProcessDescription
--,[ReviewDate] = rar.CompletionDueDate	    --Bug 24/04/13 GS
,[ReviewDate] = dbo.fn_GetRAReviewDate(@RAID)     --Bug24/04/13GS
,[Reviewer] = ISNULL(rae.Title + ' ' ,'') + rae.Forename + ' ' + rae.Surname
,[RiskAssessor] = ISNULL(ee.Title + ' ','')  + ISNULL(ee.Forename + ' ','') + ISNULL(ee.Surname,'')   --Bug 13/06/13 GS  Updated to force display where NULL exists at start of string.
,[EmpInvolved] = dbo.fn_GetEmployeesInvolvedString(@RAID)
,[NonEmpInvolved]= dbo.fn_GetNonEmployeesInvolvedString(@RAID)
,[Documents] = dbo.fn_GetRADocuments (@RAID)
,[PeopleInvolved] = dbo.fn_GetAllEmployeesInvolvedString (@RAID)
,[AddressString]	= 	


REPLACE(               --wrapped to handle occurrences of multiple comma
REPLACE(
						isnull(rtrim(SA.Address1)+ ',','') + 
						isnull(rtrim(SA.Address2)+ ',','') + 
						isnull(rtrim(SA.Address3)+ ',','') + 
						isnull(rtrim(SA.Address4)+ ',','') + 
						isnull(rtrim(SA.Address5)+ ',','') + 
						(rtrim(SA.Postcode))
						,',,',','
						),',,',',')
						
, [POSTCODE]	= SA.Postcode

FROM [RiskAssessment] ra 
INNER JOIN Peninsula_BusinessSafe.dbo.TBLCustomers c ON c.CustomerID = ra.ClientId 
LEFT JOIN MultiHazardRiskAssessment mhra ON mhra.Id = ra.Id 
LEFT JOIN [RiskAssessmentReview] rar ON rar.RiskAssessmentId = ra.Id and rar.CompletedDate is null AND rar.Deleted=0-- limits data to only incomplete assessments
----get Reviewer Name
LEFT JOIN [Employee] rae on rae.Id = rar.ReviewAssignedToId AND rae.Deleted=0
----get Risk Assessor Name
LEFT JOIN [RiskAssessor] rass ON rass.Id = ra.RiskAssessorId		AND rass.Deleted=0  	--In 20/05/13 due to data structure change   --  ||   LEFT JOIN [Employee] ee ON ee.Id = ra.RiskAssessorEmployeeId  ||  --Written out 20/05/13 GS due to data structure change
LEFT JOIN [Employee] ee ON ee.Id = rass.EmployeeId			AND ee.Deleted=0		--In 20/05/13 due to data structure change

--get Site Address
LEFT JOIN [BusinessSafe].[dbo].[SiteStructureElement] sse ON sse.Id = ra.SiteId AND sse.Deleted=0
LEFT JOIN Peninsula_BusinessSafe.dbo.TBLSiteAddresses sa ON sa.SiteAddressID = sse.SiteId 


WHERE ra.Id = @RAID


ORDER BY ReviewDate asc -- to get the closest review date

END




--select * from RiskAssessor where id = 894
--select * from Employee where id = '42236493-A6B0-434A-95DF-71E7244886AC'


--select * from

--RiskAssessment RA 
--LEFT JOIN [RiskAssessor] rass ON rass.Id = ra.RiskAssessorId		AND rass.Deleted=0  	--In 20/05/13 due to data structure change   --  ||   LEFT JOIN [Employee] ee ON ee.Id = ra.RiskAssessorEmployeeId  ||  --Written out 20/05/13 GS due to data structure change
--LEFT JOIN [Employee] ee ON ee.Id = rass.EmployeeId			AND ee.Deleted=0

--WHERE RA.Id = 1818


GO



USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO001_SiteOverview]    Script Date: 07/15/2013 15:38:41 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[SPRPT_BSO001_SiteOverview] (@SiteId int) AS

BEGIN

-- GRANT EXEC ON dbo.[SPRPT_BSO001_SiteOverview] TO ALLOWALL


SELECT


[AddressString]	= 	
REPLACE(
						isnull(rtrim(SA.Address1)+ ',','') + 
						isnull(rtrim(SA.Address2)+ ',','') + 
						isnull(rtrim(SA.Address3)+ ',','') + 
						isnull(rtrim(SA.Address4)+ ',','') + 
						isnull(rtrim(SA.Address5)+ ',','') + 
						(rtrim(SA.Postcode))
						,',,',','
						)
						
, [POSTCODE]	= SA.Postcode

FROM [RiskAssessment] ra 
INNER JOIN dbo.vwCustomers AS c ON c.CustomerID = ra.ClientId
LEFT JOIN [GeneralRiskAssessment] gra ON gra.Id = ra.Id
LEFT JOIN [RiskAssessmentReview] rar ON rar.RiskAssessmentId = ra.Id

--get Site Address
LEFT JOIN [BusinessSafe].[dbo].[SiteStructureElement] sse ON sse.Id = ra.SiteId
LEFT JOIN dbo.vwSiteAddresses AS sa ON sa.SiteAddressID = sse.SiteId 

WHERE sa.SiteAddressId = 464


------MAIN QUERY
SELECT  
[RiskAssessmentNumber] = ra.Reference
,[CompanyName] = c.CompanyName
,[DateOfAssessment] = ra.AssessmentDate
,[Title] = ra.Title
,[ReviewDate] = rar.CompletionDueDate		--!!!! Confirm !!!!!!

,[AddressString]	= 	
REPLACE(
						isnull(rtrim(SA.Address1)+ ',','') + 
						isnull(rtrim(SA.Address2)+ ',','') + 
						isnull(rtrim(SA.Address3)+ ',','') + 
						isnull(rtrim(SA.Address4)+ ',','') + 
						isnull(rtrim(SA.Address5)+ ',','') + 
						(rtrim(SA.Postcode))
						,',,',','
						)
						
, [POSTCODE]	= SA.Postcode

FROM [RiskAssessment] ra 
INNER JOIN vwCustomers c ON c.CustomerID = ra.ClientId
LEFT JOIN [GeneralRiskAssessment] gra ON gra.Id = ra.Id
LEFT JOIN [RiskAssessmentReview] rar ON rar.RiskAssessmentId = ra.Id

--get Site Address
LEFT JOIN [BusinessSafe].[dbo].[SiteStructureElement] sse ON sse.Id = ra.SiteId
LEFT JOIN vwSiteAddresses sa ON sa.SiteAddressID = sse.SiteId 


WHERE sa.SiteAddressId = @SiteId

END



GO
USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO003_PEEPs]    Script Date: 07/15/2013 15:38:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

CREATE PROCEDURE [dbo].[SPRPT_BSO003_PEEPs] (@ClientId int) AS

BEGIN
SELECT

--Employee Details
[EmployeeId] = ee.Id
,[Forename]  = ee.Forename
,[Surname] = ee.Surname
,[NameString] = ee.Forename + ' ' + ee.Surname
,[EmailAddress] = ecd.Email

--Employee Contact Details
,[Tel1] = ecd.Telephone1
,[Tel2] = ecd.Telephone2
,[AddressString]	= 	
ISNULL
(
(REPLACE(
						isnull(rtrim(ecd.Address1)+ ',','') + 
						isnull(rtrim(ecd.Address2)+ ',','') + 
						isnull(rtrim(ecd.Address3)+ ',','') + 
						(rtrim(ecd.Postcode))
						,',,',','
						)
),'[No Address Details held for this Employee]') 

,[DisabilityDescription] 


--Site Info
,[SiteName] = IsNull(sa.Address1, '[No Site Name Available]')
,[SitePostcode] = sa.Postcode

FROM 
[Employee] ee 
LEFT JOIN [EmployeeContactDetails] ecd ON ecd.EmployeeId = ee.Id
LEFT JOIN [SiteStructureElement] sse ON sse.Id = ee.SiteId
LEFT JOIN [vwSiteAddresses] sa ON sa.SiteAddressID = sse.SiteId


WHERE ee.ClientId = @ClientId
AND ee.Deleted=0

END

GO

USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO006_HRA]    Script Date: 07/15/2013 15:39:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
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

USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO006_HRAHeader]    Script Date: 07/15/2013 15:39:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
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

USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO006_HRAPictogram]    Script Date: 07/15/2013 15:39:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
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

USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO007_PRA]    Script Date: 07/15/2013 15:39:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SPRPT_BSO007_PRA] (@RAID int) AS


---- GRANT EXEC ON dbo.[SPRPT_BSO007_PRA] TO ALLOWALL

---- [SPRPT_BSO007_PRA] 50             --50 - GSTEST        --32  PC test


------FOR TESTING
--declare @RAID int		-- risk assessment ID
--set @RAID = 295
-----------------------

------MAIN QUERY
SELECT  DISTINCT

[RAID] = ra.Id 
,[RAID_HAZARD] = CAST(ra.ID as varchar(99)) + CAST(rah.Id as varchar(99))			--proxy ID for Risk Assessment + Hazard grouping purposes
,[RAID_HAZARD_CM] = CAST(ra.ID as varchar(99)) + CAST(rah.Id as varchar(99)) + CAST(cm.Id AS varchar(99))      -- proxy ID for Risk Assessment + Hazard + Control Measure grouping purposes
,[RAID_HAZARD_CM_FCM] = CAST(ra.ID as varchar(99)) + CAST(rah.Id as varchar(99)) + CAST(cm.Id AS varchar(99)) + CAST(t.Id as VARCHAR(99))      -- proxy ID for Risk Assessment + Hazard + Control Measure + Further Control Measure grouping purposes

,[RiskAssessmentNumber] = ra.Reference
,[PeopleAtRisk] = REPLACE(dbo.fn_GetPeopleAtRiskString(@RAID),',,',',')
,[HazardsIdentified] =h.Name
,[HazardsIdentifiedDescription] = rah.Description
,[ControlMeasuresInPlace] = cm.ControlMeasure
,[ControlMeasureNumber] = cast(DENSE_RANK() OVER (PARTITION BY h.ID ORDER BY cm.Id) AS int)
,[FurtherControlMeasuresRequired] = t.Description 
,[FurthercontrolMeasuresRequiredNumber] = CASE WHEN t.Description is not null THEN cast(DENSE_RANK() OVER (PARTITIOn BY cm.Id ORDER BY t.Id) AS int) END
,[ActionAllocatedTo] = ta.Forename + ' ' + ta.Surname
,[ForCompletionBy] = t.TaskCompletionDueDate
,[Status] = CASE WHEN t.TaskStatusID  =0 THEN 'Outstanding' ELSE 'Complete' END 
,[Recurring] = CASE WHEN t.TaskReoccurringTypeId = 0 THEN 0 ELSE 1 END


FROM [PersonalRiskAssessment] gra
INNER JOIN [RiskAssessment] ra ON ra.Id = gra.Id AND ra.Deleted=0
LEFT JOIN MultiHazardRiskAssessmentHazard rah ON rah.RiskAssessmentID = ra.Id AND rah.Deleted=0
LEFT JOIN Hazard h ON h.Id = rah.HazardId --get description of Hazard 
LEFT JOIN MultiHazardRiskAssessmentControlMeasure cm ON cm.MultiHazardRiskAssessmentHazardId = rah.Id AND cm.Deleted=0
LEFT JOIN RiskAssessmentPeopleAtRisk par ON par.RiskAssessmentId = ra.Id 
LEFT JOIN PeopleAtRisk pr ON pr.Id = par.PeopleAtRiskId AND pr.Deleted=0
---[Further Control Measures Required]        [Action column]
LEFT JOIN Task t ON t.MultiHazardRiskAssessmentHazardId = rah.Id AND t.Deleted=0
LEFT JOIN Employee ta ON ta.Id = t.TaskAssignedToId AND ta.Deleted=0


WHERE ra.Id = @RAID
ORDER BY HazardsIdentified, FurthercontrolMeasuresRequiredNumber


--GO

GO



USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO007_PRAHeader]    Script Date: 07/15/2013 15:39:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


CREATE PROCEDURE [dbo].[SPRPT_BSO007_PRAHeader] (@RAID int) AS

--GRANT EXEC ON SPRPT_BSO007_PRAHEader TO ALLOWALL
-- [dbo].[SPRPT_BSO007_PRAHeader]  39


BEGIN
SELECT  
[RiskAssessmentNumber] = ra.Reference
,[CompanyName] = c.CompanyName
,[DateOfAssessment] = ra.AssessmentDate
,[Title] = ra.Title
,[Description] = mhra.TaskProcessDescription
--,[ReviewDate] = rar.CompletionDueDate	    --Bug 24/04/13 GS
,[ReviewDate] = dbo.fn_GetRAReviewDate(@RAID)     --Bug24/04/13GS
,[Reviewer] = rae.Title + ' ' + rae.Forename + ' ' + rae.Surname
,[RiskAssessor] = ISNULL(ee.Title + ' ','')  + ISNULL(ee.Forename + ' ','') + ISNULL(ee.Surname,'')   --Bug 13/06/13 GS  Updated to force display where NULL exists at start of string.
,[EmpInvolved] = dbo.fn_GetEmployeesInvolvedString(@RAID)
,[NonEmpInvolved]= dbo.fn_GetNonEmployeesInvolvedString(@RAID)
,[Documents] = dbo.fn_GetRADocuments (@RAID)
,[PeopleInvolved] = dbo.fn_GetAllEmployeesInvolvedString (@RAID)
,[ChecklistsUsed] = dbo.fn_GetEmployeeChecklistRefs(@RAID)
,[AssessmentInRespectOf] = dbo.fn_GetAssessmentInRespectOfString(@RAID)
,[AddressString]	= 	


REPLACE(               --wrapped to handle occurrences of multiple comma
REPLACE(
						isnull(rtrim(SA.Address1)+ ',','') + 
						isnull(rtrim(SA.Address2)+ ',','') + 
						isnull(rtrim(SA.Address3)+ ',','') + 
						isnull(rtrim(SA.Address4)+ ',','') + 
						isnull(rtrim(SA.Address5)+ ',','') + 
						(rtrim(SA.Postcode))
						,',,',','
						),',,',',')
						
, [POSTCODE]	= SA.Postcode

FROM [RiskAssessment] ra 
INNER JOIN vwCustomers c ON c.CustomerID = ra.ClientId
LEFT JOIN MultiHazardRiskAssessment mhra ON mhra.Id = ra.Id 
LEFT JOIN [RiskAssessmentReview] rar ON rar.RiskAssessmentId = ra.Id and rar.CompletedDate is null AND rar.Deleted=0-- limits data to only incomplete assessments
----get Reviewer Name
LEFT JOIN [Employee] rae on rae.Id = rar.ReviewAssignedToId  AND rae.Deleted=0
----get Risk Assessor Name
LEFT JOIN [RiskAssessor] rass ON rass.Id = ra.RiskAssessorId	AND rass.Deleted=0  	--In 20/05/13 due to data structure change   --  ||   LEFT JOIN [Employee] ee ON ee.Id = ra.RiskAssessorEmployeeId  ||  --Written out 20/05/13 GS due to data structure change
LEFT JOIN [Employee] ee ON ee.Id = rass.EmployeeId AND ee.Deleted=0					--In 20/05/13 due to data structure change
--get Site Address
LEFT JOIN [BusinessSafe].[dbo].[SiteStructureElement] sse ON sse.Id = ra.SiteId AND sse.Deleted=0
LEFT JOIN dbo.vwSiteAddresses AS sa ON sa.SiteAddressID = sse.SiteId AND sa.flagHidden=0


WHERE ra.Id = @RAID

ORDER BY ReviewDate asc -- to get the closest review date

END


GO



USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO12_FRA]    Script Date: 07/15/2013 15:39:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SPRPT_BSO12_FRA] (@RAID int) AS


------ GRANT EXEC ON dbo.[SPRPT_BSO12_FRA] TO ALLOWALL

------ SPRPT_BSO12_FRA 472


------FOR TESTING
--declare @RAID int		-- risk assessment ID
--set @RAID = 472
-----------------------

--select * from MultiHazardRiskAssessmentHazard


------MAIN QUERY
SELECT  

[RAID] = ra.Id 
--,[RAID_HAZARD] = CAST(ra.ID as varchar(99)) + CAST(cm.Id as varchar(99))			--proxy ID for Risk Assessment + Hazard grouping purposes
,[RAID_HAZARD_CM] = CAST(ra.ID as varchar(99)) + CAST(cm.Id as varchar(99)) + CAST(fcm.FireSafetyControlMeasureId AS varchar(99))      -- proxy ID for Risk Assessment + Hazard + Control Measure grouping purposes
--,[RAID_HAZARD_CM_FCM] = CAST(ra.ID as varchar(99)) + CAST(cm.Id as varchar(99)) + CAST(cm.Id AS varchar(99)) + CAST(t.Id as VARCHAR(99))      -- proxy ID for Risk Assessment + Hazard + Control Measure + Further Control Measure grouping purposes

,[RiskAssessmentNumber] = ra.Reference

,[ControlMeasuresInPlace] = cm.Name
,[ControlMeasureNumber] = cast(DENSE_RANK() OVER (PARTITION BY ra.Id ORDER BY cm.Id) AS int)
--,[FurtherControlMeasuresRequired] = t.Description 
--,[FurthercontrolMeasuresRequiredNumber] = CASE WHEN t.Description is not null THEN cast(DENSE_RANK() OVER (PARTITIOn BY cm.Id ORDER BY t.Id) AS int) END
--,[ActionAllocatedTo] = ta.Forename + ' ' + ta.Surname
--,[ForCompletionBy] = t.TaskCompletionDueDate
--,[Status] = CASE WHEN t.TaskStatusID  =0 THEN 'Outstanding' ELSE 'Complete' END 

FROM [FireRiskAssessment] fra
INNER JOIN [RiskAssessment] ra ON ra.Id = fra.Id AND ra.Deleted=0  AND ra.Deleted=0
LEFT JOIN [FireRiskAssessmentFireSafetlyControlMeasures] fcm ON fcm.RiskAssessmentId = fra.Id 
LEFT JOIN FireSafetyControlMeasure cm ON cm.Id = fcm.FireSafetyControlMeasureId AND cm.Deleted=0


WHERE fra.Id = @RAID




--GO

GO

USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO12_FRA_FCM]    Script Date: 07/15/2013 15:40:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SPRPT_BSO12_FRA_FCM] (@RAID int) AS


------ GRANT EXEC ON dbo.[SPRPT_BSO12_FRA_FCM] TO ALLOWALL

------ SPRPT_BSO12_FRA_FCM 1145

------FOR TESTING
--declare @RAID int		-- risk assessment ID
--set @RAID = 472
-----------------------

--select * from MultiHazardRiskAssessmentHazard


------MAIN QUERY
SELECT  

[RAID] = a.FireRiskAssessmentChecklistId
,[RAID_SF_FCM] = CAST(a.FireRiskAssessmentChecklistId as varchar(99)) + CAST(sf.Id as varchar(99)) + CAST(t.Id AS varchar(99)) + CAST(t.Id as VARCHAR(99))      -- proxy ID for  grouping purposes

--Pull in question details that the significant finding relates to
,[Question] = CAST(q.ListOrder AS VARCHAR(5)) + '. ' + q.Text + ' - ' + 'No'  -- to replicate Significant findings tab ... assumes Significant Findings are only recorded when a 'No' answer
,[FCMOrder] = q.ListOrder
,[FurthercontrolMeasuresRequiredNumber] = cast(DENSE_RANK() OVER (PARTITIOn BY q.ListOrder ORDER BY t.Id) AS varchar(10)) +'. ' 
,[Title] = t.Title 
,[Description] = t.Description
,[Assignee] = ta.Forename + ' ' + ta.Surname
,[ForCompletionBy] = t.TaskCompletionDueDate
,[Status] = CASE WHEN t.TaskStatusID  =0 THEN 'Outstanding' ELSE 'Complete' END 
,[Recurring] = CASE WHEN t.TaskReoccurringTypeId = 0 THEN 0 ELSE 1 END 


FROM [Answer] a
INNER JOIN FireRiskAssessmentChecklist cl ON cl.Id = a.FireRiskAssessmentChecklistId AND cl.Deleted = 0
JOIN SignificantFinding sf ON sf.FireAnswerId = a.Id AND sf.Deleted = 0
	--[Further Control Measures Required]        [Action column]
JOIN Task t ON t.SignificantFindingId = sf.Id AND t.Deleted =0
JOIN Employee ta ON ta.Id = t.TaskAssignedToId AND ta.Deleted=0
JOIN Question q ON q.Id = a.QuestionID

where cl.FireRiskAssessmentId = @RAID
And YesNoNotApplicableResponse = 2
AND a.Deleted = 0


--GO

GO

GO
USE [BusinessSafe]
GO

/****** Object:  StoredProcedure [dbo].[SPRPT_BSO12_FRAHeader]    Script Date: 07/15/2013 15:40:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[SPRPT_BSO12_FRAHeader] (@RAID int)

AS

----GRANT EXEC ON SPRPT_BSO12_FRAHeader TO ALLOWALL
----[SPRPT_BSO12_FRAHeader] 472

--DECLARE @RAID int = 472

SELECT

--Summary
[RiskAssessor] = ISNULL(ee.Title + ' ','')  + ISNULL(ee.Forename + ' ','') + ISNULL(ee.Surname,'')   --Bug 13/06/13 GS  Updated to force display where NULL exists at start of string.
,[DateOfAssessment] = ra.AssessmentDate
,[Reference] = ra.Reference

,[PersonAppointed] = PersonAppointed

--,[ReviewDate] = rar.CompletionDueDate	    --Bug 24/04/13 GS
,[ReviewDate] = dbo.fn_GetRAReviewDate(@RAID)     --Bug24/04/13GS
,[Documents] = dbo.fn_GetRADocuments (@RAID)

,[CompanyName] = sa.CompanyName     --!! Company name actually gives name of Site... ?Resolve to MasterCAN?
,[AddressString]	= 	
REPLACE (     --wrapper to resolve double commas
REPLACE(
						isnull(rtrim(SA.Address1)+ ',','') + 
						isnull(rtrim(' '+SA.Address2)+ ',','') + 
						isnull(rtrim(' '+SA.Address3)+ ',','') + 
						isnull(rtrim(' '+SA.Address5)+ ',','') + 
						(rtrim(' '+SA.Postcode))
						,',,',','
						)
						,',,',',') 
						
, [POSTCODE]	= SA.Postcode

--Premises Information
,[Site] = '' -------------!!!!!!!!!!!!!!!!!!!!!!!!!!!! see CompanyName above
,[Location] = Location
,[LocationFlag] = CASE WHEN Location IS NULL THEN 0 ELSE 1 END   -- flag to SSRS whether to display location box
,[BuildingUse] = BuildingUse
,[NumFloors] = NumberOfFloors
,[NumPeople] = NumberOfPeople
,[SleepingFlag] = PremisesProvidesSleepingAccommodation -- i.e. "These premises include the provision of sleeping accommodation"
,[SleepingText] = CASE WHEN PremisesProvidesSleepingAccommodation = 1 THEN 'These premises include the provision of sleeping accommodation.' ELSE 'These premises do not include the provision of sleeping accommodation.' END
,[Isolator_Elec] = CASE WHEN ElectricityEmergencyShutOff IS NULL THEN 'N/A' ELSE ElectricityEmergencyShutOff END
,[Isolator_Gas] = CASE WHEN GasEmergencyShutOff IS NULL THEN 'N/A' ELSE GasEmergencyShutOff END
,[Isolator_Water] = CASE WHEN WaterEmergencyShutOff IS NULL THEN 'N/A' ELSE WaterEmergencyShutOff END
,[Isolator_Other] = CASE WHEN OtherEmergencyShutOff IS NULL THEN 'N/A' ELSE OtherEmergencyShutOff END

--SignificantFindings
,[PeopleAtRisk] = REPLACE(dbo.fn_GetPeopleAtRiskString(@RAID),',,',',')
,[SourcesOfFuel] = REPLACE(dbo.fn_GetFuelIgnitionString(@RAID, 1),',,',',')
,[SourcesOfIgnition] = REPLACE(dbo.fn_GetFuelIgnitionString(@RAID, 2),',,',',')


FROM

FireRiskAssessment fra
INNER JOIN RiskAssessment ra ON ra.Id = fra.Id AND ra.Deleted=0 
LEFT JOIN [RiskAssessmentReview] rar ON rar.RiskAssessmentId = ra.Id AND rar.Deleted=0
--get Fire Risk Assessment checklist
LEFT JOIN FireRiskAssessmentChecklist frac ON frac.FireRiskAssessmentId = fra.Id AND frac.Deleted=0
--get Fire Risk Assessment FireSafetyControlMeasures
LEFT JOIN FireRiskAssessmentFireSafetlyControlMeasures fcm ON fcm.RiskAssessmentId = fra.Id 
LEFT JOIN MultiHazardRiskAssessmentControlMeasure cm ON cm.Id = fcm.FireSafetyControlMeasureId AND cm.Deleted=0
----get Risk Assessor Name
LEFT JOIN [RiskAssessor] rass ON rass.Id = ra.RiskAssessorId			AND Rass.Deleted=0 --In 20/05/13 due to data structure change   --  ||   LEFT JOIN [Employee] ee ON ee.Id = ra.RiskAssessorEmployeeId  ||  --Written out 20/05/13 GS due to data structure change
LEFT JOIN [Employee] ee ON ee.Id = rass.EmployeeId					AND ee.Deleted=0 --In 20/05/13 due to data structure change
--get Site Address
LEFT JOIN [BusinessSafe].[dbo].[SiteStructureElement] sse ON sse.Id = ra.SiteId AND sse.Deleted=0
LEFT JOIN [Peninsula_BusinessSafe].dbo.TBLSiteAddresses sa ON sa.SiteAddressID = sse.SiteId AND sa.flagHidden=0


WHERE
@RAID = fra.Id


GO

