USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_ACCEPTANCE] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_ACCEPTANCE]
AS

SELECT [ConsignmentNo],[DateTime],[CourierId],[LocationId],[Comment] 
FROM [Entt].[Acceptance] WHERE [ConsignmentNo] NOT LIKE 'CG%MY'
UNION 
SELECT 
  [ConsignmentNo],
  [DateTime],
  [CourierId],
  [OfficeNo],
  NULL AS [Comment]
  FROM [Entt].[StatusCode] 
  WHERE [StatusCode] IN ('27','49') AND [ItemTypeCode] = '01'

GO