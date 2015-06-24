
USE [BusinessSafe]
GO

print '211'

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetPeopleAtRiskString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetPeopleAtRiskString]
GO

CREATE FUNCTION [dbo].[fn_GetPeopleAtRiskString] (@RAID int)

RETURNS  varchar(max)
AS
BEGIN

--GRANT EXEC ON dbo.[fn_GetPeopleAtRiskString] to ALLOWALL

DECLARE @result varchar(max)

SELECT @result = COALESCE(@result + '   ', '') + 
   pr.Name
FROM RiskAssessmentPeopleAtRisk grapar
LEFT JOIN PeopleAtRisk pr ON pr.Id = grapar.PeopleAtRiskId
WHERE grapar.RiskAssessmentId = @RAID 


RETURN @result


END
GO



--//@UNDO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetPeopleAtRiskString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetPeopleAtRiskString]
GO



