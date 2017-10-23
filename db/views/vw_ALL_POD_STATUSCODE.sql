USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_POD_STATUSCODE] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_POD_STATUSCODE]
AS

  SELECT [DateTime],[ConsignmentNo],[OfficeNo],[OfficeName],[CourierId],[StatusCode] 
  FROM [Entt].[StatusCode]
  WHERE [StatusCode] IN ('17','18')

GO

