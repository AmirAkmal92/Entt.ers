USE [Entt]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_GetDeliveryException]    Script Date: 10/12/2017 4:58:40 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetDeliveryException](@date date, @day int)  
RETURNS TABLE  
AS  
RETURN 
    SELECT v.[OfficeNo], v.[OfficeName], 
      SUM(CASE WHEN [PODDate] IS NOT NULL THEN 1 ELSE 0 END) [TotalWithPOD],
      SUM(CASE WHEN [PODDate] IS NULL THEN 1 ELSE 0 END) [TotalWithoutPOD],
      COUNT(*) AS [TotalDex]
    FROM (
      SELECT DISTINCT d.*, p.[DateTime] AS [PODDate], p.DeliveryCode AS [PODCode]
      FROM  [dbo].[vw_ALL_DEX] d
      LEFT JOIN [dbo].[vw_ALL_POD] p ON p.[ConsignmentNo] = d.[ConsignmentNo] 
      LEFT JOIN [Entt].[BranchStates] b ON b.[BranchCode] = d.[OfficeNo] 
      --WHERE d.[DateTime] >= @date AND  d.[DateTime] < DATEADD(day, @day, @date) 
      WHERE d.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND d.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) 
      AND b.[Display] = 1 
      --AND d.OfficeNo = '0500'
      --AND p.[DateTime] > '2017-10-01' AND p.[DateTime] <= '2017-10-15'
    ) v 
    GROUP BY v.OfficeNo, v.OfficeName
  

GO


