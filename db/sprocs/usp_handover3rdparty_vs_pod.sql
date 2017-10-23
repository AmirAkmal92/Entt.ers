USE [Entt]
GO
/****** Object:  StoredProcedure [dbo].[usp_handover3rdparty_vs_pod]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_handover3rdparty_vs_pod]
    @reportDate smalldatetime,
	  @prefixStart int,
	  @prefixEnd int
 AS   

SET NOCOUNT ON;   

SELECT 
  h.[StateName], 
  h.[BranchName], 
  h.[OfficeNo], 
  h.[WithPod], 
  h.[WithoutPod], 
  h.[Total] as [TotalHandover],
  p.[WithHandover], 
  p.[WithoutHandover], 
  p.[Total] as [TotalPod]
FROM Entt.fn_Handover3rdPartyVsPodByDate(@reportDate) h
LEFT JOIN Entt.fn_PodVsHandover3rdPartyByDatePrefix(@reportDate,@prefixStart,@prefixEnd) p on p.[OfficeNo] = h.[OfficeNo]
ORDER BY h.[OfficeNo]




GO

