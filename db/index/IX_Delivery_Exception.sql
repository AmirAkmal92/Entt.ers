USE [Entt]
GO

CREATE NONCLUSTERED INDEX [IX_Delivery_Exception]
ON [Entt].[Delivery] ([ItemTypeCode],[DateTime],[DeliveryCode])
INCLUDE ([OfficeNo],[OfficeName],[Comment],[CourierId],[CourierName],[ConsignmentNo])

GO
