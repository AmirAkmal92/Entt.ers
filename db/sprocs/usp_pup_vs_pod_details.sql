USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_pup_vs_pod_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_pup_vs_pod_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

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
WHERE a.[LocationId] = @branchCode
ORDER BY a.[DateTime] DESC

GO

