USE [BusinessSafe]
GO

	GRANT SELECT ON [Pictogram] TO AllowAll
	GRANT INSERT ON [Pictogram] TO AllowAll
	GRANT UPDATE ON [Pictogram] TO AllowAll

--//@UNDO 


	DENY SELECT ON [Pictogram] TO AllowAll
	DENY INSERT ON [Pictogram] TO AllowAll
	DENY UPDATE ON [Pictogram] TO AllowAll