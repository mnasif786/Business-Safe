USE [BusinessSafe]
Go

delete from SafeCheckCheckListQuestion
go

if not exists ( select 1 from SafeCheckCategory where Id = '2713E9BC-0D40-4DBD-95F2-2D6FC29AD96A')
begin
insert into SafeCheckCategory values ('2713E9BC-0D40-4DBD-95F2-2D6FC29AD96A','Training / Competancy','',0)
end
if not exists ( select 1 from SafeCheckCategory where Id = 'B6F42619-C452-4CFB-88D4-5BFAD0BA1FBC')
begin
insert into SafeCheckCategory values ('B6F42619-C452-4CFB-88D4-5BFAD0BA1FBC','Fire & Emergency','',0)
end
if not exists ( select 1 from SafeCheckCategory where Id = '074B964B-2E75-42E0-BC00-60B708EF1E37')
begin
insert into SafeCheckCategory values ('074B964B-2E75-42E0-BC00-60B708EF1E37','Other Subjects','',0)
end
if not exists ( select 1 from SafeCheckCategory where Id = '4ECCE4AA-DC20-4E0F-B02B-72C67EEBE0F0')
begin
insert into SafeCheckCategory values ('4ECCE4AA-DC20-4E0F-B02B-72C67EEBE0F0','First Aid','',0)
end
if not exists ( select 1 from SafeCheckCategory where Id = 'DA7DB3F3-9A33-4D00-ACFD-B305B21F6182')
begin
insert into SafeCheckCategory values ('DA7DB3F3-9A33-4D00-ACFD-B305B21F6182','Environment','',0)
end
go