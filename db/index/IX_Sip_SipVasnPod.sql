USE Entt;
GO

CREATE NONCLUSTERED INDEX IX_Sip_SipVasnPod
ON [Entt].[Sip] ([DateTime])
INCLUDE ([Comment],[OfficeNo],[OfficeName],[BeatNo],[CourierId],[CourierName],[ItemTypeCode],[ConsignmentNo],[DataFlag],[ScannerId])
GO