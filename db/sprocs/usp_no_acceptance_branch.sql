USE [Entt]
GO
/****** Object:  StoredProcedure [Entt].[usp_no_acceptance_branch]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_no_acceptance_branch]
    @reportDate nvarchar(8),
    @branchCode nvarchar(50)
 AS   

SET NOCOUNT ON;   

SELECT 
	v.[LocationId],
	v.[BranchName],
    v.[StateName],
    COUNT(*) AS [Total] FROM (
SELECT 
	ROW_NUMBER() OVER(PARTITION BY u.[ConsignmentNo] ORDER BY u.[Time] DESC) AS rownum,
    u.[LocationId],
    b.[BranchName],
    b.[StateName]
  FROM [Entt].[Unknown] u
  INNER JOIN [Entt].[BranchStates] b ON b.BranchCode = u.LocationId
  LEFT JOIN [Entt].[Acceptance] a ON a.[ConsignmentNo] = u.[ConsignmentNo]
  WHERE [Date] = @reportDate AND u.[LocationId] = @branchCode --AND u.[Status] = '1'
  ) v
  WHERE v.rownum = 1
  GROUP BY v.[LocationId],v.[BranchName],v.[StateName]
  ORDER BY v.[LocationId]
  
GO
