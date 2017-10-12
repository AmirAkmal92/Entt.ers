USE [Entt]
GO
/****** Object:  StoredProcedure [Entt].[usp_no_acceptance]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_no_acceptance]
    @reportDate smalldatetime
 AS   

SET NOCOUNT ON;   

SELECT 
    u.[LocationId],
    u.[Date],
    u.[ConsignmentNo],
    b.[BranchName],
	  b.[StateName]
  FROM [Entt].[Unknown] u
  INNER JOIN [Entt].[BranchStates] b ON b.BranchCode = u.LocationId
  WHERE [Date] >= @reportDate AND [Date] < DATEADD(DAY,1,@reportDate)
  ORDER BY u.[LocationId]
  
GO
