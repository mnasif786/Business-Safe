USE BusinessSafe
GO

IF NOT EXISTS (SELECT ID FROM [dbo].[Timescale] Where ID = 5)
BEGIN
insert into Timescale values (5, 'Urgent', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),0)
END
GO

IF NOT EXISTS (SELECT ID FROM [dbo].[Timescale] Where ID = 6)
BEGIN
insert into Timescale values (6, 'Six Weeks', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99',GETDATE(),0)
END
GO