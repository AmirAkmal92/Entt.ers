USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_DEX] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_DEX]
AS

SELECT [DateTime],[ConsignmentNo],[OfficeNo],[OfficeName],[DeliveryCode] FROM [Entt].[Delivery] 
  WHERE [DeliveryCode] IN ('02','03','04','05','06','07','08','09','12','13','20','25','34','43','44','45','46','48')

GO

