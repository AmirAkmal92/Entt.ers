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
	v.[LocationId] AS [PupOfficeNo],
	v.[BranchName] AS [PupBranchName], 
	COUNT(*) AS [TotalPup],
	SUM(CASE WHEN [PODDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithPod],
	SUM(CASE WHEN [PODDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutPod]
FROM (  
  SELECT 	p.[StateName],p.[LocationId],p.[BranchName],p.[PODDateTime] 	
  FROM [Entt].[fn_GetPupVsPodByDatePrefix](@reportDate,@day) p 
  ) v
GROUP BY v.[StateName],v.[LocationId],v.[BranchName]
ORDER BY v.[LocationId]


GO

