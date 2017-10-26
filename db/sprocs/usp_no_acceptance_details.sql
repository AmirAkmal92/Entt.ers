USE [Entt]
GO
/****** Object:  StoredProcedure [Entt].[usp_no_acceptance_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_no_acceptance_details]
    @reportDate nvarchar(8),
    @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

  SELECT 
    u.[LocationId],
    b.[BranchName],
    b.[StateName],
    u.[ConsignmentNo],
    CONVERT(datetime, SUBSTRING(u.[Date],5,4)+'-'+SUBSTRING(u.[Date],3,2)+'-'+SUBSTRING(u.[Date],1,2) +'T'+STUFF(STUFF(u.[Time], 5, 0, ':'), 3, 0, ':')) AS [DateTime],
    u.[EventType],
    u.[CourierId],
    u.[Status],
    u.[CreatedDate],
    u.[PupDateTime]
  FROM [Entt].[Unknown] u
  INNER JOIN [Entt].[BranchStates] b ON b.BranchCode = u.LocationId
  WHERE [Date] = @reportDate AND [LocationId] = branchCode
  ORDER BY u.[Time]
  
GO
