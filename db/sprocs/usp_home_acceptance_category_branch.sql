USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_home_acceptance_category_branch]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_home_acceptance_category_branch]
    @date smalldatetime,
    @branchCode nvarchar(50)
 AS   

SET NOCOUNT ON;   

SELECT 
  COALESCE([ItemCategoryDescription], 'Uncategorized') AS [ItemCategory],
	COUNT(*) AS [Total]
FROM [Entt].[Acceptance]
WHERE [DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@date)) AND [DateTime] < DATEADD(d,1,DATEDIFF(d,0,@date)) AND [LocationId] = @branchCode
GROUP BY [ItemCategoryDescription]

GO

