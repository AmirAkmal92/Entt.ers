USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_dex_vs_pod]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[usp_dex_vs_pod]
    @reportDate smalldatetime,
	  @day int
 AS   

SET NOCOUNT ON;   

SELECT 
  s.StateName,
  e.OfficeNo, 
  e.OfficeName, 
  e.TotalWithPOD, 
  e.TotalWithoutPOD, 
  e.TotalDex
FROM dbo.fn_GetDeliveryException(@reportDate,@day) e
INNER JOIN [Entt].[BranchStates] s ON s.BranchCode = e.OfficeNo
ORDER BY e.OfficeNo

GO
