USE [Entt]
GO

/****** Object:  StoredProcedure [dbo].[usp_vasn_vs_pod]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_vasn_vs_pod]
    @reportDate smalldatetime,
	  @day int
AS   

SET NOCOUNT ON;   

SELECT 
q.[StateName],
q.[BranchCode],
q.[BranchName],
COUNT(*) AS [Total],
	SUM(CASE WHEN [PodDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithPod],
	SUM(CASE WHEN [PodDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutPod] 
FROM ( 
SELECT DISTINCT b.[StateName], b.[BranchCode], b.[BranchName], v.[ConsignmentNo], p.[DateTime] AS [PodDateTime] FROM [dbo].[vw_DISTINCT_VASN] v 
LEFT JOIN [dbo].[vw_ALL_POD] p ON p.[ConsignmentNo] = v.[ConsignmentNo]  
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = v.OfficeNo 
WHERE v.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND v.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND v.[ItemTypeCode] = '01'
) q 

GROUP BY q.StateName, q.BranchCode, q.BranchName 
ORDER BY q.BranchCode

GO
