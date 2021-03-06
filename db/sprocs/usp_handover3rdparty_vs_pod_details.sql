USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_handover3rdparty_vs_pod_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_handover3rdparty_vs_pod_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
	b.[StateName],
	h.[OfficeNo],
	b.[BranchName],
	h.ConsignmentNo,
	h.[DateTime], 
	p.[DateTime] AS [PodDateTime],
	p.[OfficeNo] AS [PodOfficeNo],
	p.[OfficeName] AS [PodOfficeName]
FROM [vw_ALL_HANDOVER3RDPARTY] h 
LEFT JOIN [dbo].[vw_ALL_HANDOVER_POD] p ON p.[ConsignmentNo] = h.[ConsignmentNo]  
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = h.[OfficeNo]
WHERE h.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND h.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND h.[OfficeNo] = @branchCode

GO

