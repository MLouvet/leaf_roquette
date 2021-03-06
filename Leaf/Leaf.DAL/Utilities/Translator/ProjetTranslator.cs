﻿using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class ProjetTranslator
    {
        public static Projet DalToDto(ScaffoldedModels.Projet pProjet)
        {
            return new Projet
            {
                Client = pProjet.Client,
                //ClientNavigation = ClientTranslator.DalToDto(pProjet.ClientNavigation),
                Debut = pProjet.Debut,
                Echeance = pProjet.Echeance,
                Id = pProjet.Id,
                Nom = pProjet.Nom,
                Responsable = pProjet.Responsable,
                //ResponsableNavigation = CollaborateursTranslator.DalToDto(pProjet.ResponsableNavigation),
                Tache = TacheTranslator.DalToDto(pProjet.Tache)
            };
        }

        public static ICollection<Projet> DalToDto(ICollection<ScaffoldedModels.Projet> pProjet)
        {
            var projetList = new Collection<Projet>();
            foreach (var projet in pProjet)
            {
                projetList.Add(DalToDto(projet));
            }

            return projetList;
        }
    }
}
