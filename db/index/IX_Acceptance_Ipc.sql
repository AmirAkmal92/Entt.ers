USE Entt
GO

/*
CREATE NONCLUSTERED INDEX IX_Acceptance_Ipc
ON [Entt].[Acceptance] ([LocationId])
INCLUDE ([ConsignmentNo],[Weight],[ItemCategory],[LocationName])
*/

CREATE NONCLUSTERED INDEX IX_Acceptance_Ipc
ON [Entt].[Acceptance] ([LocationId])
INCLUDE ([ConsignmentNo],[DateTime],[Postcode],[Weight],[ItemCategory],[CourierId],[LocationName],[Pl9No])
GO
