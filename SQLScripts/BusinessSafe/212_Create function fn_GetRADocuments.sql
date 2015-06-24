
USE [BusinessSafe]
GO

print '212'

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetRADocuments]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetRADocuments]
GO

CREATE FUNCTION [dbo].[fn_GetRADocuments] (@RAID int)

RETURNS  varchar(max)
AS
BEGIN

--GRANT EXEC ON dbo.[fn_GetRADocuments] to ALLOWALL

DECLARE @result varchar(max)

SELECT @result = COALESCE(@result + '   ', '') + 
   doc.Description
FROM RiskAssessmentDocument rad
LEFT JOIN Document doc ON doc.Id = rad.Id
WHERE rad.RiskAssessmentId = @RAID

RETURN @result

END
GO



--//@UNDO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fn_GetRADocuments]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[fn_GetRADocuments]
GO



