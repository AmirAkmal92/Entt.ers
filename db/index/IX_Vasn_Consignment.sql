USE Entt
GO

CREATE NONCLUSTERED INDEX IX_Vasn
ON [Entt].[Vasn] ([DateTime],ConsignmentNo,OfficeNo)
INCLUDE (ItemTypeCode, CourierId, Comment);
GO