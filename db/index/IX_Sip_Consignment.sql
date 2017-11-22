USE Entt;
GO

CREATE NONCLUSTERED INDEX IX_Sip
ON [Entt].[Sip] (DateTime,ConsignmentNo,OfficeNo)
INCLUDE (ItemTypeCode, CourierId, Comment);
GO