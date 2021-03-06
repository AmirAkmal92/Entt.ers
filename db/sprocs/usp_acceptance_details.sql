USE [Entt]
GO
/****** Object:  StoredProcedure [Entt].[usp_acceptance_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_acceptance_details]
    @reportDate datetime,
    @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT 
  b.[StateName],
  b.[BranchName],
  a.[ConsignmentNo], 
  a.[DateTime], 
  a.[SystemName], 
  a.[SystemId],
  a.[CourierId], 
  a.[LocationId],
  a.[Postcode], 
  a.[Weight], 
  a.[Pl9No], 
  a.[PickupNo], 
  a.[CreatedDate]
FROM [Entt].[Acceptance] a 
INNER JOIN [Entt].[BranchStates] b ON b.[BranchCode] = a.[LocationId]
WHERE 
  a.[LocationId] = @branchCode AND 
  a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND 
  a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate))
ORDER BY a.[DateTime]

  
GO
