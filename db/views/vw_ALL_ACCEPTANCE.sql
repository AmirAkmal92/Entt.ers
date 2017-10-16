USE [Entt]
GO

/****** Object:  View [dbo].[vw_ALL_ACCEPTANCE] Script Date: 10/10/2017 9:20:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_ALL_ACCEPTANCE]
AS

SELECT [ConsignmentNo],[DateTime],[CourierId],[LocationId],[Comment] 
FROM [Entt].[Acceptance]


GO