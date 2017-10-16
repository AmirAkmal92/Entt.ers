USE [Entt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [Entt].[fn_GetPupVsPodByDatePrefix](@date date, @day int)  
RETURNS TABLE  
AS  
RETURN 
    SELECT 
			b.[StateName], a.[LocationId], b.[BranchName], a.[ConsignmentNo], a.[DateTime],
			p.[OfficeNo] AS [PODOfficeNo], p.[DateTime] AS [PODDateTime], p.[StateName] AS [PODStateName], p.[BranchName] AS [PODBranchName]
		FROM vw_ALL_ACCEPTANCE a
		LEFT JOIN [Entt].[fn_GetPodByDatePrefix](@date,@day) p ON p.ConsignmentNo = a.ConsignmentNo
		LEFT JOIN [Entt].[BranchStates] b ON b.BranchCode = a.LocationId
		WHERE a.[DateTime] >= @date AND a.[DateTime] < DATEADD(DAY,1,@date) 
		AND a.[LocationId] IS NOT NULL
 
 GO


