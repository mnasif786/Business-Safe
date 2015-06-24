IF NOT EXISTS ( SELECT  *
                FROM    sys.objects AS o
                WHERE   o.name = 'SafeCheckConsultant' ) 
    BEGIN
        CREATE TABLE [dbo].SafeCheckConsultant
            (
             [Id] [uniqueidentifier] NOT NULL
             ,Username VARCHAR(100) NOT NULL
            ,[Forename] [varchar](100)
            ,[Surname] [varchar](100)
            ,[Email] [varchar](100)
            ,[Deleted] [bit] NOT NULL  DEFAULT ( 0 )
            ,[PercentageOfChecklistsToSendToQualityControl] [smallint] DEFAULT ( 0 )
            ,CONSTRAINT [PK_SafeCheckConsultant] PRIMARY KEY CLUSTERED ( [Id] )
            )
	
    END

GO

GRANT SELECT,INSERT,UPDATE ON dbo.SafeCheckConsultant TO AllowAll
GRANT SELECT,INSERT,UPDATE ON dbo.SafeCheckConsultant TO AllowSelectInsertUpdate

GO

INSERT INTO dbo.SafeCheckConsultant
        ( Id
        ,Username
        ,Forename
        ,Surname
        ,Email
        ,PercentageOfChecklistsToSendToQualityControl
        )
SELECT 'B68B66EA-5488-474A-9690-0D79497DAAB9', 'Alan.Kelly', 'Alan', 'Kelly', 'Alan.Kelly@peninsula-uk.com', 20
UNION SELECT 'EB387F92-7E92-4ED7-806C-19BD098A9E9F', 'Diana.Howlett', 'Diana', 'Howlett', 'Diana.Howlett@peninsula-uk.com', 20
UNION SELECT 'C57F59C5-47F2-44B9-A57E-5A81F2C5FB3C', 'Gary.Armitt', 'Gary', 'Armitt', 'Gary.Armitt@peninsula-uk.com', 20
UNION SELECT '4A89AADA-3A94-4BA2-B04C-E16DAC257930', 'Jamie.Fogg', 'Jamie', 'Fogg', 'Jamie.Fogg@peninsula-uk.com', 20
UNION SELECT '6A37580F-3488-40CC-B542-D898CDA36100', 'Jane.Neil', 'Jane', 'Neil', 'Jane.Neil@peninsula-uk.com', 20
UNION SELECT 'F61635A4-419D-46CC-8FC2-490727E52032', 'Jimmy.Tse', 'Jimmy', 'Tse', 'Jimmy.Tse@peninsula-uk.com', 20
UNION SELECT '8ABD4678-052E-4DDD-943F-5E4EE5AA58A9', 'Joe.Heaney', 'Joe', 'Heaney', 'Joe.Heaney@peninsula-uk.com', 20
UNION SELECT 'C01D6BBD-DC73-4438-8992-5408ADFA6AC3', 'Martin.Stretton', 'Martin', 'Stretton', 'Martin.Stretton@peninsula-uk.com', 20
UNION SELECT 'F07B36D2-1881-4A40-BC79-AB5688C9010F', 'Paul.Jackson', 'Paul', 'Jackson', 'Paul.Jackson@peninsula-uk.com', 20
UNION SELECT '63A0740F-C3FF-4D76-9B99-CA95EB9E3EE2', 'Paul.Starkey', 'Paul', 'Starkey', 'Paul.Starkey@peninsula-uk.com', 20
UNION SELECT '6CBD8C6B-C9FD-4596-9C85-0B916C8BFF54', 'Simon.Hopes', 'Simon', 'Hopes', 'Simon.Hopes@peninsula-uk.com', 20
UNION SELECT 'C0A0B57F-6919-4EA4-B3A1-9DA91100426A', 'Tony.Reid', 'Tony', 'Reid', 'Tony.Reid@peninsula-uk.com', 20
