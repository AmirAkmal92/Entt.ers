USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_HANDOVER3RDPARTY] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_HANDOVER3RDPARTY]
AS

SELECT 
   [DateTime]
  ,[OfficeNo]
  ,[CourierId]
	,[CourierName]
  ,[ConsignmentNo]
  ,[StatusCode]
  ,[StatusCodeDesc]
  FROM [Entt].[StatusCode] 
  WHERE [StatusCode] IN ('23','24','41','42') AND [ItemTypeCode] = '01'

GO

