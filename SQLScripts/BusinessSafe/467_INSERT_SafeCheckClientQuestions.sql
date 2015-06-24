USE [BusinessSafe]
GO

insert into SafeCheckClientQuestion(Id,ClientId,ClientAccountNumber, QuestionId)
select NEWID(),55881 as 'ClientID', 'DEN101' as 'ClientAccountNumber', q.Id as 'QuestionId' from SafeCheckQuestion q 
	
go	