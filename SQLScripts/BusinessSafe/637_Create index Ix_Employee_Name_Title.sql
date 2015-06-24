IF NOT EXISTS ( SELECT  *
                FROM    sys.indexes AS i
                WHERE   name = 'Ix_Employee_Name_Title' ) 
    BEGIN
        CREATE NONCLUSTERED INDEX Ix_Employee_Name_Title
        ON [dbo].[Employee] ([ClientId],[Deleted])
        INCLUDE ( [Id],
        [Forename],
        [Surname],
        [JobTitle])
    END
GO

