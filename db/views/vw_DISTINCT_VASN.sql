USE [Entt]
GO

/****** Object:  View [dbo].[vw_DISTINCT_VASN] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_DISTINCT_VASN]
AS
  
SELECT DISTINCT 
      [DateTime]
      ,[ConsignmentNo]
      ,[OfficeNo]
      ,[OfficeName]
      ,[Comment]
      ,[CourierId]
      ,[ItemTypeCode]
      ,[VanItemTypeCode]
      ,[VanItemTypeDesc]
      ,[VanSenderName]
      ,[DataFlag]
  FROM [Entt].[Vasn] 
  
GO