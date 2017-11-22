USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_sop_vs_sip_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_sop_vs_sip_details]
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
  i.[DateTime] AS [SipDateTime],
  i.[OfficeNo] AS [SipOfficeNo], 
  i.[OfficeName] AS [SipOfficeName], 
  i.[StateName] AS [SipStateName], 
  i.[Comment] AS [SipComment]	
FROM [Entt].[Sop] o 
LEFT JOIN [dbo].[vw_SIP_vs_SOP] i ON i.[ConsignmentNo] = o.[ConsignmentNo]
LEFT JOIN [Entt].BranchStates bo ON bo.BranchCode = o.OfficeNo 
WHERE o.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND o.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND o.[OfficeNo] = @branchCode AND o.[ItemTypeCode] = '01' 
ORDER BY o.[DateTime] DESC

GO

