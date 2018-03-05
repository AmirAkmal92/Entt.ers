USE [Entt]
GO

/****** Object:  UserDefinedFunction [Entt].[fn_GetPodVsSipVsVasn]    Script Date: 03/05/2018 4:59:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [Entt].[fn_GetPodVsSipVsVasn](@date date)  
RETURNS TABLE  
AS  
RETURN 
    
  SELECT 
    q.[OfficeNo],	
    SUM(CASE WHEN q.[Sip] IS NULL AND q.[Vasn] IS NULL THEN 1 ELSE 0 END) PodNoSipNoVasn 
  FROM 
  (
    SELECT DISTINCT p.[OfficeNo], p.[ConsignmentNo], s.[ConsignmentNo] AS [Sip], v.[ConsignmentNo] AS [Vasn] 
    FROM [dbo].[vw_ALL_POD] p 
    LEFT JOIN [dbo].[vw_DISTINCT_SIP] s ON s.[ConsignmentNo] = p.[ConsignmentNo] AND s.[OfficeNo] = p.[OfficeNo]  
    LEFT JOIN [dbo].[vw_DISTINCT_VASN] v ON v.[ConsignmentNo] = p.[ConsignmentNo] AND v.[OfficeNo] = p.[OfficeNo]
    WHERE p.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND p.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) 
  ) q 
  GROUP BY q.[OfficeNo] 
  
GO


