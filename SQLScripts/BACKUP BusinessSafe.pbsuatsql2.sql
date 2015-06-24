DECLARE @fileDateTime VARCHAR(20)
DECLARE @fileName VARCHAR(256)

SELECT @fileDateTime = CONVERT(VARCHAR(20),GETDATE(),120)
SET @fileName = 'f:\MSSQL.1\MSSQL\Backup\BusinessSafe_New_FULL_' + REPLACE(@fileDateTime, ':', '_') + '.BAK'

BACKUP DATABASE BusinessSafe TO DISK = @fileName WITH COPY_ONLY
