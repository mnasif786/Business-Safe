
INSERT INTO [BusinessSafe].[dbo].[SafeCheckImpressionType]
           ([Id]
           ,[Title]
           ,[Comments]
           ,[CreatedBy]
           ,[CreatedOn]
           ,[LastModifiedBy]
           ,[LastModifiedOn]
           ,[Deleted])
     VALUES
           ( NEWID(),
           'IRN served - Urgent Action',
           'The overall impression of health and safety management at this site was below an acceptable level with some health and safety matters requiring urgent management corrective action. My particular concern was in relation to Immediate Risk Notification matters, detailed above. Judged against the Enforcing Authorities Enforcement Policy Statement this standard of compliance would leave you open to formal enforcement action should you receive an inspection visit. By taking immediate action in relation to the details above and by implementing the recommendations contained in the Action Plan, your present standards of health and safety will be improved and the likelihood of formal enforcement action being taken against you personally or your company will be reduced',
           'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',
           GETDATE(),
           'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',
           GETDATE(),
           0)
GO

