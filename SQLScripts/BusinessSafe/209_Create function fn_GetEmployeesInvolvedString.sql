
USE [BusinessSafe]
GO
print '209'

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetEmployeesInvolvedString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetEmployeesInvolvedString]
GO


CREATE FUNCTION [dbo].[fn_GetEmployeesInvolvedString] (@RAID int)
RETURNS  varchar(max)
AS
BEGIN

--GRANT EXEC ON dbo.fn_GetEmployeesInvolvedString to ALLOWALL

DECLARE @result varchar(max)

SELECT @result = COALESCE(@result + '   ', '   ') + 
   ee.Forename + ' ' + ee.Surname +'   '
FROM RiskAssessmentEmployee rae 
LEFT JOIN Employee ee ON ee.Id = rae.EmployeeId
WHERE rae.RiskAssessmentId = @RAID 


RETURN @result

END
GO


--//@UNDO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetEmployeesInvolvedString]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetEmployeesInvolvedString]
GO
