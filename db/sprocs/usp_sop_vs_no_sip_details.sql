USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_sop_vs_no_sip_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_sop_vs_no_sip_details]
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
  s.[DateTime] AS [SipDateTime],
  s.[OfficeNo] AS [SipOfficeNo], 
  s.[OfficeName] AS [SipOfficeName], 
  bo.[StateName] AS [SipStateName], 
  s.[Comment] AS [SipComment]	
FROM [Entt].[Sop] o 
LEFT JOIN [Entt].[Sip] s ON s.[ConsignmentNo] = o.[ConsignmentNo] 
LEFT JOIN [Entt].BranchStates bo ON bo.BranchCode = o.OfficeNo 
WHERE o.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND o.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND o.[OfficeNo] = @branchCode AND o.[ItemTypeCode] = '01' AND s.[DateTime] IS NULL 
ORDER BY o.[DateTime] DESC

GO

