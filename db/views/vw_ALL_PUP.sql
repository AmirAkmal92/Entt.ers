USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_PUP] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_PUP]
AS

SELECT [ConsignmentNo],[DateTime],[CourierId],[LocationId],[LocationName],[Comment] 
FROM [Entt].[Acceptance] WHERE [ConsignmentNo] NOT LIKE 'CG%MY'
UNION ALL 
SELECT 
  [ConsignmentNo],
  [DateTime],
  [CourierId],
  [OfficeNo],  
  [OfficeName],
  NULL AS [Comment]
  FROM [Entt].[StatusCode] 
  WHERE [StatusCode] IN ('11','12','21','22','26','27','28','29','30','31','38','43','45','47','49','55','56') AND [ItemTypeCode] = '01'

GO