SET IDENTITY_INSERT [dbo].[Collaborateurs] ON
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (2, N'MORPHE', N'Albert', N'albert.morphe', N'morphe', N'albert.morphe@leaf.fr', N'CHEF_PROJET')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (3, N'PEON', N'Evan', N'evan.peon', N'peon', N'evan.peon@leaf.fr', N'COLLABORATEUR')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (7, N'LAGANN', N'Karen', N'karen.lagann', N'lagann', N'karen.lagann@leaf.fr', N'COLLABORATEUR')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (8, N'D''MIN', N'Abou', N'abou.dmin', N'dmin', N'abou.dmin@leaf.fr', N'ADMIN')
SET IDENTITY_INSERT [dbo].[Collaborateurs] OFF