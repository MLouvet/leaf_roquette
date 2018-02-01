SET IDENTITY_INSERT [Leaf].[Collaborateurs] ON
INSERT INTO [Leaf].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (2, N'MORPHE', N'Albert', N'albert.morphe', N'morphe', N'albert.morphe@leaf.fr', N'CHEF_PROJET')
INSERT INTO [Leaf].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (3, N'PEON', N'Evan', N'evan.peon', N'peon', N'evan.peon@leaf.fr', N'COLLABORATEUR')
INSERT INTO [Leaf].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (7, N'LAGANN', N'Karen', N'karen.lagann', N'lagann', N'karen.lagann@leaf.fr', N'COLLABORATEUR')
INSERT INTO [Leaf].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (8, N'D''MIN', N'Abou', N'abou.dmin', N'dmin', N'abou.dmin@leaf.fr', N'ADMIN')
INSERT INTO [Leaf].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (9, N'OS', N'Glad', N'glad.os', N'superadmin', N'glad.os@leaf.fr', N'SUPER_ADMIN')
SET IDENTITY_INSERT [Leaf].[Collaborateurs] OFF
