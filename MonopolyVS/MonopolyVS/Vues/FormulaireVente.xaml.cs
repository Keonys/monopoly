﻿using MonopolyVS.Controleurs;
using MonopolyVS.Modeles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MonopolyVS.Vues
{
    /// <summary>
    /// Logique d'interaction pour FormulaireVente.xaml
    /// </summary>
    public partial class FormulaireVente : Window
    {
        #region MEMBRES
        private readonly Joueur Vendeur;
        private Joueur Acheteur;

        List<Joueur> listeAcheteurs = new List<Joueur>();
        Banque banque;
        private Propriete prop;

        private Controleur C;
        #endregion

        #region CONSTRUCTEURS
        public FormulaireVente()
        {
            InitializeComponent();
            foreach (Joueur j in listeAcheteurs)
            {
                ComboJoueur.Items.Add(j.Nom);
            }
        }

        /// <summary>
        /// Initialise un formulaire de vente
        /// </summary>
        /// <param name="v">Joueur vendeur</param>
        /// <param name="joueursEnJeu">Liste des joueurs en jeu</param>
        /// <param name="b">Banque de la partie</param>
        public FormulaireVente(Joueur v, List<Joueur> joueursEnJeu, Banque b, Propriete nomProp , Controleur c)
        {
            InitializeComponent();
            Vendeur = v;
            foreach(Joueur j in joueursEnJeu)
            {
                if (j != Vendeur)
                    listeAcheteurs.Add(j);
            }
            banque = b;
            prop = nomProp;
            C = c;

            if (prop.Hotel == true)
            {
                ButtonVendreMaison.IsEnabled = true;
                ButtonVendreMaison.Content = "Vendre l'Hôtel";
            }
            else if (prop.NbrMaison != 0)
                ButtonVendreMaison.IsEnabled = true;
            foreach (Joueur j in listeAcheteurs)
            {
                ComboJoueur.Items.Add(j.Nom);
            }
        }
        #endregion

        #region METHODES
        private void ButtonValider_Click(object sender, RoutedEventArgs e)
        {
            if (ComboJoueur.SelectedIndex >= 0 && CheckPrix(TextBoxPrix.Text))
            {
                Acheteur = listeAcheteurs[ComboJoueur.SelectedIndex];
                banque.Transaction(Acheteur, Vendeur, int.Parse(TextBoxPrix.Text));
                banque.VendPropriete(this, prop, Vendeur);
                C.SwitchVerrouFenetre();
                C.C.VentePropriete(Vendeur.Nom, prop.Nom, Acheteur.Nom, int.Parse(TextBoxPrix.Text));
                Close();
            }
            else if (ComboJoueur.SelectedIndex <= 0)
                LabelInfo.Content = "Choisissez un acheteur.";
        }
        
        /// <summary>
        /// Vérifie si le prix en paramètre est un nombre non signé
        /// </summary>
        /// <param name="prix">Prix à vérifier</param>
        /// <returns>Si est un nombre ou pas</returns>
        private bool CheckPrix(string prix)
        {
            bool isNumber = true;
            foreach (char ca in prix)
            {
                if (ca <= '0' && ca >= '9')
                    isNumber = false;
            }

            if (isNumber == true)
                return isNumber;
            else
            {
                LabelInfo.Content = "Entrez un prix valide";
                return isNumber;
            }
        }

        #region ACCESSEURS & MUTATEURS
        public Joueur GetVendeur()
        {
            return Vendeur;
        }

        public Joueur GetAcheteur()
        {
            return Acheteur;
        }
        #endregion

        #endregion

        private void ButtonVendreMaison_Click_1(object sender, RoutedEventArgs e)
        {
            if (prop.Hotel == true)
            {
                prop.Hotel = false;
                banque.DetruireHotel(1);
                Vendeur.Argent += prop.PrixMaison / 2;
                C.C.VenteMaisonHotel(true, Vendeur.Nom, prop.PrixMaison / 2);
                C.SwitchVerrouFenetre();
                Close();
            }
            else
            {
                prop.NbrMaison--;
                banque.DetruireMaison(1);
                Vendeur.Argent += prop.PrixMaison / 2;
                C.C.VenteMaisonHotel(false, Vendeur.Nom, prop.PrixMaison / 2);
                C.SwitchVerrouFenetre();
                Close();
            }
        }
    }
}
