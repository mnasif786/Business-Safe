USE [BusinessSafe]

GRANT DELETE ON [ChecklistGeneratorEmployee] TO AllowAll
GRANT DELETE ON [PersonalRiskAssessmentChecklist] TO AllowAll

--//@UNDO 

REVOKE DELETE ON [ChecklistGeneratorEmployee] TO AllowAll
REVOKE DELETE ON [PersonalRiskAssessmentChecklist] TO AllowAll