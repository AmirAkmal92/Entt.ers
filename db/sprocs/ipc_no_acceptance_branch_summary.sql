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
  s.[ITEMNO] AS [ConsignmentNo]
  ,s.[PROCDT]  
  ,s.[BRANCHD4CODE] AS [LocationId]
  ,a.[DateTime]
  ,a.[ItemCategory]
  ,a.[CourierId]
  ,ISNULL(b.[BranchName],'Unknown') AS [LocationName]
  ,s.[RECPRSNZIPCD] AS [Postcode]
  ,ISNULL(b.[StateName],'UNKNOWN') AS [StateName]
  ,ISNULL(b.[Order], 99) AS [Order] 
FROM [Entt].[vw_IPC_SORTING_RESULT] s 
LEFT JOIN [Entt].[Acceptance] a ON a.[ConsignmentNo] = s.[ITEMNO] 
LEFT JOIN [Entt].BranchStates b ON b.[BranchCode] = s.[BRANCHD4CODE] 
WHERE s.[INITDT] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND s.[INITDT] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
--AND b.[BranchType] IN ('Pusat POS Laju','e_Commerce Processing Centre','POS Laju Warehouse') 
AND a.[DateTime] IS NULL 
AND s.[BRANCHD4CODE] = @branchCode 
) v 
GROUP BY v.[LocationId], v.[LocationName], v.[StateName], v.[Order]
ORDER BY v.[Order], v.[LocationId]

GO
