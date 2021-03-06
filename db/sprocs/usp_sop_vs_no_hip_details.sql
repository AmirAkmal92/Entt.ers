USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_sop_vs_no_hip_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_sop_vs_no_hip_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
  bo.[StateName], 
  o.[OfficeNo], 
  o.[OfficeName], 
  o.[ConsignmentNo], 
  o.[DateTime], 
  o.[Comment],
  h.[DateTime] AS [HipDateTime],
  h.[OfficeNo] AS [HipOfficeNo], 
  h.[OfficeName] AS [HipOfficeName], 
  bo.[StateName] AS [HipStateName], 
  h.[Comment] AS [HipComment]	
FROM [Entt].[Sop] o 
LEFT JOIN [dbo].[vw_HIP_vs_SOP] h ON h.[ConsignmentNo] = o.[ConsignmentNo] 
LEFT JOIN [Entt].BranchStates bo ON bo.BranchCode = o.OfficeNo 
WHERE o.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND o.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND o.[OfficeNo] = @branchCode AND o.[ItemTypeCode] = '01' AND h.[DateTime] IS NULL
ORDER BY o.[DateTime] DESC

GO

