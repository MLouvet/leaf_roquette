using Leaf.DAL.ScaffoldedModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Leaf.DAL.Utilities.Translator
{
    public class TacheTranslator
    {
        public static Tache DalToDto(ScaffoldedModels.Tache pTache)
        {
            return new Tache
            {
                ChargeEstimee = pTache.ChargeEstimee,
                //Collab = CollaborateursTranslator.DalToDto(pTache.Collab),
                CollabId = pTache.CollabId,
                Debut = pTache.Debut,
                //Depends = pTache.Depends,
                //DependsNavigation = DalToDto(pTache.DependsNavigation),
                Fin = pTache.Fin,
                Id = pTache.Id,
                IdProj = pTache.IdProj,
                //IdProjNavigation = ProjetTranslator.DalToDto(pTache.IdProjNavigation),
                //InverseDependsNavigation = DalToDto(pTache.InverseDependsNavigation),
                //InverseSuperTacheNavigation = DalToDto(pTache.InverseSuperTacheNavigation),
                Nom = pTache.Nom,
                Progres = pTache.Progres,
                SuperTache = pTache.SuperTache,
                //SuperTacheNavigation = DalToDto(pTache.SuperTacheNavigation)
            };
        }

        public static ICollection<Tache> DalToDto(ICollection<ScaffoldedModels.Tache> pTache)
        {
            var tacheList = new Collection<Tache>();
            foreach (var tache in pTache)
            {
                tacheList.Add(DalToDto(tache));
            }

            return tacheList;
        }
    }
}
