USE [Entt]
GO

/****** Object:  StoredProcedure [dbo].[ipc_item_under_declared_branch_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[ipc_item_under_declared_branch_details]
    @reportDate smalldatetime,
	  @branchCode nvarchar(50)
AS   

SET NOCOUNT ON;   

/*
SELECT 
 v.[LocationId],
 v.[LocationName], 
 COUNT(*) AS [Total],
 SUM(CASE WHEN [ItemCategory] = '01' THEN 1 ELSE 0 END) AS [TotalParcel], 
 SUM(CASE WHEN [ItemCategory] = '02' THEN 1 ELSE 0 END) AS [TotalDocument],
 SUM(CASE WHEN [ItemCategory] = '01' THEN [Weight] ELSE 0 END) AS [TotalParcelWeight], 
 SUM(CASE WHEN [ItemCategory] = '02' THEN [Weight] ELSE 0 END) AS [TotalDocumentWeight],
 0 AS [TotalIpc],
 0.0 AS [TotalIpcWeight]
FROM 
(
	SELECT 
		  [DateTime]
		  ,[ConsignmentNo]
		  ,[ItemCategory]
		  ,[Weight]
		  ,[CourierId]
		  ,[LocationId]
		  ,[LocationName]
		  ,[Pl9No]
		  ,[Postcode]
	FROM [Entt].[Acceptance] a 
	WHERE a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
	AND a.[LocationId] = @branchCode 
) v 
GROUP BY v.[LocationId], v.[LocationName]
*/

SELECT 
      a.[DateTime]
      ,a.[ConsignmentNo]
      ,a.[ItemCategory]
      ,a.[Weight]
      ,a.[CourierId]
      ,a.[LocationId]
      ,a.[LocationName]
      ,a.[Pl9No]
      ,a.[Postcode]
      ,b.[StateName]
      ,CASE WHEN [ItemCategory] = '01' THEN 1 ELSE 0 END AS [IsParcel]
      ,CASE WHEN [ItemCategory] = '02' THEN 1 ELSE 0 END AS [IsDocument]
      ,CASE WHEN [ItemCategory] NOT IN ('01','02') THEN 1 ELSE 0 END AS [IsOther]
FROM [Entt].[Acceptance] a 
LEFT JOIN [Entt].BranchStates b ON b.[BranchCode] = a.[LocationId] 
WHERE a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND a.[LocationId] = @branchCode
ORDER BY a.[DateTime]
  
GO
