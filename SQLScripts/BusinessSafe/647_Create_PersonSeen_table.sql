IF NOT EXISTS(SELECT * FROM sys.objects AS o
WHERE o.name = 'SafeCheckChecklistPersonSeen')
BEGIN
	
	CREATE TABLE SafeCheckChecklistPersonSeen (
		Id UNIQUEIDENTIFIER NOT NULL
		,ChecklistId UNIQUEIDENTIFIER NOT NULL
		,EmployeeId UNIQUEIDENTIFIER NULL
		,FullName VARCHAR(400)
		,EmailAddress VARCHAR(100)
		,CreatedOn DATETIME
		,LastModifiedOn DATETIME
		,CONSTRAINT PK_SafeCheckChecklistPersonSeen PRIMARY KEY (Id)
		,CONSTRAINT FK_SafeCheckChecklistPersonSeen_SafeCheckChecklist FOREIGN KEY (ChecklistId) REFERENCES SafeCheckChecklist (Id)
		,CONSTRAINT FK_SafeCheckChecklistPersonSeen_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee (Id)
	)
	
END
GO
GRANT SELECT, INSERT, UPDATE, DELETE ON SafeCheckChecklistPersonSeen TO AllowAll
GRANT SELECT, INSERT, UPDATE, DELETE ON SafeCheckChecklistPersonSeen TO AllowSelectInsertUpdate


--DROP TABLE SafeCheckChecklistPersonSeen