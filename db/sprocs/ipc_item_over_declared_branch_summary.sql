USE [Entt]
GO

/****** Object:  StoredProcedure [dbo].[ipc_item_over_declared_branch_summary]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[ipc_item_over_declared_branch_summary]
    @reportDate smalldatetime,
	@branchCode nvarchar(50)
AS   

SET NOCOUNT ON;   

SELECT 
	v.[StateName],
	v.[LocationId],
	v.[LocationName], 
	COUNT(*) AS [Total],
	SUM(CASE WHEN [ItemCategory] = '01' THEN 1 ELSE 0 END) AS [TotalParcel], 
	SUM(CASE WHEN [ItemCategory] = '02' THEN 1 ELSE 0 END) AS [TotalDocument],
	SUM(CASE WHEN [ItemCategory] NOT IN ('01','02') THEN 1 ELSE 0 END) AS [TotalOthers],
	SUM(CASE WHEN [ItemCategory] = '01' THEN [Weight] ELSE 0 END) AS [TotalParcelWeight], 
	SUM(CASE WHEN [ItemCategory] = '02' THEN [Weight] ELSE 0 END) AS [TotalDocumentWeight],
	SUM(v.[IpcWeight]/1000) AS [TotalIpcWeight]
FROM 
(
SELECT 	
  s.[ITEMNO]
  ,s.[PROCDT]
  ,a.[DateTime]
  ,a.[ConsignmentNo]
  ,a.[ItemCategory]
  ,a.[Weight] AS [Weight]
  ,s.[ITEMACTUALWGHT] AS [IpcWeight] 
  ,a.[CourierId]
  ,a.[LocationId]
  ,a.[LocationName]
  ,a.[Pl9No]
  ,a.[Postcode]
  ,b.[StateName]
  ,b.[Order]
  ,CASE WHEN [ItemCategory] = '01' THEN 1 ELSE 0 END AS [IsParcel]
  ,CASE WHEN [ItemCategory] = '02' THEN 1 ELSE 0 END AS [IsDocument]
  ,CASE WHEN [ItemCategory] NOT IN ('01','02') THEN 1 ELSE 0 END AS [IsOther]
FROM [Entt].[vw_IPC_SORTING_RESULT] s 
LEFT JOIN [Entt].[Acceptance] a ON a.[ConsignmentNo] = s.[ITEMNO] 
LEFT JOIN [Entt].BranchStates b ON b.[BranchCode] = a.[LocationId] 
WHERE s.[INITDT] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND s.[INITDT] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND b.[BranchType] IN ('Pusat POS Laju','e_Commerce Processing Centre','POS Laju Warehouse') AND a.[LocationId] = @branchCode 
AND a.[Weight] > s.[ITEMACTUALWGHT]/1000 
) v 
GROUP BY v.[StateName], v.[LocationId], v.[LocationName], v.[Order] 
ORDER BY v.[Order], v.[LocationId]

GO
