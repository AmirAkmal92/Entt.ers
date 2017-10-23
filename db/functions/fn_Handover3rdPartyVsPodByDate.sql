USE [Entt]
GO
/****** Object:  UserDefinedFunction [Entt].[fn_Handover3rdPartyVsPodByDate]    Script Date: 10/17/2017 4:58:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [Entt].[fn_Handover3rdPartyVsPodByDate](@date date)  
RETURNS TABLE  
AS  
RETURN 
  SELECT 
    b.[StateName],
    b.[BranchName],
    h.[OfficeNo],
    SUM(CASE WHEN d.[DateTime] IS NOT NULL THEN 1 ELSE 0 END) [WithPOD],
    SUM(CASE WHEN d.[DateTime] IS NULL THEN 1 ELSE 0 END) [WithoutPOD],
    COUNT(*) AS [Total]
  FROM [dbo].[vw_ALL_HANDOVER3RDPARTY] h
  LEFT JOIN (
  SELECT [OfficeNo], [ConsignmentNo], [DateTime] FROM [vw_ALL_POD]
  UNION 
  SELECT [OfficeNo], [ConsignmentNo], [DateTime] FROM [vw_ALL_POD_STATUSCODE] 
  ) d ON d.[ConsignmentNo] = h.[ConsignmentNo]
  INNER JOIN [Entt].[BranchStates] b ON b.BranchCode = h.OfficeNo
  WHERE h.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,(@date))) AND h.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,(@date)))
  GROUP BY b.[StateName], b.[BranchName], h.[OfficeNo]

  --SELECT DATEADD(d,7,DATEDIFF(d,0,('2017-09-06')))
	
GO
