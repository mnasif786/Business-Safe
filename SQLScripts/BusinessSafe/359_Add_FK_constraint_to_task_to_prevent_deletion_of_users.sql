
IF NOT EXISTS (SELECT * FROM sys.objects AS o
WHERE o.type = 'F'
	AND name = 'FK_Task_LastModifiedBy')
	BEGIN
		
		ALTER TABLE dbo.Task
		ADD CONSTRAINT FK_Task_LastModifiedBy FOREIGN KEY (LastModifiedBy) REFERENCES dbo.[User](UserId)
	END
	
	
