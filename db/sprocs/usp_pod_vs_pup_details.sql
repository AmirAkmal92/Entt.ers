USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_pod_vs_pup_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_pod_vs_pup_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
  bd.[StateName], 
  d.[OfficeNo], 
  d.[OfficeName], 
  d.[ConsignmentNo], 
  d.[DateTime], 
  d.[Comment],
  p.[LocationId] AS [PupOfficeNo], 
  p.[LocationName] AS [PupBranchName], 
  p.[DateTime] AS [PupDateTime], 
  NULL AS [PupComment]
FROM [dbo].[vw_ALL_POD] d 
LEFT JOIN [dbo].[vw_ALL_PUP] p ON p.[ConsignmentNo] = d.[ConsignmentNo]  
LEFT JOIN [Entt].BranchStates bd ON bd.BranchCode = d.OfficeNo  
WHERE d.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND d.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND d.[OfficeNo] = @branchCode

GO

