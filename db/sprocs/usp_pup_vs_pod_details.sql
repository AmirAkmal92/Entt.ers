USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_pup_vs_pod_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_pup_vs_pod_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT
  --ROW_NUMBER()  OVER(ORDER BY p.[DateTime]) AS [Row],
  p.[StateName], 
  p.[LocationId] AS [OfficeNo], 
  p.[BranchName], 
  p.[ConsignmentNo], 
  p.[DateTime], 
  p.[Comment], 
  p.[PODOfficeNo], 
  p.[PODDateTime], 
  p.[PODBranchName], 
  p.[PODStateName], 
  p.[PODComment]	
FROM [Entt].[fn_GetPupVsPodByDatePrefix](@reportDate,@day) p 
WHERE p.[LocationId] = @branchCode AND p.[DateTime] >= @reportDate AND p.[DateTime] < DATEADD(DAY,1,@reportDate)
ORDER BY p.[DateTime]

GO

