USE [Entt]
GO

/****** Object:  UserDefinedFunction [Entt].[fn_GetSipVsVasnVsPod]    Script Date: 03/05/2018 4:59:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [Entt].[fn_GetSipVsVasnVsPod](@date date)  
RETURNS TABLE  
AS  
RETURN 
    
     SELECT 
      q.[OfficeNo], 
      COUNT(q.[ConsignmentNo]) AS [TotalSip],
      v.[TotalVasn], 
      p.[TotalPod],
      SUM(CASE WHEN q.[VasnDateTime] IS NOT NULL AND q.[PodDateTime] IS NOT NULL THEN 1 ELSE 0 END) SipWithVansWithPod,
      SUM(CASE WHEN q.[VasnDateTime] IS NOT NULL AND q.[PodDateTime] IS NULL THEN 1 ELSE 0 END) SipWithVansNoPod,
      SUM(CASE WHEN q.[VasnDateTime] IS NULL AND q.[PodDateTime] IS NULL THEN 1 ELSE 0 END) SipNoVansNoPod,
      SUM(CASE WHEN q.[VasnDateTime] IS NULL AND q.[PodDateTime] IS NOT NULL THEN 1 ELSE 0 END) SipNoVansWithPod  
    FROM (
      SELECT s.[OfficeNo], s.[ConsignmentNo], s.[DateTime] AS [SipDateTime], v.[DateTime] AS VasnDateTime, p.[DateTime] AS [PodDateTime] 
      FROM [dbo].[vw_DISTINCT_SIP] s 
      LEFT JOIN [dbo].[vw_DISTINCT_VASN] v ON v.[ConsignmentNo] = s.[ConsignmentNo] AND v.[OfficeNo] = s.[OfficeNo] 
      LEFT JOIN [dbo].[vw_ALL_POD] p ON p.[ConsignmentNo] = s.[ConsignmentNo]   
      WHERE s.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND s.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) 
    ) q 
    LEFT JOIN 
    (
      SELECT v.[OfficeNo], COUNT(*) AS [TotalVasn] FROM [dbo].[vw_DISTINCT_VASN] v
      WHERE v.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND v.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) 
      GROUP BY v.[OfficeNo]
    ) v ON v.OfficeNo = q.OfficeNo 
    LEFT JOIN 
    (
      SELECT p.[OfficeNo], COUNT(*) AS [TotalPod] FROM [dbo].[vw_ALL_POD] p
      WHERE p.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND p.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) 
      GROUP BY p.[OfficeNo] 
    ) p ON p.[OfficeNo] = q.[OfficeNo] 
    GROUP BY q.[OfficeNo], v.[TotalVasn], p.[TotalPod]
  
GO


