using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WPFModernVerticalMenu.Model;

namespace WPFModernVerticalMenu.Service
{
    public class UtilisateurService
    {
        /// <summary>
        /// Récupère la liste des utilisateurs à partir du serveur.
        /// </summary>
        /// <returns>Liste des utilisateurs.</returns>
        public List<Utilisateur> GetListUtilisateur()
        {
            HttpClient client = new HttpClient();
            var services = new List<Utilisateur>();

            // Configurer l'URL de base pour les requêtes HTTP
            client.BaseAddress = new Uri(ConfigurationSettings.AppSettings["linkServeurPhp"]);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Envoyer une requête GET pour récupérer la liste des utilisateurs
            var response = client.GetAsync("backend/List.php").Result;

            if (response.IsSuccessStatusCode)
            {
                var responsedata = response.Content.ReadAsStringAsync().Result;
                // Désérialiser les données JSON en une liste d'objets Utilisateur
                services = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Utilisateur>>(responsedata);
            }
            return services;
        }

        /// <summary>
        /// Ajoute un nouvel utilisateur sur le serveur.
        /// </summary>
        /// <param name="user">Utilisateur à ajouter.</param>
        /// <returns>True si l'utilisateur a été ajouté avec succès, sinon False.</returns>
        public bool AddUtilisateur(Utilisateur user)
        {
            bool rep = false;
            string Id = user.id > 0 ? user.id.ToString() : "0";
            var values = new Dictionary<string, string>
            {
                {"id", Id},
                {"nom", user.nom},
                {"prenom", user.prenom},
                {"age", user.age.ToString()}
            };
            var content = new FormUrlEncodedContent(values);

            try
            {
                using (var client = new HttpClient())
                {
                    // Configurer l'URL de base pour les requêtes HTTP
                    client.BaseAddress = new Uri(ConfigurationSettings.AppSettings["linkServeurPhp"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Envoyer une requête POST pour ajouter un nouvel utilisateur
                    var response = client.PostAsync("backend/Create.php", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception selon les besoins
            }
            return rep;
        }

        /// <summary>
        /// Met à jour un utilisateur existant sur le serveur.
        /// </summary>
        /// <param name="user">Utilisateur à mettre à jour.</param>
        /// <returns>True si l'utilisateur a été mis à jour avec succès, sinon False.</returns>
        public bool UpdateUtilisateur(Utilisateur user)
        {
            bool rep = false;
            var values = new Dictionary<string, string>
            {
                {"id", user.id.ToString()},
                {"nom", user.nom},
                {"prenom", user.prenom},
                {"age", user.age.ToString()}
            };
            var content = new FormUrlEncodedContent(values);

            try
            {
                using (var client = new HttpClient())
                {
                    // Configurer l'URL de base pour les requêtes HTTP
                    client.BaseAddress = new Uri(ConfigurationSettings.AppSettings["linkServeurPhp"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Envoyer une requête POST pour mettre à jour l'utilisateur existant
                    var response = client.PostAsync("backend/Update.php", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception selon les besoins
            }
            return rep;
        }

        /// <summary>
        /// Supprime un utilisateur existant sur le serveur.
        /// </summary>
        /// <param name="userId">ID de l'utilisateur à supprimer.</param>
        /// <returns>True si l'utilisateur a été supprimé avec succès, sinon False.</returns>
        public bool DeleteUtilisateur(int userId)
        {
            bool rep = false;
            var values = new Dictionary<string, string>
            {
                {"id", userId.ToString()}
            };
            var content = new FormUrlEncodedContent(values);

            try
            {
                using (var client = new HttpClient())
                {
                    // Configurer l'URL de base pour les requêtes HTTP
                    client.BaseAddress = new Uri(ConfigurationSettings.AppSettings["linkServeurPhp"]);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Envoyer une requête POST pour supprimer l'utilisateur
                    var response = client.PostAsync("backend/Delete.php", content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        rep = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Gérer l'exception selon les besoins
            }
            return rep;
        }
    }
}
