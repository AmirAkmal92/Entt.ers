USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_expected_arrival_report]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_expected_arrival_report]
    @reportDate smalldatetime
 AS   

SET NOCOUNT ON;   

SELECT 
	  r.[BranchCode],
	  r.[RouteCode],
	  r.[BranchName],
	  r.[StateName],
	  COUNT(*) AS [Total]
  FROM [Entt].[Acceptance] a 
  CROSS APPLY [Entt].fn_GetPostcodeBranchRouting(a.[Postcode]) r
  WHERE [Postcode] LIKE '%[0-9]'
  AND a.[DateTime] >= DATEADD(DAY,-1,@reportDate) AND a.[DateTime] < @reportDate 
  GROUP BY r.[BranchCode],r.[RouteCode],r.[BranchName],r.[StateName]
  ORDER BY [BranchCode]
  
GO
