use BusinessSafe
go

insert into SafeCheckQaAdvisor (Id, Forename, Surname, Email, Deleted, InRotation) values(NEWID(), 'Michael','Thomson','Michael.Thomson@peninsula-uk.com',0,0)
insert into SafeCheckQaAdvisor (Id, Forename, Surname, Email, Deleted, InRotation) values(NEWID(), 'John','Mvududu','John.Mvududu@peninsula-uk.com',0,0)
insert into SafeCheckQaAdvisor (Id, Forename, Surname, Email, Deleted, InRotation) values(NEWID(), 'Lynne','Symes','Lynne.Symes@peninsula-uk.com',0,0) 
insert into SafeCheckQaAdvisor (Id, Forename, Surname, Email, Deleted, InRotation) values(NEWID(), 'Simon','Zeigler','simon.zeigler@peninsula-uk.com',0,0)
insert into SafeCheckQaAdvisor (Id, Forename, Surname, Email, Deleted, InRotation) values(NEWID(), 'Ian','Smith','Ian.Smith@peninsula-uk.com',0,0) 

delete from SafeCheckQaAdvisor where id = '4D28A450-B3CA-4A9F-83B1-AF288020DA90'
