USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_EXCEPTION] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_EXCEPTION]
AS

SELECT DISTINCT 
  [DateTime],
  [ConsignmentNo],
  [OfficeNo],
  [OfficeName],   
  [Comment],
  [CourierId],
  [CourierName],
  [DeliveryCode]
  FROM [Entt].[Delivery] d 
  WHERE [DeliveryCode] IN ('02','03','04','05','06','07') 
  AND [ItemTypeCode] = '01'

UNION 

SELECT DISTINCT 
  [DateTime],
  [ConsignmentNo],
  [OfficeNo],
  [OfficeName],   
  NULL AS [Comment],
  [CourierId],
  [CourierName],
  [StatusCode]
  FROM [Entt].[StatusCode] s 
  WHERE [StatusCode] IN ('12','20','34','43','44','45','46','48') 
  AND [ItemTypeCode] = '01'
  
GO

