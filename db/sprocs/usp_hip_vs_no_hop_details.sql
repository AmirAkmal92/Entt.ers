USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_hip_vs_no_hop_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_hip_vs_no_hop_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
  bo.[StateName], 
  i.[OfficeNo], 
  i.[OfficeName], 
  i.[ConsignmentNo], 
  i.[DateTime], 
  i.[Comment],
  o.[DateTime] AS [HopDateTime],
  o.[OfficeNo] AS [HopOfficeNo], 
  o.[OfficeName] AS [HopOfficeName], 
  bo.[StateName] AS [HopStateName], 
  o.[Comment] AS [HopComment]	
FROM [Entt].[Hip] i 
LEFT JOIN [Entt].[Hop] o ON o.[ConsignmentNo] = i.[ConsignmentNo] AND o.[OfficeNo] = i.[OfficeNo]   
LEFT JOIN [Entt].BranchStates bo ON bo.BranchCode = i.OfficeNo 
WHERE i.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND i.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND i.[OfficeNo] = @branchCode AND i.[ItemTypeCode] = '01' AND o.[DateTime] IS NULL 
ORDER BY i.[DateTime] DESC

GO

