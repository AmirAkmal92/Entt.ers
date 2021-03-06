USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_pup_vs_sop_branch]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_pup_vs_sop_branch]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(50)
 AS   

SET NOCOUNT ON;   

SELECT 
v.[StateName],
v.[BranchCode],
v.[BranchName],
COUNT(*) AS [Total],
	SUM(CASE WHEN [SopDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithSop],
	SUM(CASE WHEN [SopDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutSop] 
FROM ( 
	SELECT DISTINCT 
		b.[StateName], 
		b.[BranchCode], 
		b.[BranchName],
		p.[DateTime], 
		p.[ConsignmentNo], 
		s.[DateTime] AS [SopDateTime] 
	FROM dbo.[vw_ALL_PUP] p 
LEFT JOIN [Entt].[Sop] s ON s.[ConsignmentNo] = p.[ConsignmentNo] 
LEFT JOIN [Entt].[BranchStates] b ON b.BranchCode = p.[LocationId]
WHERE p.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND p.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) AND p.[LocationId] = @branchCode 
) v 

GROUP BY v.StateName, v.BranchCode, v.BranchName 
ORDER BY v.BranchCode

GO

