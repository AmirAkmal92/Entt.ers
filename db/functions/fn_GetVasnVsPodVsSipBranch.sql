USE [Entt]
GO

/****** Object:  UserDefinedFunction [Entt].[fn_GetVasnVsPodVsSipBranch]    Script Date: 03/05/2018 4:59:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [Entt].[fn_GetVasnVsPodVsSipBranch](@date date,@branchCode nvarchar(10))  
RETURNS TABLE  
AS  
RETURN 
    
  SELECT 
    q.[OfficeNo], 
    COUNT(*) AS [TotalVasn], 
    SUM(CASE WHEN q.[Pod] IS NOT NULL AND q.[Sip] IS NOT NULL THEN 1 ELSE 0 END) VasnWithPodWithSip, 
    SUM(CASE WHEN q.[Pod] IS NOT NULL AND q.[Sip] IS NULL THEN 1 ELSE 0 END) VasnWithPodNoSip, 
    SUM(CASE WHEN q.[Pod] IS NULL AND q.[Sip] IS NULL THEN 1 ELSE 0 END) VasnNoPodNoSip 
  FROM 
  ( 
  SELECT DISTINCT  
    v.[OfficeNo] 
    ,v.[ConsignmentNo] 
    ,p.[ConsignmentNo] AS [Pod] 
    ,s.[ConsignmentNo] AS [Sip] 
	FROM [dbo].[vw_DISTINCT_VASN] v 
  LEFT JOIN [dbo].[vw_ALL_POD] p ON p.[ConsignmentNo] = v.[ConsignmentNo] AND  p.[OfficeNo] = v.[OfficeNo] 
  LEFT JOIN [dbo].[vw_DISTINCT_SIP] s ON s.[ConsignmentNo] = v.[ConsignmentNo] AND  v.[OfficeNo] = s.[OfficeNo]  
  WHERE v.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND v.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) 
  AND v.[OfficeNo] = @branchCode 
  ) q 
  GROUP BY q.OfficeNo 
  
GO


