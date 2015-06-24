USE [BusinessSafe]
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckImpressionType' AND COLUMN_NAME = 'Comments')
BEGIN
	UPDATE [SafeCheckImpressionType]
	SET Comments = 'The overall standard of health and safety management at this site was below an acceptable level with some health and safety matters requiring urgent management corrective action. Judged against the Enforcing Authorities'' Enforcement Policy Statement this standard of compliance would leave you open to formal enforcement action should you receive an inspection visit. By implementing the recommendations contained within the Action Plan, your present standards of health and safety will be improved and the likelihood of formal enforcement action being taken against you personally or your company will be reduced.'
	WHERE Title = 'Unsatisfactory'
END

--//@UNDO 

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'SafeCheckImpressionType' AND COLUMN_NAME = 'Comments')
BEGIN
	UPDATE [SafeCheckImpressionType]
	SET Comments = 'The overall standard of health and safety management at this site was below an acceptable level with some health and safety matters requiring urgent management corrective action. Judged against the Enforcing Authorities’ Enforcement Policy Statement this standard of compliance would leave you open to formal enforcement action should you receive an inspection visit. By implementing the recommendations contained within the Action Plan, your present standards of health and safety will be improved and the likelihood of formal enforcement action being taken against you personally or your company will be reduced.'
	WHERE Title = 'Unsatisfactory'
END
