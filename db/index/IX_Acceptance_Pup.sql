USE Entt
GO

CREATE NONCLUSTERED INDEX IX_Acceptance_Pup
ON [Entt].[Acceptance] (ConsignmentNo)
INCLUDE (DateTime, CourierId, LocationId, LocationName, Comment);
GO
