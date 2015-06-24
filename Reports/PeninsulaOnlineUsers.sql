USE PeninsulaOnline
SELECT  u.Id
		,c.CustomerKey
       ,u.UserName
       ,e.Forename
       ,e.Surname
       ,CASE WHEN r.NAME = 'UserAdmin' OR r.Name IS NULL THEN 1 ELSE 0 END AS IsAdmin
       ,m.IsApproved AS IsRegistered
       ,m.IsLockedOut
       ,m.RegistrationExpiryDate
       ,m.Deleted AS IsPeninsulaOnlineDeleted
       ,CASE WHEN u2.UserId IS NOT NULL THEN 1 ELSE 0 END AS IsInBSO
       ,u2.Deleted AS IsBsoUserDeleted
       ,e.Deleted AS IsBsoEmployeeDeleted
FROM    dbo.[User] AS u
        INNER JOIN dbo.Membership AS m ON u.Id = m.UserId
        LEFT JOIN BusinessSafe.dbo.[User] AS u2 ON u.Id = u2.UserId
        LEFT JOIN BusinessSafe.dbo.Employee AS e ON u2.EmployeeId = e.Id
        INNER JOIN Peninsula.dbo.tblcustomers c ON u.ClientId = c.CustomerId
        LEFT JOIN BusinessSafe.dbo.Role AS r ON u2.RoleId = r.RoleId
ORDER BY c.CustomerKey 



       
      


