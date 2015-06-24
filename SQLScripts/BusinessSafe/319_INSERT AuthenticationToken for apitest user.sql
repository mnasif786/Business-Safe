IF NOT EXISTS(
	SELECT  * FROM dbo.[AuthenticationToken] AS at
	WHERE Id = N'3f2139db-d478-4a46-9ae1-00fceb73df86')
BEGIN
	INSERT [dbo].[AuthenticationToken] ([Id], [ApplicationTokenId], [UserId], [CreatedOn], [LastAccessDate], [IsEnabled], [ReasonForDeAuthorisation]) 
	VALUES (N'3f2139db-d478-4a46-9ae1-00fceb73df86', N'39046f90-5967-43b8-be7b-aab4bc28ee5e', N'24fe13ca-cad8-4ebd-bb64-05109a2dbd1e', CAST(0x0000A1AB00F431AC AS DateTime), CAST(0x0000A1AB00F431AC AS DateTime), 1, NULL)	
END


