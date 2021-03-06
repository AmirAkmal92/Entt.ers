USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_hip_vs_hop]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_hip_vs_hop]
    @reportDate smalldatetime,
	  @day int
 AS   

SET NOCOUNT ON;   

SELECT 
v.[StateName],
v.[BranchCode],
v.[BranchName],
COUNT(*) AS [Total],
	SUM(CASE WHEN [HopDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithHop],
	SUM(CASE WHEN [HopDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutHop] 
FROM ( 
SELECT DISTINCT b.[StateName], b.[BranchCode], b.[BranchName], i.[ConsignmentNo], o.[DateTime] AS [HopDateTime] FROM [Entt].[Hip] i 
LEFT JOIN [Entt].[Hop] o ON o.[ConsignmentNo] = i.[ConsignmentNo]  AND o.[OfficeNo] = i.[OfficeNo]  
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = i.OfficeNo 
WHERE i.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND i.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND i.[ItemTypeCode] = '01' 
) v 

GROUP BY v.StateName, v.BranchCode, v.BranchName 
ORDER BY v.BranchCode

GO

