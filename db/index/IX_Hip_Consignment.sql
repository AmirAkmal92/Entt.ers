USE Entt
GO

CREATE NONCLUSTERED INDEX IX_Hip
ON [Entt].[Hip] (DateTime,ConsignmentNo,OfficeNo)
INCLUDE (ItemTypeCode, CourierId, Comment);
GO