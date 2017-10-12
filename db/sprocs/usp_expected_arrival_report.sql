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
    a.[DateTime],
    a.[Postcode],     
	  r.[BranchCode],
	  r.[RouteCode],
	  r.[BranchName],
	  r.[StateName]
  FROM [Entt].[Acceptance] a 
  CROSS APPLY [Entt].fn_GetPostcodeBranchRouting(a.[Postcode]) r
  WHERE [Postcode] IS NOT NULL AND LEN([Postcode]) = 5 
  AND a.[DateTime] >= DATEADD(DAY,-1,@reportDate) AND a.[DateTime] < @reportDate 
  ORDER BY [BranchCode]
  
GO
