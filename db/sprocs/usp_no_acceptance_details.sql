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
	v.[LocationId],
    v.[BranchName],
    v.[StateName],
    v.[ConsignmentNo],
    v.[DateTime],
    v.[EventType],
    v.[CourierId],
    v.[Status],
    v.[CreatedDate],
    v.[PupDateTime],    
    v.[PupStatus], 
    v.[AcceptanceLocation],
    v.[AcceptanceDateTime],
    v.[AcceptanceCreatedDate]

 FROM (
SELECT 
	ROW_NUMBER() OVER(PARTITION BY u.[ConsignmentNo] ORDER BY u.[Time] DESC) AS rownum,
    u.[LocationId],
    b.[BranchName],
    b.[StateName],
    u.[ConsignmentNo],
    CONVERT(datetime, SUBSTRING(u.[Date],5,4)+'-'+SUBSTRING(u.[Date],3,2)+'-'+SUBSTRING(u.[Date],1,2) +'T'+STUFF(STUFF(u.[Time], 5, 0, ':'), 3, 0, ':')) AS [DateTime],
    u.[EventType],
    u.[CourierId],
    u.[Status],
    u.[CreatedDate],
    u.[PupDateTime],    
    CASE WHEN a.[DateTime] IS NOT NULL THEN 0 ELSE 1 END AS [PupStatus], 
    a.[LocationId] AS [AcceptanceLocation],
    a.[DateTime] AS [AcceptanceDateTime],
    a.[CreatedDate] AS [AcceptanceCreatedDate]
  FROM [Entt].[Unknown] u
  INNER JOIN [Entt].[BranchStates] b ON b.BranchCode = u.LocationId
  LEFT JOIN [Entt].[Acceptance] a ON a.[ConsignmentNo] = u.[ConsignmentNo]
  WHERE [Date] = @reportDate AND u.[LocationId] = @branchCode --AND u.[Status] = '1' 
  ) v
  WHERE v.rownum = 1
  ORDER BY v.[DateTime]
  
GO
