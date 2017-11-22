USE [Entt]
GO
CREATE NONCLUSTERED INDEX [IX_StatusCode_Pup]
ON [Entt].[StatusCode] ([ItemTypeCode],[DateTime],[StatusCode])
INCLUDE ([OfficeNo],[OfficeName],[ConsignmentNo])
GO