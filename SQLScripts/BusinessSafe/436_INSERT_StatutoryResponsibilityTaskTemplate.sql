SET IDENTITY_INSERT [StatutoryResponsibilityTaskTemplate] ON

IF NOT EXISTS (SELECT Id FROM StatutoryResponsibilityTaskTemplate WHERE Id=53) 
BEGIN
	INSERT INTO [dbo].[StatutoryResponsibilityTaskTemplate]([id],[StatutoryResponsibilityId],[Title],[Description],[TaskReoccurringTypeId],[CreatedBy],[CreatedOn],[Deleted]) 
	VALUES(53,48 ,'Lifting accessories (slings, etc.)','Statutory examinations carried out and equipment remains fit for use.', 4 , N'16ac58fb-4ea4-4482-ac3d-000d607af67c', '2013-08-19 17:00:00.000', 0)
END 
SET IDENTITY_INSERT [StatutoryResponsibilityTaskTemplate] OFF