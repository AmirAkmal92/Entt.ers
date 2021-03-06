USE [Entt]
GO

/****** Object:  StoredProcedure [dbo].[usp_io_exception_vs_package_branch]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_io_exception_vs_package_branch]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(50)
AS   

SET NOCOUNT ON;   

SELECT 
	b.[StateName],
	q.[OfficeNo],
	b.[BranchName],
	COUNT(*) AS [TotalEx],
	SUM(CASE WHEN q.[PatDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithPat],
	SUM(CASE WHEN q.[PatDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutPat] 
FROM 
(
SELECT e.[OfficeNo], e.[ConsignmentNo], p.[DateTime] AS [PatDateTime]  FROM dbo.[vw_ALL_EXCEPTION] e 
LEFT JOIN dbo.[vw_ALL_PACKAGEATSTATION] p ON p.[ConsignmentNo] = e.[ConsignmentNo] AND p.[OfficeNo] = e.[OfficeNo] 
WHERE e.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) 
AND e.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND e.[OfficeNo] = @branchCode 
) q 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = q.[OfficeNo] 
GROUP BY q.[OfficeNo], b.[StateName], b.[BranchName] 
ORDER BY q.[OfficeNo]

GO
