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
	d.[ConsignmentNo],
	d.[StateName],
	d.[OfficeNo],
	d.[BranchName],
	d.[DateTime], 
	d.[Comment],
	a.[LocationId] AS [PupOfficeNo], 
	b.BranchName AS [PupBranchName], 
	a.[DateTime] AS [PupDateTime],
	a.[Comment] AS [PupComment]
FROM [Entt].fn_GetPodByDate(@reportDate) d 
  LEFT JOIN vw_ALL_ACCEPTANCE a ON a.ConsignmentNo = d.ConsignmentNo
  LEFT JOIN [Entt].BranchStates b ON b.BranchCode = a.[LocationId]
  WHERE d.[OfficeNo] = @branchCode
  ORDER BY d.[DateTime]

GO

