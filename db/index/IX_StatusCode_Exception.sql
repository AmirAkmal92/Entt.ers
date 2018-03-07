USE Entt
GO

CREATE NONCLUSTERED INDEX [IX_StatusCode_Exception]
ON [Entt].[StatusCode] ([OfficeNo],[ItemTypeCode],[StatusCode])
INCLUDE ([DateTime],[CourierId],[CourierName],[ConsignmentNo],[StatusCodeDesc])

GO
