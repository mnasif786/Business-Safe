USE BusinessSafe
GO

IF EXISTS (SELECT ID FROM [dbo].[Timescale] WHERE ID = 5)
BEGIN
DELETE FROM Timescale where ID = 5
END 

IF EXISTS (SELECT ID FROM [dbo].[Timescale] WHERE ID = 6)
BEGIN
DELETE FROM Timescale where ID = 6
END 

IF NOT EXISTS (SELECT ID FROM [dbo].[Timescale] Where ID = 5)
BEGIN
INSERT INTO Timescale VALUES (5,'Six Weeks', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),0)
END
GO
