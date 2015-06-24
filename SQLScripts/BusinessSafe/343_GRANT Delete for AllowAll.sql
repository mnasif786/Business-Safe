USE [BusinessSafe]
GO

EXEC sp_MSforeachtable 'GRANT DELETE ON ? TO [AllowAll]'
GO

--//@UNDO

EXEC sp_MSforeachtable 'REVOKE DELETE ON ? TO [AllowAll]'
GO
