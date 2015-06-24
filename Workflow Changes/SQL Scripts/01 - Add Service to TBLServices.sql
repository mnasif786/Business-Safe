
set identity_insert [Peninsula].[dbo].[TBLServices] ON

INSERT INTO [Peninsula].[dbo].[TBLServices]
           ([ServiceTypeId]
           ,[Title]
           ,[Description]
           ,[MinimumFee]
           ,[MaximumFee]
           ,[CreatedBy]
           ,[CreatedOn]           
           ,[OrderBy]
           ,[ContractCheckBoxPosition]
           ,[flagHide]           
           ,[HideEnterCost]
           ,[CreatesWorkflow]
          )
    SELECT a.a, a.b, a.c, a.d, a.e, a.f, a.g, a.h, a.k, a.l, a.m, a.n  FROM (
	SELECT 48 as a, 'BusinessSafe Online' as b, 'BusinessSafe Online' as c, 0 as d, 0 as e, 'Nima.Montazer' as f, getdate() as g, 105 as h, null as k, 0 as l, 0 as m, 0 as n
	--UNION SELECT 288 as a, 'Small Business Visit' as b, NULL as c, 0 as d, 1 as e, '/PBSNet2005/GUI/Applications/HealthAndSafety/frmTimesheetPopup.aspx?' as f, NULL as g, null as h
	) as a
	LEFT JOIN [Peninsula].[dbo].[TBLServices] b ON b.[ServiceTypeID] = a.a
	WHERE b.[ServiceTypeID] IS NULL
GO

set identity_insert [Peninsula].[dbo].[TBLServices] OFF



INSERT INTO [Peninsula].[dbo].[TBLServicesServiceGroups]
           ([ServiceTypeID]
           ,[ServiceGroupID]
           ,[FeePercentage]
          )
    SELECT a.a, a.b, a.c FROM (
	SELECT 48 as a, 3 as b, 0 as c
	--UNION SELECT 288 as a, 'Small Business Visit' as b, NULL as c, 0 as d, 1 as e, '/PBSNet2005/GUI/Applications/HealthAndSafety/frmTimesheetPopup.aspx?' as f, NULL as g, null as h
	) as a
	LEFT JOIN [Peninsula].[dbo].[TBLServicesServiceGroups] b ON b.[ServiceTypeID] = a.a
	WHERE b.[ServiceTypeID] IS NULL
GO



