USE [BusinessSafe]
GO

IF  NOT EXISTS( SELECT * FROM SafeCheckQaAdvisor WHERE Email = 'george.cooper@peninsula-uk.com')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES (NEWID(), 'George', 'Cooper','george.cooper@peninsula-uk.com', 0)
END
GO

--//@UNDO 

IF EXISTS( SELECT * FROM SafeCheckQaAdvisor WHERE Email = 'george.cooper@peninsula-uk.com')
BEGIN
	DELETE FROM [dbo].[SafeCheckQaAdvisor] WHERE [Email] = 'george.cooper@peninsula-uk.com'
END


select * from [SafeCheckQaAdvisor]

