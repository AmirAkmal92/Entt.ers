USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_home_dashboard]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_home_dashboard]
    @date smalldatetime
 AS   

SET NOCOUNT ON;   

 SELECT  (
        SELECT COUNT(*) FROM [Entt].[Acceptance] WHERE 
  [DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND [DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date))
        ) AS [Acceptance],
        (
        SELECT COUNT(*) FROM [Entt].[Delivery] WHERE 
  [DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND [DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date))
        ) AS [Delivery],
		(
		SELECT COUNT(*) FROM (
SELECT ROW_NUMBER() OVER(PARTITION BY [ConsignmentNo] ORDER BY [Time] DESC) AS [RowNum], [ConsignmentNo] FROM [Entt].[Unknown] WHERE 
  [CreatedDate] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND [CreatedDate] < DATEADD(d,1,DATEDIFF(d,0,@date)) AND [Status] = '1' 
  ) v WHERE v.[RowNum] = 1 
        ) AS [Unknown]

GO

