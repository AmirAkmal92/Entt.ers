USE [Entt]
GO
/****** Object:  StoredProcedure [Entt].[usp_no_acceptance]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_no_acceptance]
    @reportDate nvarchar(8)
 AS   

SET NOCOUNT ON;   

SELECT 
    u.[LocationId],
    b.[BranchName],
    b.[StateName],
    COUNT(*) AS [Total]
  FROM [Entt].[Unknown] u
  INNER JOIN [Entt].[BranchStates] b ON b.BranchCode = u.LocationId
  WHERE [Date] = @reportDate 
  GROUP BY u.[LocationId],b.[BranchName],b.[StateName]
  ORDER BY u.[LocationId]
  
GO
