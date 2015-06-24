USE [BusinessSafe]
GO

GRANT ALTER ON [AccidentRecord] TO [AllowAll]

--//@UNDO 

REVOKE ALTER ON [AccidentRecord] TO [AllowAll]