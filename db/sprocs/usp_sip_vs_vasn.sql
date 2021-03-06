USE [Entt]
GO

/****** Object:  StoredProcedure [dbo].[usp_sip_vs_vasn]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_sip_vs_vasn]
    @reportDate smalldatetime,
	  @day int
AS   

SET NOCOUNT ON;   

SELECT 
v.[StateName],
v.[BranchCode],
v.[BranchName],
COUNT(*) AS [Total],
	SUM(CASE WHEN [VasnDateTime] IS NOT NULL THEN 1 ELSE 0 END) AS [WithVasn],
	SUM(CASE WHEN [VasnDateTime] IS NULL THEN 1 ELSE 0 END) AS [WithoutVasn] 
FROM ( 
SELECT DISTINCT b.[StateName], b.[BranchCode], b.[BranchName], s.[ConsignmentNo], v.[DateTime] AS [VasnDateTime] FROM [dbo].[vw_DISTINCT_SIP] s 
LEFT JOIN [dbo].[vw_DISTINCT_VASN] v ON v.[ConsignmentNo] = s.[ConsignmentNo] AND v.[DateTime] > s.[DateTime] 
LEFT JOIN [Entt].BranchStates b ON b.BranchCode = s.OfficeNo 
WHERE s.[DateTime] >= DATEADD(d,0,DATEDIFF(d,0,@reportDate)) AND s.[DateTime] < DATEADD(d,1,DATEDIFF(d,0,@reportDate)) 
AND s.[ItemTypeCode] = '01'
) v 

GROUP BY v.StateName, v.BranchCode, v.BranchName 
ORDER BY v.BranchCode

GO
