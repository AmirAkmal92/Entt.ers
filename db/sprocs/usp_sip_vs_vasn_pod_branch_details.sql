USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_sip_vs_vasn_pod_branch_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_sip_vs_vasn_pod_branch_details]
    @reportDate smalldatetime,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT 
  b.[StateName], 
  s.[OfficeNo], 
  b.[BranchName], 
  s.[ConsignmentNo], 
  s.[DateTime] AS [SipDateTime], 
  CASE WHEN bv.[BranchCode] IS NOT NULL THEN 1 ELSE 0 END IsVasn,
  CASE WHEN bp.[BranchCode] IS NOT NULL THEN 1 ELSE 0 END IsPod,
  bv.[BranchCode] AS [VasnBranchCode], 
  v.[DateTime] AS [VasnDateTime], 
  bp.[BranchCode] AS [PodBranchCode], 
  p.[DateTime] AS [PodDateTime] 
FROM [dbo].[vw_DISTINCT_SIP] s 
LEFT JOIN [dbo].[vw_DISTINCT_VASN] v ON v.[ConsignmentNo] = s.[ConsignmentNo] AND v.[OfficeNo] = s.[OfficeNo] 
LEFT JOIN [dbo].[vw_ALL_POD] p ON p.[ConsignmentNo] = s.[ConsignmentNo]  
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = s.OfficeNo 
LEFT JOIN [Entt].BranchStates bv ON bv.BranchCode = v.OfficeNo  
LEFT JOIN [Entt].BranchStates bp ON bp.BranchCode = p.OfficeNo  
WHERE s.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND s.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND s.[OfficeNo] = @branchCode 
ORDER BY s.[DateTime], s.[ConsignmentNo]

/*
SELECT v.[OfficeNo], COUNT(*) AS [TotalVasn] FROM [dbo].[vw_DISTINCT_VASN] v
      WHERE v.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND v.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
      AND v.[OfficeNo] = @branchCode 
      GROUP BY v.[OfficeNo]

SELECT p.[OfficeNo], COUNT(*) AS [TotalPod] FROM [dbo].[vw_ALL_POD] p
      WHERE p.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND p.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
      AND p.[OfficeNo] = @branchCode  
      GROUP BY p.[OfficeNo]       

*/      
GO
