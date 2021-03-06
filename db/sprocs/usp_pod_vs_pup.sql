USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_pod_vs_pup]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_pod_vs_pup]
    @reportDate smalldatetime,
	  @day int
 AS   

SET NOCOUNT ON;   

SELECT 
v.[StateName],
v.[BranchCode],
v.[BranchName],
COUNT(*) AS [Total],
	SUM(CASE WHEN [PupDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithPup],
	SUM(CASE WHEN [PupDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutPup] 
FROM ( 
SELECT DISTINCT b.[StateName], b.[BranchCode], b.[BranchName], d.[ConsignmentNo], p.[DateTime] AS [PupDateTime] FROM [dbo].[vw_ALL_POD] d 
LEFT JOIN [dbo].[vw_ALL_PUP] p ON p.[ConsignmentNo] = d.[ConsignmentNo]  
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = d.OfficeNo 
WHERE d.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND d.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
) v 

GROUP BY v.StateName, v.BranchCode, v.BranchName 
ORDER BY v.BranchCode

GO
