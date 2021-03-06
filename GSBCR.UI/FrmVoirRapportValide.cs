﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GSBCR.modele;
using GSBCR.BLL;

namespace GSBCR.UI
{
    public partial class FrmVoirRapportValide : Form
    {
        private string matricule;
        private string med;
        public FrmVoirRapportValide(string mat)
        {
            InitializeComponent();
            matricule = mat;
            bsRapports.DataSource = Manager.ChargerRapportVisiteurFinis(mat);
            cbxRapport.DataSource = bsRapports;
            cbxRapport.DisplayMember = "RAP_NUM";
            if (bsRapports.Count == 0)
            {
                MessageBox.Show("Il n'y a aucun rapport de visite.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.ShowDialog();
            }
        }
        private void cbxRapport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxRapport.SelectedIndex != -1)
            {
                RAPPORT_VISITE r = (RAPPORT_VISITE)cbxRapport.SelectedItem;
                ucRapportValide1.LeRapportVisite = r;
                ucRapportValide1.Visible = true;
                List<RAPPORT_VISITE> v;              
                v = Manager.ChargerRapportVisiteurFinis(matricule);
            }
        }

        private void btnRetour_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAfficherMed1_Click(object sender, EventArgs e)
        {
            if(ucRapportValide1.listCodeMed.Items.Count != 0)
            {
                ucRapportValide1.listCodeMed.SelectedIndex = 0;
                med = ucRapportValide1.listCodeMed.SelectedItem.ToString();
                if (med != null)
                {
                    FrmUnMedicamment f = new FrmUnMedicamment(med);
                    f.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Le medicament n°1 n'est pas dans le rapport.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAfficherMed2_Click(object sender, EventArgs e)
        {
            if (ucRapportValide1.listCodeMed.Items.Count != 0 && ucRapportValide1.listCodeMed.Items.Count != 1)
            {
                ucRapportValide1.listCodeMed.SelectedIndex = 1;
                med = ucRapportValide1.listCodeMed.SelectedItem.ToString();
                if (med != null)
                {
                    FrmUnMedicamment f = new FrmUnMedicamment(med);
                    f.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Le medicament n°2 n'est pas dans le rapport.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAfficherPracticien_Click(object sender, EventArgs e)
        {
            string p = ucRapportValide1.txtBoxCodePra.Text.ToString();
            short pr = Convert.ToInt16(p);
            FrmUnPracticien fp = new FrmUnPracticien(pr);
            fp.ShowDialog();
        }
    }
}
