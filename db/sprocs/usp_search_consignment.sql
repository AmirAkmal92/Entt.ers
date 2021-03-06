USE [Entt]
GO
/****** Object:  StoredProcedure [Entt].[usp_search_consignment]    Script Date: 12/14/2016 10:47:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [Entt].[usp_search_consignment]
	  @connote nvarchar(50)
 AS   

SET NOCOUNT ON;   

SELECT TOP 1 [ConsignmentNo]
      ,[DateTime]
      ,[PickupNo]
      ,[TotalConsignment]
      ,[Postcode]
      ,[ParentWeight]
      ,[TotalItem]
      ,[ProductType]
      ,[ProductTypeDescription]
      ,[PackageType]
      ,[PackageTypeDescription]
      ,[Country]
      ,[Height]
      ,[Width]
      ,[Length]
      ,[Weight]
      ,[ItemCategory]
      ,[ItemCategoryDescription]
      ,[TotalBaby]
      ,[TotalParent]
      ,[RoutingCode]
      ,[TotalWeight]
      ,[TotalDimWeight]
      ,[Price]
      ,[ConsignmentFee]
      ,[CourierId]
      ,[CourierName]
      ,[LocationId]
      ,[LocationName]
      ,[BeatNo]
      ,[SystemId]
      ,[SystemName]
      ,[WeightDensity]
      ,[WeightVolumetric]
      ,[ConsigneeAddressPostcode]
      ,[ConsigneeAddressCountry]
      ,[ShipperAccountNo]
      ,[ShipperAddressPostcode]
      ,[ShipperAddressCountry]
      ,[IposReceiptNo]
      ,[FailPickupReason]
      ,[Comment]
      ,[DropCode]
      ,[LatePickup]
      ,[Pl9No]
      ,[PickupDateTime]
      ,[ClerkId]
      ,[DropOption]
      ,[ScannerId]  
      ,[CreatedDate]
  FROM [Entt].[Entt].[Acceptance] 
  WHERE ConsignmentNo = @connote 
  ORDER BY [Version] DESC, [DateTime] DESC

GO

