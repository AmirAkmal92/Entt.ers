USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_sip_vs_vasn_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_sip_vs_vasn_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
  bo.[StateName], 
  s.[OfficeNo], 
  s.[OfficeName], 
  s.[ConsignmentNo], 
  s.[DateTime], 
  s.[Comment],
  v.[DateTime] AS [VasnDateTime],
  v.[OfficeNo] AS [VasnOfficeNo], 
  v.[OfficeName] AS [VasnOfficeName], 
  bo.[StateName] AS [VasnStateName], 
  v.[Comment] AS [VasnComment]	
FROM dbo.[vw_DISTINCT_SIP] s 
LEFT JOIN dbo.[vw_DISTINCT_VASN] v ON v.[ConsignmentNo] = s.[ConsignmentNo] AND v.[DateTime] > s.[DateTime] 
LEFT JOIN [Entt].BranchStates bo ON bo.BranchCode = s.OfficeNo 
WHERE s.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND s.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND s.[OfficeNo] = @branchCode AND s.[ItemTypeCode] = '01' 
ORDER BY s.[DateTime] DESC

GO
