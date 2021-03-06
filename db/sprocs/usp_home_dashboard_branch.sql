USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_home_dashboard_branch]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_home_dashboard_branch]
    @date smalldatetime,
    @branchCode nvarchar(50)
 AS   

SET NOCOUNT ON;   

SELECT v.[Acceptance], v.[Delivery], v.[Unknown], v.[ExpectedDeliveries], b.[BranchName] FROM (

 SELECT  (
        SELECT COUNT(*) FROM [Entt].[Acceptance] WHERE 
  [DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) 
  AND [DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) AND [LocationId] = @branchCode
        ) AS [Acceptance],
        (
        SELECT COUNT(*) FROM [Entt].[Delivery] WHERE 
  [DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) 
  AND [DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) AND [OfficeNo] = @branchCode
        ) AS [Delivery],
		(
		SELECT COUNT(*) FROM (
SELECT ROW_NUMBER() OVER(PARTITION BY [ConsignmentNo] ORDER BY [Time] DESC) AS [RowNum], [ConsignmentNo] FROM [Entt].[Unknown] WHERE 
  [CreatedDate] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND [CreatedDate] < DATEADD(d,1,DATEDIFF(d,0,@date)) 
  AND [LocationId] = @branchCode AND [Status] = '1' 
  ) v WHERE v.[RowNum] = 1 
        ) AS [Unknown],
		(
		SELECT COUNT(*) FROM [Entt].[Acceptance] a 
  CROSS APPLY [Entt].fn_GetPostcodeBranchRouting(a.[Postcode]) r
  WHERE [Postcode] LIKE '%[0-9]'
  AND a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date))  
  AND r.[BranchCode] = @branchCode
		) AS [ExpectedDeliveries]  ) v 
LEFT JOIN [Entt].[BranchStates] b ON b.[BranchCode] = @branchCode

GO

