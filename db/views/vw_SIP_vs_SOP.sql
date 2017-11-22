USE [Entt]
GO

/****** Object:  View [dbo].[vw_SIP_vs_SOP] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_SIP_vs_SOP]
AS

SELECT sip.[DateTime], sip.[ConsignmentNo], sip.[OfficeNo], sip.[OfficeName], b.[StateName], sip.[Comment] FROM [Entt].[Sip] sip 
LEFT JOIN [Entt].[Sop] sop ON sop.[ConsignmentNo] = sip.[ConsignmentNo] 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = sip.OfficeNo 
WHERE sip.[OfficeNo] = sop.[DestOfficeId] AND sop.[ItemTypeCode] = '01'

GO