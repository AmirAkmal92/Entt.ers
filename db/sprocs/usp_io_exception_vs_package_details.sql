USE [Entt]
GO

/****** Object:  StoredProcedure [dbo].[usp_io_exception_vs_package_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_io_exception_vs_package_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(50)
AS   

SET NOCOUNT ON;   

SELECT 
  b.[StateName], 
  e.[OfficeNo], 
  b.[BranchName] AS [OfficeName], 
  e.[ConsignmentNo], 
  e.[DateTime],
  e.[DeliveryCode], 
  e.[Comment], 
  e.[CourierId], 
  p.[DateTime] AS [PasDateTime]  
FROM dbo.[vw_ALL_EXCEPTION] e 
LEFT JOIN dbo.[vw_ALL_PACKAGEATSTATION] p ON p.[ConsignmentNo] = e.[ConsignmentNo] AND p.[OfficeNo] = e.[OfficeNo] 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = e.[OfficeNo] 
WHERE e.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) 
AND e.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND e.[OfficeNo] = @branchCode 
ORDER BY e.[DateTime]

GO
