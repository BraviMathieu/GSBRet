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
    public static class RapportVisiteDAO
    {
        /// <summary>
        /// Permet de retourner un rapport de visite en connaissant son id
        /// </summary>
        /// <param name="m">matricule Visiteur</param>
        /// <param name="n">numéro rapport</param>
        /// <returns>RAPPORT_VISITE</returns>
        public static RAPPORT_VISITE FindById(string m, int n)
        {
            RAPPORT_VISITE rv =null;
            // écrire et exécuter la requête Linq
            using (var context = new GSB_VisiteEntities())
            {
                //désactiver le chargement différé
                //context.Configuration.LazyLoadingEnabled = false;
                var req = from r in context.RAPPORT_VISITE
                          where r.RAP_MATRICULE == m && r.RAP_NUM == n
                          select r;
                rv = req.SingleOrDefault<RAPPORT_VISITE>();
                
            }
            return rv;
        }
        /// <summary>
        /// Permet de créer une liste avec tous les rapports de visite de visiteurs qui ont un certain état
        /// </summary>
        /// <param name="lesMatricules">Liste de matricule (string)</param>
        /// <param name="lesEtats">Liste d'états (int)</param>
        /// <returns></returns>
        public static List<RAPPORT_VISITE> FindByEtatEtVisiteur(List<string> lesMatricules, List<int> lesEtats)
        {
            List<RAPPORT_VISITE> lesRapports = null;
            using (var context = new GSB_VisiteEntities())
            {
                //désactiver le chargement différé
                //context.Configuration.LazyLoadingEnabled = false;
                int i = 0;
                string reqStr = "select * from RAPPORT_VISITE r where r.RAP_MATRICULE in(";
                foreach (string m in lesMatricules)
                {
                    if (i != 0)
                        reqStr += ",";
                    else
                        i++;
                    reqStr += "'" + m + "'";
                }
                reqStr += ") and r.RAP_ETAT in(";
                i = 0;
                foreach (int e in lesEtats)
                {
                    if (i != 0)
                        reqStr += ",";
                    else
                        i++;
                    reqStr += e ;
                }
                reqStr += ")";
                lesRapports = context.RAPPORT_VISITE.SqlQuery(reqStr).ToList<RAPPORT_VISITE>();
                
            } 
            return lesRapports;
        }

        /// <summary>
        /// Permet de créer une liste avec tout les rapport selon leur état
        /// </summary>
        /// <param name="lesEtats">Liste d'états (int)</param>
        /// <returns></returns>
        public static List<RAPPORT_VISITE> FindByEtat(List<int> lesEtats)
        {
            List<RAPPORT_VISITE> lesRapports = null;
            using (var context = new GSB_VisiteEntities())
            {
                int i = 0;
            string reqStr = "select * from RAPPORT_VISITE r where r.RAP_ETAT in(";
 
            foreach (int e in lesEtats)
            {
                if (i != 0)
                    reqStr += ",";
                else
                    i++;
                reqStr += e;
            }
            reqStr += ")";
            lesRapports = context.RAPPORT_VISITE.SqlQuery(reqStr).ToList<RAPPORT_VISITE>();

        } 
            return lesRapports;
        }
        /// <summary>
        /// Permet de créer un rapport dans la base de données par appel de la procédure stockée addRapport
        /// </summary>
        /// <param name="r">objet rapport de visite</param>
        public static void insert(RAPPORT_VISITE r)
        {
            using (var context = new GSB_VisiteEntities())
            {
                try
                {
                    //ajout du rapport au contexte
                    context.RAPPORT_VISITE.Add(r);
                    //sauvegarde du contexte
                    context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                } 
            }

        }

         /// <summary>
        /// Permet de mettre à jour un rapport dans la base de données par appel de la procédure stockée updRapport
        /// </summary>
        /// <param name="r">objet rapport de visite</param>
        public static void update(RAPPORT_VISITE r)
        {
            using (var context = new GSB_VisiteEntities())
            {
                try
                {
                    //mise à jour de l'état du rapport 
                    context.Entry<RAPPORT_VISITE>(r).State = System.Data.EntityState.Modified;
                    //sauvegarde du contexte
                    context.SaveChanges();
                }
                catch (Exception ex)
                {

                    throw ex;
                }
                
            }
        }
        /// <summary>
        /// Permet de retourner des rapports de visite si le visiteur en a pour un praticien
        /// </summary>
        /// <param name="visiteurcode">matricule Visiteur</param>
        /// <param name="praticiencode">matricule praticien</param>
        /// <returns>RAPPORT_VISITE</returns>
        public static List<RAPPORT_VISITE> FindRapportExiste(string visiteurcode, Int16 praticiencode)
        {
            List<RAPPORT_VISITE> rv = null;
            // écrire et exécuter la requête Linq
            using (var context = new GSB_VisiteEntities())
            {
                //context.Configuration.LazyLoadingEnabled = false;
                var req = from r in context.RAPPORT_VISITE
                          where r.RAP_MATRICULE == visiteurcode && r.RAP_PRANUM == praticiencode
                          select r;
                rv = req.ToList<RAPPORT_VISITE>();
            }
            return rv;
        }
        public static List<RAPPORT_VISITE> FindRapportExisteMedicament(string visiteurcode, string depotLegal)
        {
            List<RAPPORT_VISITE> rv = null;
            // écrire et exécuter la requête Linq
            using (var context = new GSB_VisiteEntities())
            {
                //context.Configuration.LazyLoadingEnabled = false;
                var req = from r in context.RAPPORT_VISITE
                          where r.RAP_MATRICULE == visiteurcode && r.RAP_MED1== depotLegal || r.RAP_MED2 == depotLegal
                          select r;
                rv = req.ToList<RAPPORT_VISITE>();
            }
            return rv;
        }

    }
}
