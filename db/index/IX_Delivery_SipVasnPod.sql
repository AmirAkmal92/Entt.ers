USE [Entt]
GO

CREATE NONCLUSTERED INDEX [IX_Delivery_SipVasnPod]
ON [Entt].[Delivery] ([ItemTypeCode],[ConsignmentNo],[DeliveryCode])
INCLUDE ([DateTime],[OfficeNo],[OfficeName],[Comment],[CourierId])
GO
