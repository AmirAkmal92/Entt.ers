USE Entt;
GO

CREATE NONCLUSTERED INDEX IX_StatusCode_StatusCode
ON [Entt].[StatusCode] (StatusCode,ItemTypeCode)
INCLUDE (ConsignmentNo, DateTime, CourierId, OfficeNo);
GO