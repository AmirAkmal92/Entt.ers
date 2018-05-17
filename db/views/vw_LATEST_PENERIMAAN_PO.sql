USE [Entt]
GO

/****** Object:  View [Entt].[vw_LATEST_PENERIMAAN_PO] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [Entt].[vw_LATEST_PENERIMAAN_PO]
AS

SELECT 
  [DateTime],
	[ConsignmentNo],
	[OfficeNo],
	[OfficeName],
	[CourierId],
	[StatusCode] 
	 FROM (
SELECT
	ROW_NUMBER() OVER(PARTITION BY [ConsignmentNo] ORDER BY [DateTime] DESC) AS [Rank],  
	[DateTime],
	[ConsignmentNo],
	[OfficeNo],
	[OfficeName],
	[CourierId],
	[StatusCode] 
  FROM [Entt].[StatusCode]
  WHERE [StatusCode] = '21' 
  --AND [DateTime] >= DATEADD(d,0,DATEDIFF(d,0,'2018-05-06')) AND [DateTime] < DATEADD(d,1,DATEDIFF(d,0,'2018-05-06'))
  ) v WHERE v.Rank = 1
  
GO

