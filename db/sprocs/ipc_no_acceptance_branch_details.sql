USE [Entt]
GO

/****** Object:  StoredProcedure [dbo].[ipc_no_acceptance_branch_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[ipc_no_acceptance_branch_details]
    @reportDate smalldatetime,
	@branchCode nvarchar(50)
AS   

SET NOCOUNT ON;   

SELECT 
  s.[ITEMNO] AS [ConsignmentNo]
  ,s.[PROCDT] AS [DateTime]
  ,a.[DateTime] AS [DateTimeOal]
  ,a.[ItemCategory]
  ,a.[CourierId]
  ,s.[BRANCHD4CODE] AS [LocationId]
  ,b.[BranchName] AS [LocationName]
  ,s.[RECPRSNZIPCD] AS [Postcode]
  ,b.[StateName]
FROM [Entt].[vw_IPC_SORTING_RESULT] s 
LEFT JOIN [Entt].[Acceptance] a ON a.[ConsignmentNo] = s.[ITEMNO] 
LEFT JOIN [Entt].BranchStates b ON b.[BranchCode] = s.[BRANCHD4CODE] 
WHERE s.[INITDT] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND s.[INITDT] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
--AND b.[BranchType] IN ('Pusat POS Laju','e_Commerce Processing Centre','POS Laju Warehouse') 
AND a.[DateTime] IS NULL 
AND s.[BRANCHD4CODE] = @branchCode 
ORDER BY s.[PROCDT]

GO
