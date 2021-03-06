USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_po_acceptance_vs_reso_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_po_acceptance_vs_reso_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(10)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
  b.[StateName], b.[BranchCode], b.[BranchName], a.[ConsignmentNo],a.[DateTime]
  ,s.[DateTime] AS [PoDateTime]
	,s.[OfficeNo] AS [PoOfficeNo]
	,s.[OfficeName] AS [PoOfficeName]
FROM [Entt].[Entt].[Acceptance] a 
LEFT JOIN [Entt].[vw_LATEST_PENERIMAAN_PO] s ON s.[ConsignmentNo] = a.[ConsignmentNo] 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = a.[LocationId] 
WHERE a.[SystemId] = '012' 
AND a.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND a.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND a.[LocationId] = @branchCode
ORDER BY a.[DateTime] DESC

GO
