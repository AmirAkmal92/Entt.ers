USE [Entt]
GO

/****** Object:  StoredProcedure [dbo].[ipc_no_acceptance_branch_summary]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[ipc_no_acceptance_branch_summary]
    @reportDate smalldatetime,
	  @branchCode nvarchar(50)
AS   

SET NOCOUNT ON;   

SELECT 
	v.[StateName],
	v.[LocationId],
	v.[LocationName], 
	COUNT(*) AS [Total]  
FROM (
SELECT 
	a.[DateTime]
    ,a.[ConsignmentNo]
    ,a.[ItemCategory]
    ,a.[CourierId]
    ,a.[LocationId]
    ,a.[LocationName]
    ,a.[Postcode]
    ,b.[StateName]
	,s.[PROCDT]
	,b.[Order]
FROM [Entt].[Acceptance] a 
LEFT JOIN [Entt].BranchStates b ON b.[BranchCode] = a.[LocationId] 
LEFT JOIN [Entt].[vw_IPC_SORTING_RESULT] s ON s.[ITEMNO] = a.[ConsignmentNo] AND s.[INITDT] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND s.[INITDT] < DATEADD(d,7,DATEDIFF(d,0,@reportDate)) 
WHERE a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND s.[PROCDT] IS NULL AND b.[StateName] IN ('SELANGOR','WP KUALA LUMPUR','WP PUTRAJAYA') AND b.[BranchType] = 'Pusat POS Laju' AND a.[LocationId] = @branchCode 
) v 
GROUP BY v.[LocationId], v.[LocationName], v.[StateName], v.[Order]
ORDER BY v.[Order], v.[LocationId]

GO
