USE [BusinessSafe]
GO

	GRANT DELETE ON [HazardousSubstancePictogram] TO AllowAll
	GRANT DELETE ON [HazardousSubstanceRiskPhrase] TO AllowAll
	GRANT DELETE ON [HazardousSubstanceSafetyPhrase] TO AllowAll

--//@UNDO 

	DENY DELETE ON [HazardousSubstancePictogram] TO AllowAll
	DENY DELETE ON [HazardousSubstanceRiskPhrase] TO AllowAll
	DENY DELETE ON [HazardousSubstanceSafetyPhrase] TO AllowAll