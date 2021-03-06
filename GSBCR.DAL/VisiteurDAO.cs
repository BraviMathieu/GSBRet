﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSBCR.modele;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


namespace GSBCR.DAL
{
    public static class VisiteurDAO
    {
        /// <summary>
        /// Permet de retrouver les infos d'un visiteur à partir de son login
        /// </summary>
        /// <param name="matricule">matricule Visiteur</param>
        /// <returns>VISITEUR</returns>
        public static VISITEUR FindById(string matricule)
        {
            VISITEUR vis = null;
            using (var context = new GSB_VisiteEntities())
            {
                //désactiver le chargement différé
                //context.Configuration.LazyLoadingEnabled = false;
                var req = from v in context.VISITEUR
                          where v.VIS_MATRICULE == matricule
                          orderby v.VIS_NOM
                          select v; 
                vis = req.SingleOrDefault<VISITEUR>();
            }
            return vis;
        }

        public static List<VISITEUR> FindBySecteur(string respon, string secteur)
        {
            List<VISITEUR> vis = null;
            using (var context = new GSB_VisiteEntities())
            {
                //context.Configuration.LazyLoadingEnabled = false;
                var req = from v in context.VISITEUR
                          where v.SEC_CODE == secteur && v.VIS_MATRICULE != respon
                          orderby v.VIS_NOM
                          select v;
                vis = req.ToList<VISITEUR>();
            }
            return vis;
        }
        public static void UpdateVisiteurMdp(string mat, string nmdp)
        {
            using (var context = new GSB_VisiteEntities())
            {
                var req =
                    (from v in context.VISITEUR
                     where v.VIS_MATRICULE == mat
                     select v).Single();

                req.vis_mdp = nmdp;

                context.SaveChanges();
            }
        }

        public static void UpdateVisiteurInfo(string mat, string adr, string CP, string ville, string nadr, string nCP, string nville)
        {
            using (var context = new GSB_VisiteEntities())
            {
                var req =
                   (from v in context.VISITEUR
                    where v.VIS_MATRICULE == mat
                    select v).Single();

                req.VIS_ADRESSE = nadr;
                req.VIS_CP = nCP;
                req.VIS_VILLE = nville;
                    
                context.SaveChanges();
            }
        }
        
    }
}
