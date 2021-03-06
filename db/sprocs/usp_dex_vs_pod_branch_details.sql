USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_dex_vs_pod_branch_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_dex_vs_pod_branch_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(50)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
	bo.[StateName], 
	d.[OfficeNo], 
	d.[OfficeName], 
	d.[ConsignmentNo], 
	d.[DateTime], 
	d.[DeliveryCode], 
	p.[DateTime] AS [PODDateTime], 
	p.[OfficeNo] AS [PODOfficeNo], 
	p.[OfficeName] AS [PODOfficeName], 
	p.DeliveryCode AS [PODCode]
FROM  [dbo].[vw_ALL_DEX] d
LEFT JOIN [dbo].[vw_ALL_POD] p ON p.[ConsignmentNo] = d.[ConsignmentNo] --AND p.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND p.[DateTime] < DATEADD(d,31,DATEDIFF(d,7,@reportDate)) 
LEFT JOIN [Entt].BranchStates bo ON bo.BranchCode = d.OfficeNo 
WHERE d.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND d.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND d.[OfficeNo] = @branchCode

GO
