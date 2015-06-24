UPDATE Task SET Discriminator = 'GeneralRiskAssessmentFurtherControlMeasureTask' WHERE Discriminator = 'FurtherControlMeasureTask'

--//@UNDO 

UPDATE Task SET Discriminator = 'FurtherControlMeasureTask' WHERE Discriminator = 'GeneralRiskAssessmentFurtherControlMeasureTask'