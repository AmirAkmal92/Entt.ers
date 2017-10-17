USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_pod_vs_pup]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_pod_vs_pup]
    @reportDate smalldatetime,
	  @day int
 AS   

SET NOCOUNT ON;   

SELECT 
	v.[StateName],
	v.[OfficeNo],
	v.[BranchName], 
	COUNT(*) AS [TotalPod],
	SUM(CASE WHEN [PupDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithPup],
	SUM(CASE WHEN [PupDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutPup]
FROM (  
  SELECT d.[StateName],d.[OfficeNo],d.[BranchName],d.[DateTime], a.LocationId, b.BranchName AS [PupBranchName], a.[DateTime] AS [PupDateTime]
  FROM [Entt].fn_GetPodByDate(@reportDate) d 
  LEFT JOIN vw_ALL_ACCEPTANCE a ON a.ConsignmentNo = d.ConsignmentNo
  LEFT JOIN [Entt].BranchStates b ON b.BranchCode = a.[LocationId]
  --WHERE d.OfficeNo = '0100'
  --AND a.[DateTime] > DATEADD(d,-7,DATEDIFF(d,0,@reportDate)) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate))
  ) v
GROUP BY v.[StateName],v.[OfficeNo],v.[BranchName]
ORDER BY v.[OfficeNo]


GO

