USE [Entt]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_GetDeliveryPod]    Script Date: 10/12/2017 4:59:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [dbo].[fn_GetDeliveryPod](@date date, @day int)  
RETURNS TABLE  
AS  
RETURN 
    SELECT 
      v.[OfficeNo], 
      v.[OfficeName], 
      SUM(CASE WHEN [DexDate] IS NOT NULL THEN 1 ELSE 0 END) [TotalWithDex],
      SUM(CASE WHEN [DexDate] IS NULL THEN 1 ELSE 0 END) [TotalWithoutDex],
      COUNT(*) AS [TotalPod]
    FROM (
      SELECT DISTINCT p.*, d.[DateTime] AS [DexDate], d.DeliveryCode AS [DexCode]
      FROM  [dbo].[vw_ALL_POD] p
      LEFT JOIN [dbo].[vw_ALL_DEX] d ON d.[ConsignmentNo] = p.[ConsignmentNo]
      WHERE p.[DateTime] >= @date AND  p.[DateTime] < DATEADD(day, @day, @date)
      --AND d.[DateTime] > '2017-10-01' AND d.[DateTime] <= '2017-10-08'
    ) v 
   GROUP BY v.OfficeNo, v.OfficeName
  

GO


