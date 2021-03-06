USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_po_acceptance_vs_reso]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_po_acceptance_vs_reso]
    @reportDate smalldatetime,
	  @day int
 AS   

SET NOCOUNT ON;   

SELECT 
v.[StateName],
v.[BranchCode],
v.[BranchName],
COUNT(*) AS [Total],
	SUM(CASE WHEN [PoDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithReso],
	SUM(CASE WHEN [PoDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutReso] 
FROM ( 
SELECT DISTINCT 
	b.[StateName], b.[BranchCode], b.[BranchName], a.[ConsignmentNo],a.[DateTime]
  ,a.[LocationId],a.[LocationName]
	,s.[DateTime] AS [PoDateTime]
	,s.[OfficeNo], b.[Order] 
FROM [Entt].[Entt].[Acceptance] a 
LEFT JOIN [Entt].[vw_LATEST_PENERIMAAN_PO] s ON s.[ConsignmentNo] = a.[ConsignmentNo] 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = a.[LocationId] 
WHERE a.[SystemId] = '012' 
AND a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
) v 

GROUP BY v.StateName, v.BranchCode, v.BranchName, v.[Order]
ORDER BY v.[Order], v.BranchCode

GO

