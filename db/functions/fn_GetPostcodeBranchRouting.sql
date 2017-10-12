USE [Entt]
GO

/****** Object:  UserDefinedFunction [dbo].[fn_GetPostcodeBranchRouting]    Script Date: 10/12/2017 4:59:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [Entt].[fn_GetPostcodeBranchRouting](@postcode nvarchar(5))  
RETURNS TABLE  
AS  
RETURN 
    SELECT 
      r.[PostcodeStart]
      ,r.[PostcodeEnd]
      ,r.[Area]
      ,r.[BranchCode]
      ,r.[RouteCode]
      ,s.[BranchName]
      ,s.[StateName]
  FROM [Entt].[BranchPostcodeRange] r
  INNER JOIN [Entt].[BranchStates] s ON s.BranchCode = r.BranchCode
  WHERE CAST(@postcode AS int) BETWEEN [PostcodeStart] AND [PostcodeEnd] 
  

GO


