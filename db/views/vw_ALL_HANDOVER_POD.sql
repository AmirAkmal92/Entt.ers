USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_HANDOVER_POD] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_HANDOVER_POD]
AS

SELECT 
	[DateTime],
	[ConsignmentNo],
	[OfficeNo],
	[OfficeName],
	[CourierId],
	[DeliveryCode] AS [Code]
FROM  dbo.[vw_ALL_POD]
/*
UNION 
SELECT 
	[DateTime],
	[ConsignmentNo],
	[OfficeNo],
	[OfficeName],
	[CourierId],
	[StatusCode] AS [Code]
FROM dbo.[vw_ALL_POD_STATUSCODE] 
*/
GO

