USE [Entt]
GO

CREATE NONCLUSTERED INDEX IX_Delivery_AllPod
ON [Entt].[Delivery] ([OfficeNo],[ItemTypeCode],[DateTime],[DeliveryCode])
INCLUDE ([Comment],[ConsignmentNo])
GO