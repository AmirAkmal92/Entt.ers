USE [Entt]
GO

/****** Object:  View [dbo].[vw_HIP_vs_SOP] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_HIP_vs_SOP]
AS

SELECT 
	hip.[DateTime], 
	hip.[ConsignmentNo], 
	hip.[OfficeNo], 
	hip.[OfficeName], 
	b.[StateName], 
	hip.[Comment] 
FROM [Entt].[Hip] hip 
LEFT JOIN [Entt].[Sop] sop ON sop.[ConsignmentNo] = hip.[ConsignmentNo] 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = hip.OfficeNo 
WHERE hip.[OfficeNo] = sop.[DestOfficeId] AND sop.[ItemTypeCode] = '01'

GO