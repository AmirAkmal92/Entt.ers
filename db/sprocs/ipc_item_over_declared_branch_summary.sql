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
		  ,b.[StateName]
		  ,b.[Order]
	FROM [Entt].[Acceptance] a 
	LEFT JOIN [Entt].BranchStates b ON b.[BranchCode] = a.[LocationId] 
	WHERE a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
	AND b.[BranchType] = 'Pusat POS Laju' AND a.[LocationId] = @branchCode 
) v 
GROUP BY v.[StateName], v.[LocationId], v.[LocationName], v.[Order] 
ORDER BY v.[Order], v.[LocationId]

GO
