USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_search_nearest_branch]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_search_nearest_branch]
    @lat decimal(9,6),
    @lng decimal(9,6)
 AS   

SET NOCOUNT ON;   

SELECT b.*, ( 6371 * acos(cos(radians(@lat)) *
  cos(radians([Lattitude]) ) * cos( radians([Longitude]) - radians(@lng)) +
  sin(radians(@lat)) * sin(radians([Lattitude])))) AS Distance
FROM [Entt].[BranchProfiles] b
WHERE [BranchType] = '{B50372AD-E605-4729-872D-4D1C74F6FE6E}'
ORDER BY Distance ASC

GO

