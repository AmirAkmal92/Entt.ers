USE Entt
GO

CREATE NONCLUSTERED INDEX [IX_StatusCode_StatusCode]
ON [Entt].[StatusCode] ([ItemTypeCode],[DateTime],[StatusCode])
INCLUDE ([OfficeNo],[OfficeName],[CourierId],[CourierName],[ConsignmentNo])

GO