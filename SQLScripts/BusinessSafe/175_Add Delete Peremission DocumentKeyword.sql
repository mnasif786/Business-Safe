----------------------------------------------------------------------------------------------------------------------------------------------------------
			/* CREATE the CompanyDefaults table */
----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [BusinessSafe]
GO

GRANT DELETE ON [DocumentKeyword] TO [AllowAll]

GO

--//@UNDO 

DENY DELETE ON [DocumentKeyword] TO [AllowAll]