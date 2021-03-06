USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_vasn_vs_pod_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_vasn_vs_pod_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
  bo.[StateName], 
  v.[OfficeNo], 
  v.[OfficeName], 
  v.[ConsignmentNo], 
  v.[DateTime], 
  v.[Comment],
  p.[DateTime] AS [PodDateTime],
  p.[OfficeNo] AS [PodOfficeNo], 
  p.[OfficeName] AS [PodOfficeName], 
  bo.[StateName] AS [PodStateName], 
  p.[Comment] AS [PodComment]	
FROM dbo.[vw_DISTINCT_VASN] v 
LEFT JOIN dbo.[vw_ALL_POD] p ON p.[ConsignmentNo] = v.[ConsignmentNo]
LEFT JOIN [Entt].BranchStates bo ON bo.BranchCode = v.OfficeNo 
WHERE v.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND v.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND v.[OfficeNo] = @branchCode AND v.[ItemTypeCode] = '01' 
ORDER BY v.[DateTime] DESC

GO
