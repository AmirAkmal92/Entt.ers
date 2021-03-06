USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_pup_vs_sop_details]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_pup_vs_sop_details]
    @reportDate smalldatetime,
	  @day int,
	  @branchCode nvarchar(50)
 AS   

SET NOCOUNT ON;   

SELECT DISTINCT 
		b.[StateName], 
		b.[BranchCode], 
		b.[BranchName],
		p.[DateTime], 
		p.[ConsignmentNo], 
		p.[Comment],
		s.[DateTime] AS [SopDateTime], 
		bs.[StateName] AS [SopStateName],
		s.[OfficeNo] AS [SopOfficeNo],
		s.[OfficeName] AS [SopOfficeName],
		s.[Comment] AS [SopComment] 
	FROM dbo.[vw_ALL_PUP] p 
LEFT JOIN [Entt].[Sop] s ON s.[ConsignmentNo] = p.[ConsignmentNo] 
LEFT JOIN [Entt].[BranchStates] b ON b.BranchCode = p.[LocationId] 
LEFT JOIN [Entt].[BranchStates] bs ON bs.BranchCode = s.[OfficeNo] 
WHERE p.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND p.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND p.[LocationId] = @branchCode 
ORDER BY p.[DateTime]

GO

