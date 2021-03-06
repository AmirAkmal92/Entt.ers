USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_handover3rdparty_vs_pod]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_handover3rdparty_vs_pod]
    @reportDate smalldatetime,
	  @day int
 AS   

SET NOCOUNT ON;   

SELECT 
	q.[StateName],
	q.[OfficeNo],
	q.[BranchName],
	COUNT(*) AS [TotalHandover],
	SUM(CASE WHEN q.[PodDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithPod],
	SUM(CASE WHEN q.[PodDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutPod]
FROM (
SELECT DISTINCT 
	b.[StateName],
	h.[OfficeNo],
	b.[BranchName],
	h.ConsignmentNo,
	h.[DateTime], 
	p.[DateTime] AS [PodDateTime] 
FROM [vw_ALL_HANDOVER3RDPARTY] h 
LEFT JOIN [dbo].[vw_ALL_HANDOVER_POD] p ON p.[ConsignmentNo] = h.[ConsignmentNo]  
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = h.[OfficeNo]
WHERE h.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND h.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND b.[BranchType] = 'Pusat POS Laju' 
) q 

GROUP BY q.[OfficeNo], q.[StateName], q.[BranchName]
ORDER BY q.[OfficeNo]

GO

