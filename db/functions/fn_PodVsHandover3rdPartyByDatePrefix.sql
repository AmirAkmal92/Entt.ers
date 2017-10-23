USE [Entt]
GO
/****** Object:  UserDefinedFunction [Entt].[fn_PodVsHandover3rdPartyByDatePrefix]    Script Date: 10/17/2017 4:58:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [Entt].[fn_PodVsHandover3rdPartyByDatePrefix](@date date, @prefixStart int, @prefixEnd int)  
RETURNS TABLE  
AS  
RETURN 
  SELECT 
    p.[OfficeNo], 
    SUM(CASE WHEN h.[DateTime] IS NOT NULL THEN 1 ELSE 0 END) [WithHandover],
    SUM(CASE WHEN h.[DateTime] IS NULL THEN 1 ELSE 0 END) [WithoutHandover],
    COUNT(*) AS [Total] 
  FROM (
  SELECT [OfficeNo], [ConsignmentNo], [DateTime] FROM [vw_ALL_POD]
  UNION 
  SELECT [OfficeNo], [ConsignmentNo], [DateTime] FROM [vw_ALL_POD_STATUSCODE] ) p
  LEFT JOIN [dbo].[vw_ALL_HANDOVER3RDPARTY] h ON h.[ConsignmentNo] = p.[ConsignmentNo]
  WHERE p.[DateTime] >= DATEADD(d,0,DATEDIFF(d,@prefixStart,(@date))) AND p.[DateTime] < DATEADD(d,@prefixEnd,DATEDIFF(d,0,(@date)))
  AND h.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,(@date))) AND h.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,(@date)))
  GROUP BY  p.[OfficeNo]
	
GO
