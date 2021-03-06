USE [BusinessSafe]
GO
	-- Add default hazard types
print 'Add default hazard types'
SET IDENTITY_INSERT [BusinessSafe].[dbo].[HazardType] ON

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (1, 'General', 0, GETDATE(), NULL, NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (2, 'Personal', 0, GETDATE(), NULL, NULL, NULL)

INSERT INTO [BusinessSafe].[dbo].[HazardType] ([Id], [Name], [Deleted], [CreatedOn], [CreatedBy], [LastModifiedOn], [LastModifiedBy])
VALUES (3, 'Hazardous Substance', 0, GETDATE(), NULL, NULL, NULL)

print 'Add default hazard types END'

SET IDENTITY_INSERT [BusinessSafe].[dbo].[HazardType] OFF
	

--//@UNDO
print 'deleting HazardTypes'
DELETE
FROM [BusinessSafe].[dbo].[HazardType]
WHERE id IN ( 1, 2, 3)
