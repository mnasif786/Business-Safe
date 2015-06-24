USE [BusinessSafe]
GO

alter table SafeCheckIndustry alter column Name nvarchar(100) null
go

insert into SafeCheckIndustry(Id,Name) values(NEWID(),'Default')
go

insert into SafeCheckIndustryQuestion (Id,IndustryId,QuestionId)
select NEWID(),i.Id as 'IndustryId',q.Id as 'QuestionId' from SafeCheckIndustry i 
	cross join SafeCheckQuestion q 
	where i.Name = 'Default'
	
