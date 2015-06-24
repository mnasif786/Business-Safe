use BusinessSafe
go
CREATE NONCLUSTERED INDEX [IX_SafeCheckCheckListAnswer_Id] ON [dbo].[SafeCheckCheckListAnswer] 
(
	[CheckListId] ASC
)
INCLUDE ( [Id],
[QaComments],
[QaSignedOffBy]) WITH (SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF) ON [PRIMARY]
