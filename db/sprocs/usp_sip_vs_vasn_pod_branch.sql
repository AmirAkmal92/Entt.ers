USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_sip_vs_vasn_pod_branch]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_sip_vs_vasn_pod_branch]
    @reportDate smalldatetime,
	  @branchCode nvarchar(50)
 AS   

SET NOCOUNT ON;   

SELECT 
	b.[StateName],
	b.[BranchName],
	svp.*
FROM [Entt].[fn_GetSipVsVasnVsPodBranch](@reportDate,@branchCode) svp 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = svp.OfficeNo 
ORDER BY svp.[OfficeNo]

GO



