USE [Entt]
GO
/****** Object:  UserDefinedFunction [Entt].[fn_GetPupByDate]    Script Date: 10/17/2017 4:58:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [Entt].[fn_GetPupByDate](@date date)  
RETURNS TABLE  
AS  
RETURN 
  SELECT b.[StateName],a.[LocationId],b.[BranchName],a.[DateTime], a.[ConsignmentNo], a.[CourierId], a.[Comment]	
	FROM [dbo].[vw_ALL_ACCEPTANCE] a 
	INNER JOIN [Entt].[BranchStates] b ON a.[LocationId] = b.[BranchCode]
	WHERE a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,(@date))) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,(@date)))
	
GO
