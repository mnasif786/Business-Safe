
IF NOT EXISTS ( SELECT  *
                FROM    sys.indexes AS i
                WHERE   name = 'Ix_Task_For_Searching' ) 
    BEGIN

        CREATE INDEX Ix_Task_For_Searching
        ON [dbo].[Task] ([Deleted],[Discriminator],[TaskAssignedToId],[TaskStatusId])
        INCLUDE ([Id],[MultiHazardRiskAssessmentHazardId],[Title]
        ,[Description],[Reference],[CreatedOn],[TaskCompletionDueDate]
        ,[TaskCompletedDate],[TaskCompletedComments],[TaskCategoryId])

    END
     

IF NOT EXISTS ( SELECT  *
                FROM    sys.indexes AS i
                WHERE   name = 'Ix_Employee_Name' ) 
    BEGIN
        CREATE INDEX Ix_Employee_Name
        ON [dbo].[Employee]  (Id,SiteId,ClientId)
        INCLUDE(Forename,Surname)
    END

IF NOT EXISTS ( SELECT  *
                FROM    sys.indexes AS i
                WHERE   name = 'Ix_MultiHazardRiskAssessmentHazard_For_Searching' ) 
    BEGIN
        CREATE INDEX Ix_MultiHazardRiskAssessmentHazard_For_Searching
        ON [dbo].[MultiHazardRiskAssessmentHazard]  (Id,RiskAssessmentId,[Deleted])

    END
