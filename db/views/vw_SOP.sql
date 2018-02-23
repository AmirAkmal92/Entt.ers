USE [Entt]
GO

/****** Object:  View [dbo].[vw_SOP] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_SOP]
AS
  
SELECT DISTINCT [Version]
      ,[EventName]
      ,[Comment]
      ,[Channel]
      ,[DateTime]
      ,[OfficeNo]
      ,[OfficeName]
      ,[OfficeNextCode]
      ,[BeatNo]
      ,[ConsignmentNo]
      ,[CourierId]
      ,[CourierName]
      ,[ItemTypeCode]
      ,[DestOfficeId]
      ,[DestOfficeName]
      ,[BatchName]
      ,[DataFlag]
      ,[ScannerId]
  FROM [Entt].[Sop]
  
GO