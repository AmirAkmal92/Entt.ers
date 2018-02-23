USE [Entt]
GO

/****** Object:  View [dbo].[vw_HIP] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_HIP]
AS

SELECT DISTINCT [Version]
      ,[EventName]
      ,[Channel]
      ,[Comment]
      ,[DateTime]
      ,[OfficeNo]
      ,[OfficeName]
      ,[OfficeNextCode]
      ,[BeatNo]
      ,[CourierId]
      ,[CourierName]
      ,[ItemTypeCode]
      ,[ConsignmentNo]
      ,[BatchName]
      ,[DataFlag]
      ,[ScannerId]
  FROM [Entt].[Hip]
  
GO