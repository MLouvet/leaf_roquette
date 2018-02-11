/* Drop all Foreign Key constraints */
DECLARE @name VARCHAR(128)
DECLARE @constraint VARCHAR(254)
DECLARE @SQL VARCHAR(254)

SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)

WHILE @name is not null
BEGIN
    SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
    WHILE @constraint IS NOT NULL
    BEGIN
        SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] DROP CONSTRAINT [' + RTRIM(@constraint) +']'
        EXEC (@SQL)
        PRINT 'Dropped FK Constraint: ' + @constraint + ' on ' + @name
        SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
    END
SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'FOREIGN KEY' ORDER BY TABLE_NAME)
END
GO

/* Drop all Primary Key constraints */
DECLARE @name VARCHAR(128)
DECLARE @constraint VARCHAR(254)
DECLARE @SQL VARCHAR(254)

SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)

WHILE @name IS NOT NULL
BEGIN
    SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
    WHILE @constraint is not null
    BEGIN
        SELECT @SQL = 'ALTER TABLE [dbo].[' + RTRIM(@name) +'] DROP CONSTRAINT [' + RTRIM(@constraint)+']'
        EXEC (@SQL)
        PRINT 'Dropped PK Constraint: ' + @constraint + ' on ' + @name
        SELECT @constraint = (SELECT TOP 1 CONSTRAINT_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' AND CONSTRAINT_NAME <> @constraint AND TABLE_NAME = @name ORDER BY CONSTRAINT_NAME)
    END
SELECT @name = (SELECT TOP 1 TABLE_NAME FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE constraint_catalog=DB_NAME() AND CONSTRAINT_TYPE = 'PRIMARY KEY' ORDER BY TABLE_NAME)
END
GO


/* Drop all tables */
DECLARE @name VARCHAR(128)
DECLARE @SQL VARCHAR(254)

SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'U' AND category = 0 ORDER BY [name])

WHILE @name IS NOT NULL
BEGIN
    SELECT @SQL = 'DROP TABLE [dbo].[' + RTRIM(@name) +']'
    EXEC (@SQL)
    PRINT 'Dropped Table: ' + @name
    SELECT @name = (SELECT TOP 1 [name] FROM sysobjects WHERE [type] = 'U' AND category = 0 AND [name] > @name ORDER BY [name])
END
GO

CREATE TABLE [Roles] (
    [nom] VARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([nom] ASC)
);

CREATE TABLE [Client] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [nom]       VARCHAR (50)  NOT NULL,
    [compagnie] VARCHAR (50)  NOT NULL,
    [adresse]   VARCHAR (100) NOT NULL,
    [mail]      VARCHAR (50)  NOT NULL,
    [telephone] VARCHAR (15)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO



CREATE TABLE [Collaborateurs] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [nom]         VARCHAR (50) NOT NULL,
    [prenom]      VARCHAR (50) NOT NULL,
    [identifiant] VARCHAR (30) NOT NULL,
    [mdp]         VARCHAR (50) NOT NULL,
    [mail]        VARCHAR (50) NOT NULL,
    [statut]      VARCHAR (30) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_status_collab] FOREIGN KEY ([statut]) REFERENCES [Roles] ([nom])
);
GO


CREATE TABLE [Admin] (
    [Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_admin_id] FOREIGN KEY ([Id]) REFERENCES [Collaborateurs] ([Id])
);
GO

CREATE TABLE [SuperAdmin] (
    [Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SuperAdmin_id] FOREIGN KEY ([Id]) REFERENCES [Admin] ([Id])
);
GO

CREATE TABLE [Projet] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [nom]         VARCHAR (50) NOT NULL,
    [debut]       DATE         NOT NULL,
    [echeance]    DATE         NOT NULL,
    [client]      INT          NULL,
    [responsable] INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_Repo_Proj] FOREIGN KEY ([responsable]) REFERENCES [Collaborateurs] ([Id]),
    CONSTRAINT [FK_Client_proj] FOREIGN KEY ([client]) REFERENCES [Client] ([Id])
);
GO

CREATE TABLE [Tache] (
    [Id]             INT          IDENTITY (1, 1) NOT NULL,
    [nom ]           VARCHAR (50) NOT NULL,
    [debut]          DATE         NULL,
    [fin]            DATE         NULL,
    [charge_estimee] INT          NOT NULL,
    [progres]        INT          NOT NULL,
    [IdProj]         INT          NOT NULL,
    [CollabId]       INT          NOT NULL,
    [Super_tache]    INT          NULL,
    [depends]        INT          NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_Dependance] FOREIGN KEY ([depends]) REFERENCES [Tache] ([Id]),
    CONSTRAINT [Fk_Sous_Tache] FOREIGN KEY ([Super_tache]) REFERENCES [Tache] ([Id]),
    CONSTRAINT [Fk_Tach_Coll] FOREIGN KEY ([CollabId]) REFERENCES [Collaborateurs] ([Id]),
    CONSTRAINT [Fk_Tach_Proj] FOREIGN KEY ([IdProj]) REFERENCES [Projet] ([Id])
);
GO


CREATE TABLE [Notification] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [message]      VARCHAR (MAX) NOT NULL,
	[idProjet]	   INT,
	[idTache]	   INT,
    [lue]          BIT           NOT NULL,
    [horodatage]   DATETIME      NOT NULL,
    [destinataire] INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [Fk_notification_to] FOREIGN KEY ([destinataire]) REFERENCES [Collaborateurs] ([Id]),
	CONSTRAINT [Fk_projet_id] FOREIGN KEY ([idProjet]) REFERENCES [Projet] ([Id]),
	CONSTRAINT [Fk_tache_id] FOREIGN KEY ([idTache]) REFERENCES [Tache] ([Id])
);
GO

/* Insertion de données */

INSERT INTO [Roles] ([nom]) VALUES (N'CHEF_PROJET')
INSERT INTO [Roles] ([nom]) VALUES (N'COLLABORATEUR')
INSERT INTO [Roles] ([nom]) VALUES (N'ADMIN')
INSERT INTO [Roles] ([nom]) VALUES (N'SUPER_ADMIN')

SET IDENTITY_INSERT [Client] ON
INSERT INTO [dbo].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (2, N'Ancel', N'UBISOFT', N'5505 Boulevard St-Laurent, Montreal, QC H3T 1S6, Canda', N'ancel@ubi.com', N'+1 514-490-2000')
INSERT INTO [dbo].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (4, N'Kaplan', N'BLIZZARD', N'145 Rue Yves le Coz 78000 Versailles', N'kaplan@blizz.com', N'+33130679000')
INSERT INTO [dbo].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (5, N'Genty', N'LEAF', N'rue Joliot-Curie 91190 Gif-sur-Yvette', N'pierre.genty@u-psud.fr', N'+33642567898')
INSERT INTO [dbo].[Client] ([Id], [nom], [compagnie], [adresse], [mail], [telephone]) VALUES (6, N'Rath', N'CAPCOM', N'3-1-3 Uchihirano-machi, Chuo-ku, Osaka
540-0037, Japan', N'rath@capcom.com', N'+816920-3600')
SET IDENTITY_INSERT [Client] OFF

SET IDENTITY_INSERT [Collaborateurs] ON
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (2, N'MORPHE', N'Albert', N'albert.morphe', N'morphe', N'albert.morphe@leaf.fr', N'CHEF_PROJET')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (3, N'PEON', N'Evan', N'evan.peon', N'peon', N'evan.peon@leaf.fr', N'COLLABORATEUR')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (7, N'LAGANN', N'Karen', N'karen.lagann', N'lagann', N'karen.lagann@leaf.fr', N'COLLABORATEUR')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (8, N'D''MIN', N'Abou', N'abou.dmin', N'dmin', N'abou.dmin@leaf.fr', N'ADMIN')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (9, N'OS', N'Glad', N'glad.os', N'superadmin', N'glad.os@leaf.fr', N'SUPER_ADMIN')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (10, N'SaDiablo', N'Saetan', N'saetan.sadiablo', N'diablosa', N'seatan.sadiablo@leaf.fr', N'ADMIN')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (12, N'Angelline', N'Jaenelle', N'jaenelle.angelline', N'gelline', N'jaenelle.angeline@leaf.fr', N'ADMIN')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (13, N'Sadi', N'Daemon', N'daemon.sadi', N'jaenelle', N'daemon.sadi@leaf.fr', N'CHEF_PROJET')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (14, N'Lucivar', N'Yaslana', N'lucivar.yaslana', N'daemonar', N'lucivar.yaslana@leaf.fr', N'CHEF_PROJET')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (15, N'Dorothea', N'SaDiablo', N'dorothea.sadiablo', N'loveofpower', N'dorthea.sadiablo@leaf.fr', N'COLLABORATEUR')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (16, N'Hekatah', N'SaDiablo', N'hekatah.sadiablo', N'lovepower', N'hekatah.sadiablo@leaf.fr', N'CHEF_PROJET')
INSERT INTO [dbo].[Collaborateurs] ([Id], [nom], [prenom], [identifiant], [mdp], [mail], [statut]) VALUES (17, N'Surreal', N'SaDiablo', N'surreal.sadiablo', N'assassin', N'surreal.sadiablo@leaf.fr', N'COLLABORATEUR')
SET IDENTITY_INSERT [Collaborateurs] OFF

SET IDENTITY_INSERT [Projet] ON
INSERT INTO [dbo].[Projet] ([Id], [nom], [debut], [echeance], [client], [responsable]) VALUES (1, N'Overwatch', N'2013-05-14', N'2019-06-14', 2, 2)
INSERT INTO [dbo].[Projet] ([Id], [nom], [debut], [echeance], [client], [responsable]) VALUES (2, N'Monster Hunter World', N'2013-06-15', N'2018-12-31', 6, 7)
SET IDENTITY_INSERT [Projet] OFF

SET IDENTITY_INSERT [dbo].[Tache] ON
INSERT INTO [dbo].[Tache] ([Id], [nom ], [debut], [fin], [charge_estimee], [progres], [IdProj], [CollabId], [Super_tache], [depends]) VALUES (1, N'Ajout de l''interface', N'2017-09-24', N'2017-09-28', 3, 0, 1, 2, 1, 1)
INSERT INTO [dbo].[Tache] ([Id], [nom ], [debut], [fin], [charge_estimee], [progres], [IdProj], [CollabId], [Super_tache], [depends]) VALUES (4, N'Implémenter back-face culling', N'2017-10-01', N'2017-10-09', 2, 1, 1, 2, NULL, NULL)
INSERT INTO [dbo].[Tache] ([Id], [nom ], [debut], [fin], [charge_estimee], [progres], [IdProj], [CollabId], [Super_tache], [depends]) VALUES (7, N'Création du moteur de jeu', N'2013-09-15', N'2014-09-15', 3, 100, 2, 3, NULL, NULL)
INSERT INTO [dbo].[Tache] ([Id], [nom ], [debut], [fin], [charge_estimee], [progres], [IdProj], [CollabId], [Super_tache], [depends]) VALUES (11, N'Implementation du systeme de chasse', N'2013-09-23', N'2014-02-26', 1, 100, 2, 8, 7, NULL)
INSERT INTO [dbo].[Tache] ([Id], [nom ], [debut], [fin], [charge_estimee], [progres], [IdProj], [CollabId], [Super_tache], [depends]) VALUES (14, N'Création du moteur graphique', N'2013-09-23', N'2014-02-26', 1, 100, 2, 3, 7, NULL)
INSERT INTO [dbo].[Tache] ([Id], [nom ], [debut], [fin], [charge_estimee], [progres], [IdProj], [CollabId], [Super_tache], [depends]) VALUES (15, N'Implementation du gameplay', N'2014-02-26', N'2014-09-15', 1, 100, 2, 7, 7, 11)
INSERT INTO [dbo].[Tache] ([Id], [nom ], [debut], [fin], [charge_estimee], [progres], [IdProj], [CollabId], [Super_tache], [depends]) VALUES (16, N'Création des armes', N'2013-10-15', N'2018-01-26', 1, 100, 2, 3, NULL, NULL)
INSERT INTO [dbo].[Tache] ([Id], [nom ], [debut], [fin], [charge_estimee], [progres], [IdProj], [CollabId], [Super_tache], [depends]) VALUES (17, N'Création des armures', N'2013-10-15', N'2018-01-26', 1, 100, 2, 2, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Tache] OFF

SET IDENTITY_INSERT [dbo].[Notification] ON
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (2, N'Tâche en retard', 1, 1, 1, N'2017-09-24 00:00:00', 2)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (3, N'Tâche ajoutée', 1, 4, 0, N'2017-10-01 00:00:00', 2)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (6, N'Projet commencé', 1, NULL, 1, N'2017-03-03 00:00:00', 2)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (7, N'Projet commencé', 2, NULL, 1, N'2013-06-15 00:00:00', 7)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (8, N'Tâche ajoutée', 2, 7, 1, N'2013-09-15 00:00:00', 3)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (9, N'Sous tâche ajoutée', 2, 11, 1, N'2013-09-23 00:00:00', 8)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (10, N'Sous tâche ajoutée', 2, 14, 1, N'2013-09-23 00:00:00', 3)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (11, N'Sous tâche ajoutée', 2, 15, 1, N'2014-02-26 00:00:00', 7)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (12, N'Tâche ajoutée', 2, 16, 1, N'2013-10-15 00:00:00', 3)
INSERT INTO [dbo].[Notification] ([Id], [message], [idProjet], [idTache], [lue], [horodatage], [destinataire]) VALUES (13, N'Tache ajoutée', 2, 17, 1, N'2013-10-15 00:00:00', 2)
SET IDENTITY_INSERT [dbo].[Notification] OFF

INSERT INTO [dbo].[Admin] ([Id]) VALUES (8)
INSERT INTO [dbo].[Admin] ([Id]) VALUES (9)
INSERT INTO [dbo].[Admin] ([Id]) VALUES (10)
INSERT INTO [dbo].[Admin] ([Id]) VALUES (12)
INSERT INTO [SuperAdmin] ([Id]) VALUES (9)
GO
