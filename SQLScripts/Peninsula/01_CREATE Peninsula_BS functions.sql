----------------------------------------------------------------------------------------------------------------------------------------------------------

			/* CREATE the Peninsula_BS functions */

----------------------------------------------------------------------------------------------------------------------------------------------------------
USE [Peninsula_BS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create Function [dbo].[JustAlphaNumeric] (@StrInput varchar( 100))
	returns varchar(100)
	with schemabinding
	as
	begin
		declare @retVal as varchar(100)
		set @retVal = replace(@StrInput,' ','')
		set @retVal = replace(@retVal,'(','')
		set @retVal = replace(@retVal,')','')
		set @retVal = replace(@retVal,'£','')
		set @retVal = replace(@retVal,'.','')
		set @retVal = replace(@retVal,'/','')
		set @retVal = replace(@retVal,'\','')
		set @retVal = replace(@retVal,'''','')
		set @retVal = replace(@retVal,'@','')
		set @retVal = replace(@retVal,'&','')
		set @retVal = replace(@retVal,'-','')
		set @retVal = replace(@retVal,',','')
		

		return @retVal
	end	

GO
