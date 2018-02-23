USE [Entt]
GO

/****** Object:  View [dbo].[vw_LATEST_SIP] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_LATEST_SIP]
AS

SELECT 
	   [Id]
      ,[Version]
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
      ,[CreatedDate]
      ,[CreatedBy]
      ,[ChangedDate]
      ,[ChangedBy]
 FROM 
( 
SELECT 
	ROW_NUMBER() OVER(PARTITION BY [ConsignmentNo] ORDER BY [DateTime] DESC, [CreatedDate] DESC) AS Rank
	  ,[Id]
      ,[Version]
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
      ,[CreatedDate]
      ,[CreatedBy]
      ,[ChangedDate]
      ,[ChangedBy]
  FROM [Entt].[Sip] 
  ) v 
  WHERE v.Rank = 1

GO

