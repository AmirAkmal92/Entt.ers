USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_EXCEPTION] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_EXCEPTION]
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
  WHERE [StatusCode] IN ('02','03','04','05','06','07','08','09','20','33','34','48') AND [ItemTypeCode] = '01'

GO

