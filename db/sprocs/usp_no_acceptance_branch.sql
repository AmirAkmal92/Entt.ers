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
    u.[LocationId],
    b.[BranchName],
    b.[StateName],
    COUNT(*) AS [Total]
  FROM [Entt].[Unknown] u
  INNER JOIN [Entt].[BranchStates] b ON b.BranchCode = u.LocationId
  WHERE u.[LocationId] = @branchCode AND [Date] = @reportDate 
  GROUP BY u.[LocationId],b.[BranchName],b.[StateName]
  ORDER BY u.[LocationId]
  
GO
