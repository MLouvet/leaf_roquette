SET IDENTITY_INSERT [Leaf].[Client] ON
INSERT INTO [Leaf].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (2, N'Ancel', N'UBISOFT', N'5505 Boulevard St-Laurent, Montreal, QC H3T 1S6, Canda', N'ancel@ubi.com', N'+1 514-490-2000')
INSERT INTO [Leaf].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (4, N'Kaplan', N'BLIZZARD', N'145 Rue Yves le Coz 78000 Versailles', N'kaplan@blizz.com', N'+33130679000')
INSERT INTO [Leaf].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (5, N'Genty', N'LEAF', N'rue Joliot-Curie 91190 Gif-sur-Yvette', N'pierre.genty@u-psud.fr', N'+33642567898')
SET IDENTITY_INSERT [Leaf].[Client] OFF
