SET IDENTITY_INSERT [StatutoryResponsibilityTemplate] ON

IF NOT EXISTS (SELECT Id FROM StatutoryResponsibilityTemplate WHERE Id=48) 
BEGIN

	INSERT INTO [dbo].[StatutoryResponsibilityTemplate]([Id], [ResponsibilityCategoryId],[Title],[Description],[GuidanceNote],[ResponsibilityReasonId], [TaskReoccurringTypeId],[CreatedBy],[CreatedOn],[Deleted]) 
	VALUES (48,5,'Lifting accessories (slings, etc.)','Statutory examinations carried out and equipment remains fit for use.','None',2,4, N'16ac58fb-4ea4-4482-ac3d-000d607af67c', '2013-08-19 17:00:00.000', 0)

END
SET IDENTITY_INSERT [StatutoryResponsibilityTemplate] OFF




