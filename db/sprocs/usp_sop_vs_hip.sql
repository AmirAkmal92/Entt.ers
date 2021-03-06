USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_sop_vs_hip]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_sop_vs_hip]
    @reportDate smalldatetime,
	  @day int
 AS   

SET NOCOUNT ON;   

SELECT 
v.[StateName],
v.[BranchCode],
v.[BranchName],
COUNT(*) AS [Total],
	SUM(CASE WHEN [SipDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithSip],
	SUM(CASE WHEN [SipDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutSip] 
FROM ( 
SELECT DISTINCT b.[StateName], b.[BranchCode], b.[BranchName], o.[ConsignmentNo], i.[DateTime] AS [SipDateTime] FROM [Entt].[Sop] o 
LEFT JOIN [dbo].[vw_HIP_vs_SOP] i ON i.[ConsignmentNo] = o.[ConsignmentNo] 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = o.OfficeNo 
WHERE o.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND o.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND o.[ItemTypeCode] = '01' 
) v 

GROUP BY v.StateName, v.BranchCode, v.BranchName 
ORDER BY v.BranchCode


GO

