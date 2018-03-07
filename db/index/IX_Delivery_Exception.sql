USE [Entt]
GO

CREATE NONCLUSTERED INDEX [IX_Delivery_Exception]
ON [Entt].[Delivery] ([OfficeNo],[ItemTypeCode],[DateTime],[DeliveryCode])
INCLUDE ([OfficeName],[Comment],[CourierId],[CourierName],[ConsignmentNo])

GO
