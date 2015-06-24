USE [BusinessSafe]
GO
insert into SafeCheckIndustry(Id,Name) values(NEWID(),'Retail')
insert into SafeCheckIndustry(Id,Name) values(NEWID(),'Leisure')
insert into SafeCheckIndustry(Id,Name) values(NEWID(),'Manufacturing')
insert into SafeCheckIndustry(Id,Name) values(NEWID(),'Care')
insert into SafeCheckIndustry(Id,Name) values(NEWID(),'Education')

go

insert into SafeCheckIndustryQuestion (Id,IndustryId,QuestionId)
select NEWID(),i.Id as 'IndustryId',q.Id as 'QuestionId' from SafeCheckIndustry i 
	cross join SafeCheckQuestion q 
	
	