USE [BusinessSafe]
GO

DROP TABLE PREVIOUS_AddedDocument;
DROP TABLE PREVIOUS_AddedDocument_With_ClientId;
DROP TABLE PREVIOUS_FurtherControlMeasureDocument;
DROP TABLE PREVIOUS_GeneralRiskAssessment;
DROP TABLE PREVIOUS_HazardousSubstanceRiskAssessment;
DROP TABLE PREVIOUS_Permission;
DROP TABLE PREVIOUS_PermissionGroup;
DROP TABLE PREVIOUS_PermissionGroupsPermissions;
DROP TABLE PREVIOUS_RiskAssessmentDocument;


--//@UNDO
USE [BusinessSafe]
GO

-- These tables need to go so let's not go back hey code does not reference them!



