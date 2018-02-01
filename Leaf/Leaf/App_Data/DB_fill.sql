SET IDENTITY_INSERT [dbo].[Client] ON
INSERT INTO [dbo].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (2, N'Ancel', N'UBISOFT', N'5505 Boulevard St-Laurent, Montreal, QC H3T 1S6, Canda', N'ancel@ubi.com', N'+1 514-490-2000')
INSERT INTO [dbo].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (4, N'Kaplan', N'BLIZZARD', N'145 Rue Yves le Coz 78000 Versailles', N'kaplan@blizz.com', N'+33130679000')
INSERT INTO [dbo].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (5, N'Genty', N'LEAF', N'rue Joliot-Curie 91190 Gif-sur-Yvette', N'pierre.genty@u-psud.fr', N'+33642567898')
SET IDENTITY_INSERT [dbo].[Client] OFF
SET IDENTITY_INSERT [dbo].[Collaborateurs] ON
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (2, N'MORPHE', N'Albert', N'albert.morphe', N'morphe', N'albert.morphe@leaf.fr', N'CHEF_PROJET')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (3, N'PEON', N'Evan', N'evan.peon', N'peon', N'evan.peon@leaf.fr', N'COLLABORATEUR')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (7, N'LAGANN', N'Karen', N'karen.lagann', N'lagann', N'karen.lagann@leaf.fr', N'COLLABORATEUR')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (8, N'D''MIN', N'Abou', N'abou.dmin', N'dmin', N'abou.dmin@leaf.fr', N'ADMIN')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (9, N'OS', N'Glad', N'glad.os', N'superadmin', N'glad.os@leaf.fr', N'SUPER_ADMIN')
SET IDENTITY_INSERT [dbo].[Collaborateurs] OFF
INSERT INTO [dbo].[Roles] ([nom]) VALUES (N'CHEF_PROJET')
INSERT INTO [dbo].[Roles] ([nom]) VALUES (N'COLLABORATEUR')
INSERT INTO [dbo].[Roles] ([nom]) VALUES (N'ADMIN')
INSERT INTO [dbo].[Roles] ([nom]) VALUES (N'SUPER_ADMIN')
SET IDENTITY_INSERT [dbo].[Projet] ON
INSERT INTO [dbo].[Projet] ([Id], [nom], [debut], [echeance], [client], [responsable]) VALUES (1, N'Overwatch', N'2013-05-14', N'2019-06-14', 2, 2)
SET IDENTITY_INSERT [dbo].[Projet] OFF
INSERT INTO [dbo].[Admin] ([Id]) VALUES (8)
INSERT INTO [dbo].[Admin] ([Id]) VALUES (9)
INSERT INTO [dbo].[SuperAdmin] ([Id]) VALUES (9)
