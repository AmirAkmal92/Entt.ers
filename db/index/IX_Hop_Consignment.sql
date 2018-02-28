USE Entt
GO

CREATE NONCLUSTERED INDEX IX_Hop
ON [Entt].[Hop] (DateTime,ConsignmentNo,OfficeNo)
INCLUDE (ItemTypeCode, CourierId, Comment);
GO