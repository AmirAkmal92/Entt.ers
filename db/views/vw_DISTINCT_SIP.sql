USE [Entt]
GO

/****** Object:  View [dbo].[vw_DISTINCT_SIP] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_DISTINCT_SIP]
AS
  
SELECT DISTINCT 
       [DateTime]
      ,[ConsignmentNo]
      ,[OfficeNo]
      ,[OfficeName]
      ,[Comment]
      ,[BeatNo]
      ,[CourierId]
      ,[CourierName]
      ,[ItemTypeCode]
      ,[DataFlag]
      ,[ScannerId]
  FROM [Entt].[Sip] 
  
GO