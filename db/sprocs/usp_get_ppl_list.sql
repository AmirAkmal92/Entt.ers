USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_get_ppl_list]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_get_ppl_list]

 AS   

SET NOCOUNT ON;   

  SELECT 
    [BranchCode],
    [BranchName],
    [BranchType],
    [StateName]
  FROM [Entt].[BranchStates]
  WHERE [BranchType] = 'Pusat POS Laju'
  ORDER BY [Order],[BranchCode]

GO

