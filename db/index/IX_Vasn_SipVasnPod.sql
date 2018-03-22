USE Entt
GO

CREATE NONCLUSTERED INDEX IX_Vasn_SipVasnPod
ON [Entt].[Vasn] ([OfficeNo],[ItemTypeCode],[ConsignmentNo])
INCLUDE ([Comment],[DateTime],[OfficeName],[CourierId],[VanItemTypeCode],[VanItemTypeDesc],[VanSenderName],[DataFlag])

GO
