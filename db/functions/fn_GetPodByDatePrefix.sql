USE [Entt]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [Entt].[fn_GetPodByDatePrefix](@date date, @day int)  
RETURNS TABLE  
AS  
RETURN 
    SELECT b.[StateName], b.[BranchName], [OfficeNo], [DateTime], [ConsignmentNo], [CourierId], [Comment]
    FROM vw_ALL_POD p
    LEFT JOIN [Entt].[BranchStates] b ON b.BranchCode = p.OfficeNo
    WHERE [DateTime] >= @date AND [DateTime] < DATEADD(DAY,@day,@date) 
 
 GO
