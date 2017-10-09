USE [PittisNonCoreProd]
GO

/****** Object:  View [dbo].[uv_Aims_GetAssetRepairDetailByBranch]    Script Date: 09-Nov-16 9:57:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[uv_Aims_GetAssetRepairDetailByBranch]
AS

  SELECT [BranchId]
	  ,p.[BranchCode]
	  ,p.[BranchName]
	  ,[ReferenceNumber]
      ,[Requestor]
	  ,a.AssetNumber
	  ,a.AssetName
	  ,d.SerialNumber
	  ,c.CategoryName
      ,[DateSendDeviceToHQ]
      ,[DateSendDeviceToVendor]
      ,[DateHQReturnRepairedAssetToBranch]
      ,[ConsignmentNumber]
	  ,r.ReasonForRepair
      ,[Details]
	  ,s.AssetRepairStatus
      ,[AssetReplacementId]
      --,[AssetItemDetailsId]
      ,[IsReceivedByHq]
      ,[ReceivedByHq]
      ,[ReceivedDateByHq]
      ,[IsReceived]
      ,[ReceivedDate] 
FROM  dbo.[AIMSRequestAssetRepair] f
INNER JOIN dbo.[BROMBranchProfile] p ON p.Id = f.BranchId
INNER JOIN dbo.[AIMSAssetItemDetails] d ON d.Id = f.AssetItemDetailsId
INNER JOIN dbo.[AIMSAssetBatch] b ON b.Id = d.BatchId
INNER JOIN dbo.[AIMSAsset] a ON a.Id = b.AssetId
INNER JOIN dbo.[AIMSCategory] c ON c.Id = a.CategoryId
INNER JOIN dbo.[AIMSReasonForRepair] r ON r.Id = f.ReasonForRepairId
INNER JOIN dbo.[AIMSAssetRepairStatus] s ON s.Id = f.AssetRepairStatusId

GO
