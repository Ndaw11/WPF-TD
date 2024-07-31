using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFModernVerticalMenu.Model;
using WPFModernVerticalMenu.Service;

namespace WPFModernVerticalMenu.Pages
{
    /// <summary>
    /// Logique d'interaction pour frmUtilisateur.xaml
    /// </summary>
    public partial class frmUtilisateur : Page
    {
        public frmUtilisateur()
        {
            InitializeComponent();
            dgUtilisateur.ItemsSource = Service.GetListUtilisateur();
        }
        // Instance du service pour gérer les utilisateurs
        UtilisateurService Service = new UtilisateurService();
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

     

        // Utilisateur actuellement sélectionné dans la DataGridView
        private Utilisateur selectUser;
        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            // Vérification si tous les champs sont remplis
            if (string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtPrenom.Text) || string.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs avant d'ajouter un utilisateur.", "Erreur");
                return;
            }

            // Vérification que le nom et le prénom contiennent uniquement des lettres
            if (!IsString(txtNom.Text) || !IsString(txtPrenom.Text))
            {
                MessageBox.Show("Le nom et le prénom doivent contenir uniquement des lettres.", "Erreur");
                return;
            }

            // Vérification que l'âge est un nombre valide
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Veuillez entrer un âge valide.", "Erreur");
                return;
            }




            // Création d'un nouvel utilisateur avec les informations saisies
            Utilisateur user = new Utilisateur
            {
                nom = txtNom.Text,
                prenom = txtPrenom.Text,
                age = age
            };

            // Appel du service pour ajouter l'utilisateur et vérifier si l'ajout a réussi
            bool result = Service.AddUtilisateur(user);

            if (result)
            {
                MessageBox.Show("Utilisateur ajouté avec succès.", "Succès");
            }
            else
            {
                MessageBox.Show("L'ajout de l'utilisateur a échoué.", "Erreur");
            }

            // Effacement des champs de texte et mise à jour de la liste des utilisateurs
            Effacer();
        }

        // Méthode pour vérifier si une chaîne contient uniquement des lettres
        private bool IsString(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        // Méthode pour réinitialiser les champs de saisie et mettre à jour la liste des utilisateurs
        private void Effacer()
        {
            txtNom.Text = string.Empty;
            txtPrenom.Text = string.Empty;
            txtAge.Text = string.Empty;
            dgUtilisateur.ItemsSource = Service.GetListUtilisateur();
            txtNom.Focus();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            if (selectUser != null)
            {
                // Appelle le service pour supprimer l'utilisateur et réinitialise les champs après suppression
                Service.DeleteUtilisateur(selectUser.id);
                Effacer();
                selectUser = null;
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur.");
                return;
            }
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
            if (dgUtilisateur.SelectedItem != null)
            {
                selectUser = (Utilisateur)dgUtilisateur.SelectedItem;

                txtNom.Text = selectUser.nom;
                txtPrenom.Text = selectUser.prenom;
                txtAge.Text = selectUser.age.ToString();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur.");
            }
        }



        private void btnModifier_Click(object sender, RoutedEventArgs e)
        {
            // Vérifie si un utilisateur est sélectionné
            if (selectUser == null)
            {
                MessageBox.Show("Veuillez sélectionner un utilisateur.");
                return;
            }

            // Vérifie si tous les champs sont remplis
            if (string.IsNullOrEmpty(txtNom.Text) || string.IsNullOrEmpty(txtPrenom.Text) || string.IsNullOrEmpty(txtAge.Text))
            {
                MessageBox.Show("Veuillez remplir tous les champs.");
                return;
            }

            // Vérifie que le nom et le prénom contiennent uniquement des lettres
            if (!IsString(txtNom.Text) || !IsString(txtPrenom.Text))
            {
                MessageBox.Show("Le nom et le prénom doivent contenir uniquement des lettres.", "Erreur");
                return;
            }

            // Vérifie que l'âge est un nombre valide
            if (!int.TryParse(txtAge.Text, out int age))
            {
                MessageBox.Show("Veuillez entrer un âge valide.");
                return;
            }

            // Met à jour l'utilisateur sélectionné avec les nouvelles informations
            selectUser.nom = txtNom.Text;
            selectUser.prenom = txtPrenom.Text;
            selectUser.age = age;

            // Appel du service pour mettre à jour l'utilisateur et vérifier si la mise à jour a réussi
            bool result = Service.UpdateUtilisateur(selectUser);

            if (result)
            {
                MessageBox.Show("Utilisateur mis à jour avec succès.");
                selectUser = null;
            }
            else
            {
                MessageBox.Show("La mise à jour de l'utilisateur a échoué.");
                selectUser = null;
            }

            // Efface les champs de texte et met à jour la liste des utilisateurs après la mise à jour
            Effacer();
        }

        private void btnEffacer_Click(object sender, RoutedEventArgs e)
        {
            Effacer();
        }
    }
}
