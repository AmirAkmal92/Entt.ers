USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_POD Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_POD]
AS

SELECT [DateTime],[ConsignmentNo],[OfficeNo],[OfficeName],[CourierId],[DeliveryCode],[Comment] FROM [Entt].[Delivery] 
  WHERE [DeliveryCode] IN ('01','10','11','17','18')

GO

