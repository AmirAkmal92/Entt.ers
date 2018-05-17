USE Entt
GO

CREATE NONCLUSTERED INDEX [IX_StatusCode_PenerimaanPo]
ON [Entt].[StatusCode] ([StatusCode])
INCLUDE ([DateTime],[OfficeNo],[ConsignmentNo])

GO