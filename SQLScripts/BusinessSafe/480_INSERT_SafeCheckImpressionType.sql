USE [BusinessSafe]
GO

INSERT INTO dbo.SafeCheckImpressionType (Id,Title,Comments)
VALUES (NEWID(), N'Satisfactory', N'The overall standard of health and safety management appeared to be satisfactory although it was identified that early action is necessary to improve safety management at this site. By implementing the recommendations contained within the Action Plan, your present standards of health and safety will be enhanced.')
GO
INSERT INTO dbo.SafeCheckImpressionType
VALUES (NEWID(), N'Unsatisfactory', N'The overall standard of health and safety management at this site was below an acceptable level with some health and safety matters requiring urgent management corrective action. Judged against the Enforcing Authorities’ Enforcement Policy Statement this standard of compliance would leave you open to formal enforcement action should you receive an inspection visit. By implementing the recommendations contained within the Action Plan, your present standards of health and safety will be improved and the likelihood of formal enforcement action being taken against you personally or your company will be reduced.')
GO
INSERT INTO dbo.SafeCheckImpressionType
VALUES (NEWID(), N'Good Standard', N'The overall standard of health and safety management appeared to be very satisfactory. However, by implementing the recommendations contained within the Action Plan, your present standards of health and safety will be further enhanced.')
GO