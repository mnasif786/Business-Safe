 
 set xact_abort on
 
 begin transaction
  update [dbo].[ActionPlan]
  set lastmodifiedBy = createdby, lastmodifiedon= createdon
  where lastmodifiedon is null
  
    
  update [dbo].[Action]
  set lastmodifiedBy = createdby, lastmodifiedon= createdon
  where lastmodifiedon is null
  
 commit transaction
  