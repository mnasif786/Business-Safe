use BusinessSafe
go

alter table SafeCheckReportLetterStatementCategory
add Sequence int not null default 0
go

update SafeCheckReportLetterStatementCategory set Name = 'Documentation', Sequence = 0 where Name = 'Management of Health and Safety Documentation'
update SafeCheckReportLetterStatementCategory set Name = 'Work Processes', Sequence = 1 where Name = 'Management of Practices and Procedures'
update SafeCheckReportLetterStatementCategory set Name = 'Work Premises', Sequence = 2 where Name = 'Management of the Premises'
update SafeCheckReportLetterStatementCategory set Name = 'Risk Management', Sequence = 3 where Name = 'Health and Safety Risk Management'

insert into SafeCheckReportLetterStatementCategory (Id,Name, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Deleted, Sequence) values (NEWID(), 'Work Equipment', 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', GETDATE(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', GETDATE(), 0, 4)

go

--//@UNDO				

update SafeCheckReportLetterStatementCategory set Name = 'Management of Practices and Procedures' where Name = 'Work Processes'
update SafeCheckReportLetterStatementCategory set Name = 'Health and Safety Risk Management' where Name = 'Risk Management'
update SafeCheckReportLetterStatementCategory set Name = 'Management of Health and Safety Documentation' where Name = 'Documentation'
update SafeCheckReportLetterStatementCategory set Name = 'Management of the Premises' where Name = 'Work Premises'

delete from SafeCheckReportLetterStatementCategory where name = 'Work Equipment'

GO
