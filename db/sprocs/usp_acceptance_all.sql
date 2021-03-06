USE [Entt]
GO
/****** Object:  StoredProcedure [Entt].[usp_acceptance_all]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_acceptance_all]
    @reportDate datetime
 AS   

SET NOCOUNT ON;   

SELECT 
  b.[StateName],
	b.[BranchName],
	v.[LocationId],
	COUNT(*) AS [Total],
	SUM(v.[Pickup]) AS [TotalPickup] 
FROM (
  SELECT 
    a.[LocationId],
    CASE WHEN a.[PickupNo] IS NOT NULL AND a.[SystemId] = '002' THEN 1 ELSE 0 END AS [Pickup]
  FROM [Entt].[Entt].[Acceptance] a 
  WHERE 
    a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND 
    a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate))
  ) v
INNER JOIN [Entt].[BranchStates] b ON b.[BranchCode] = v.[LocationId]
GROUP BY b.StateName, b.[BranchName], v.[LocationId]
ORDER BY [LocationId] 
 
GO
