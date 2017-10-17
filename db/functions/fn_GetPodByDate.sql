USE [Entt]
GO
/****** Object:  UserDefinedFunction [Entt].[fn_GetPodByDate]    Script Date: 10/17/2017 4:58:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [Entt].[fn_GetPodByDate](@date date)  
RETURNS TABLE  
AS  
RETURN 
    SELECT b.[StateName],d.[OfficeNo],b.[BranchName],d.[DateTime], d.[ConsignmentNo], d.[CourierId], d.[Comment]	
	FROM [dbo].[vw_ALL_POD] d 
	INNER JOIN [Entt].[BranchStates] b ON d.[OfficeNo] = b.[BranchCode]
	WHERE d.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,(@date))) AND d.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,(@date)))
	
GO
