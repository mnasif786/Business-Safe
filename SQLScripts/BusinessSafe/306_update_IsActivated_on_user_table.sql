
SET XACT_ABORT ON

BEGIN TRANSACTION

UPDATE dbo.[User]
SET IsActivated = 0
FROM dbo.[User] AS u
	INNER JOIN PeninsulaOnline.dbo.[User] AS u2 ON u.UserId = u2.Id
	INNER JOIN PeninsulaOnline.dbo.Membership AS m ON u2.Id = m.UserId
WHERE m.IsApproved = 0
AND u.IsActivated IS NULL


UPDATE dbo.[User]
SET IsActivated = 1
FROM dbo.[User] AS u
	INNER JOIN PeninsulaOnline.dbo.[User] AS u2 ON u.UserId = u2.Id
	INNER JOIN PeninsulaOnline.dbo.Membership AS m ON u2.Id = m.UserId
WHERE m.IsApproved = 1
AND (u.IsActivated = 0 OR u.IsActivated IS NULL)


COMMIT TRANSACTION

/*
SELECT u.IsActivated, * FROM dbo.[User] AS u
	INNER JOIN PeninsulaOnline.dbo.[User] AS u2 ON u.UserId = u2.Id
	INNER JOIN PeninsulaOnline.dbo.Membership AS m ON u2.Id = m.UserId
WHERE m.IsApproved = 0
AND u.IsActivated IS NULL


SELECT u.IsActivated, * FROM dbo.[User] AS u
	INNER JOIN PeninsulaOnline.dbo.[User] AS u2 ON u.UserId = u2.Id
	INNER JOIN PeninsulaOnline.dbo.Membership AS m ON u2.Id = m.UserId
WHERE m.IsApproved = 1
AND (u.IsActivated = 0 OR u.IsActivated IS NULL)
*/