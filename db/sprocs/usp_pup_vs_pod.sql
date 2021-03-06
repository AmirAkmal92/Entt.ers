USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_pup_vs_pod]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_pup_vs_pod]
    @reportDate smalldatetime,
	  @day int
 AS   

SET NOCOUNT ON;   

SELECT 
	v.[StateName],
	v.[OfficeNo] AS [PupOfficeNo],
	v.[BranchName] AS [PupBranchName], 
	COUNT(*) AS [TotalPup],
	SUM(CASE WHEN [PODDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithPod],
	SUM(CASE WHEN [PODDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutPod]
FROM (  
  SELECT DISTINCT 
  a.[StateName], 
  a.[LocationId] AS [OfficeNo], 
  a.[BranchName], 
  a.[ConsignmentNo], 
  a.[DateTime], 
  a.[Comment], 
  d.[OfficeNo] AS [PODOfficeNo], 
  d.[DateTime] AS [PODDateTime], 
  d.[OfficeName] AS [PODBranchName], 
  b.[StateName] AS [PODStateName], 
  d.[Comment] AS [PODComment]	
FROM [Entt].fn_GetPupByDate(@reportDate) a 
LEFT JOIN vw_ALL_POD d ON d.ConsignmentNo = a.ConsignmentNo
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = d.[OfficeNo]
  ) v
GROUP BY v.[StateName],v.[OfficeNo],v.[BranchName]
ORDER BY v.[OfficeNo]


GO

