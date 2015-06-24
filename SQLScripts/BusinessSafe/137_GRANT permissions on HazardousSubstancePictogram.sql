USE [BusinessSafe]
GO

	GRANT SELECT ON [HazardousSubstancePictogram] TO AllowAll
	GRANT INSERT ON [HazardousSubstancePictogram] TO AllowAll
	GRANT UPDATE ON [HazardousSubstancePictogram] TO AllowAll

--//@UNDO 

	DENY SELECT ON [HazardousSubstancePictogram] TO AllowAll
	DENY INSERT ON [HazardousSubstancePictogram] TO AllowAll
	DENY UPDATE ON [HazardousSubstancePictogram] TO AllowAll