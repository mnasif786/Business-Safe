
DECLARE @Table TABLE( 
	Id UniqueIdentifier NOT NULL,
	Recorded INT 	
); 

DECLARE @Counter INT
SET @Counter = 0 

INSERT INTO @Table (Id)
SELECT  scq.Id 
FROM SafeCheckQuestion scq WHERE 
(scq.Mandatory=1 AND scq.Deleted=0 AND scq.CustomQuestion = 0  AND  scq.Id NOT IN ( 
	SELECT scr.QuestionId FROM SafeCheckQuestionResponse scr WHERE  scr.Title LIKE 'Not Applicable')) 

While (@Counter < (Select Count(*) From @Table)) 
Begin

	DECLARE @QuestionID UniqueIdentifier
	SELECT Top 1 @QuestionID = Id From @Table Where Recorded IS NULL
		 
	INSERT INTO SafeCheckQuestionResponse (Id, Title, ResponseType, QuestionId, CreatedBy, CreatedOn, LastModifiedBy, LastModifiedOn, Deleted )
	VALUES (NEWID(), 'Not Applicable', 'Neutral',@QuestionID, 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', GETDATE(), 'B03C83EE-39F2-4F88-B4C4-7C276B1AAD99', GETDATE(), 0)
	  
	UPDATE @Table SET Recorded = 1 WHERE Id = @QuestionID
	    
    SET @Counter = @Counter + 1 
   
End
