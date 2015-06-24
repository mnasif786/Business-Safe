IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'AuthorisationToken' AND COLUMN_NAME = 'CreatedOn')
BEGIN
	ALTER TABLE [AuthorisationToken]
	ADD [CreatedOn] DateTime NULL

	ALTER TABLE [AuthorisationToken]
	ADD CONSTRAINT [DF_AuthorisationToken_CreatedOn] DEFAULT (getdate()) FOR [CreatedOn]

	ALTER TABLE [AuthorisationToken]
	ADD CONSTRAINT [DF_AuthorisationToken_CreationDate] DEFAULT (getdate()) FOR [CreationDate]
END
GO	