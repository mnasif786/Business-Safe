USE [BusinessSafe]
GO

IF NOT EXISTS( SELECT * FROM SafeCheckQaAdvisor where Email =  'David.Brierley@peninsula-uk.com')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES (NEWID(), 'David', 'Brierley','David.Brierley@peninsula-uk.com', 0)
END

IF NOT EXISTS( SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Diane.Smith@peninsula-uk.com')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES (NEWID(), 'Diane', 'Smith','Diane.Smith@peninsula-uk.com', 0)
END

IF NOT EXISTS( SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Sinead.Lewis@peninsula-uk.com')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES (NEWID(), 'Sinead', 'Lewis','Sinead.Lewis@peninsula-uk.com', 0)
END

IF NOT EXISTS( SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Gary.Armitt@peninsula-uk.com')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES (NEWID(), 'Gary', 'Armitt','Gary.Armitt@peninsula-uk.com', 0)
END


IF NOT EXISTS( SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Paul.Leather@peninsula-uk.com')
BEGIN		
	INSERT INTO [dbo].[SafeCheckQaAdvisor]([Id], [Forename], [Surname], [Email], [Deleted]) VALUES (NEWID(), 'Paul', 'Leather','Paul.Leather@peninsula-uk.com', 0)
END

GO

--//@UNDO 


IF EXISTS(SELECT * FROM SafeCheckQaAdvisor where Email =  'David.Brierley@peninsula-uk.com')
BEGIN		
	DELETE FROM SafeCheckQaAdvisor where Email =  'David.Brierley@peninsula-uk.com'
END

IF EXISTS(SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Diane.Smith@peninsula-uk.com')
BEGIN		
	DELETE FROM [SafeCheckQaAdvisor] where [Email] = 'Diane.Smith@peninsula-uk.com'
END

IF EXISTS(SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Sinead.Lewis@peninsula-uk.com')
BEGIN		
	DELETE FROM [SafeCheckQaAdvisor] where [Email] = 'Sinead.Lewis@peninsula-uk.com'
END

IF EXISTS(SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Gary.Armitt@peninsula-uk.com')
BEGIN		
	DELETE FROM [SafeCheckQaAdvisor] where [Email] = 'Gary.Armitt@peninsula-uk.com'
END


IF EXISTS(SELECT * FROM [SafeCheckQaAdvisor] where [Email] = 'Paul.Leather@peninsula-uk.com')
BEGIN		
	DELETE FROM [SafeCheckQaAdvisor] where [Email] = 'Paul.Leather@peninsula-uk.com'
END
GO