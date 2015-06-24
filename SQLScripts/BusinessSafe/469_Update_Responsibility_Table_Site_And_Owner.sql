USE [BusinessSafe]
GO
alter table responsibility alter column SiteId bigint null 
alter table responsibility alter column OwnerId uniqueidentifier Null
GO