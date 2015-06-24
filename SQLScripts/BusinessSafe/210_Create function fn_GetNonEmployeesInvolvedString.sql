
USE [BusinessSafe]
GO
print '210'

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetNonEmployeesInvolvedString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetNonEmployeesInvolvedString]
GO

CREATE FUNCTION [dbo].[fn_GetNonEmployeesInvolvedString] (@RAID int)
RETURNS  varchar(max)
AS
BEGIN

--GRANT EXEC ON dbo.[fn_GetNonEmployeesInvolvedString] to ALLOWALL

DECLARE @result varchar(max)
--2) Non Employees Involved
SELECT @result = COALESCE('   ' + @result + '   ','') +
ne.Name 
FROM RiskAssessmentsNonEmployees rane

LEFT JOIN NonEmployee ne ON ne.Id = rane.NonEmployeeId AND rane.RiskAssessmentId = @RAID


RETURN @result

END
GO



--//@UNDO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetNonEmployeesInvolvedString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetNonEmployeesInvolvedString]
GO



