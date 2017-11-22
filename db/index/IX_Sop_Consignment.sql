USE Entt;
GO

CREATE NONCLUSTERED INDEX IX_Sop
ON [Entt].[Sop] (DateTime,ConsignmentNo,OfficeNo)
INCLUDE (ItemTypeCode, CourierId, Comment);
GO