DECLARE @clientId AS BIGINT
SET @clientId = 31028

BEGIN TRAN

DELETE FROM EmployeeContactDetails
WHERE EmployeeId IN
(
	SELECT Id
	FROM Employee
	WHERE ClientId = @clientId
)

DELETE FROM Employee
WHERE ClientId = @clientId

DELETE FROM Task
WHERE LastModifiedBy IN
(
	SELECT UserId 
	FROM [User]
	WHERE ClientId = @clientId
)

DELETE FROM [User]
WHERE ClientId = @clientId

DELETE FROM [PeninsulaOnline].dbo.[Membership]
WHERE UserId IN
(
	SELECT Id 
	FROM [PeninsulaOnline].dbo.[User]
	WHERE ClientId = @clientId
)

DELETE FROM [PeninsulaOnline].dbo.UserApplication
WHERE UserId IN
(
	SELECT Id 
	FROM [PeninsulaOnline].dbo.[User]
	WHERE ClientId = @clientId
)

DELETE FROM [PeninsulaOnline].dbo.[User]
WHERE ClientId = @clientId

--COMMIT TRAN
--ROLLBACK TRAN

