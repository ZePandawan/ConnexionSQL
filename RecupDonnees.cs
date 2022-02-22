using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnexionSQL
{
    class RecupDonnees
    {
        public static List<string> RecupDonn()
        {
            //Connexion à la base de données "master" qui existe dans tout les cas
            SqlConnection connection = DBUtils.GetDBConnection("master");

            //Requête SQL pour récupérer le nom de toutes les bases de données présentes
            SqlCommand cmd2 = new SqlCommand("SELECT name from sys.databases", connection);
            //Ouverture de la connexion
            connection.Open();
            //Execution de la requête
            DbDataReader reader = cmd2.ExecuteReader();
            //Liste qui va contenir les différentes bases de données
            List<string> listDatabase = new List<string>();
            //Parcourir le résultat de la requête
            while (reader.Read())
            {
                //Récupération du nom de la base de données
                string databaseName = reader.GetString(0).ToString();
                //Vérification du nom pour ne pas prendre les bases de données systèmes
                if (databaseName != "master" && databaseName != "tempdb" && databaseName != "model" && databaseName != "msdb")
                {
                    //Affichage dans la console du nom de la bdd puis ajout de celle-ci dans la liste
                    Console.WriteLine(reader.GetString(0));
                    listDatabase.Add(reader.GetString(0).ToString());
                }
            }
            reader.Close();

            //Vérification du nombre d'éléments dans la liste
            var nbDatabase = listDatabase.Count();
            Console.WriteLine(nbDatabase);
            //Fermeture de la connexion précédente
            connection.Close();

            List<string> listResult = new List<string>();

            for (int i = 0; i < nbDatabase; i++)
            {
                //Ecriture dans la console de la base à laquelle on va se connecter
                Console.WriteLine(listDatabase[i]);

                //Appel de la classe pour la connexion avec la base de données concernée
                listResult.Add(listDatabase[i].ToString());
                SqlConnection conn = DBUtils.GetDBConnection(listDatabase[i]);
                //Ouverture de la nouvelle connexion
                conn.Open();

                //Requete pour récupérer les noms des colonnes dans la table DONNEES
                string requete = "select * from INFORMATION_SCHEMA.COLUMNS where TABLE_Name = 'DONNEES'";

                //Appel de la classe qui permet d'executer les commandes
                SqlCommand cmd = new SqlCommand();

                //Passage des paramètres pour la classe ci-dessus
                cmd.Connection = conn;
                cmd.CommandText = requete;

                //Execution de la requête et résultat dans un reader
                DbDataReader reader2 = cmd.ExecuteReader();

                //Initialisation d'une variable vide
                string strDatabase = "";

                //On regarde ligne par ligne dans le reader
                while (reader2.Read())
                {
                    //Si c'est la première fois que l'on écrit dans cette variable
                    if (strDatabase != "")
                    {
                        //GetString(3) correspond aux noms des colonnes
                        strDatabase = strDatabase + " " + reader2.GetString(3);
                    }
                    else
                    {
                        strDatabase = reader2.GetString(3);
                    }
                }

                //Console.WriteLine(tab);
                //On divise la chaine de caractères précédentes au niveau des espaces pour en faire un tableau
                string[] tabDatabase = strDatabase.Split(' ');

                //Test en affichant les différentes colonnes dans la console
                foreach (var database in tabDatabase)
                {
                    Console.WriteLine(database);
                }
                reader2.Close();

                //En utilisant la connexion
                using (conn)
                {
                    //Nouvelle requête qui permet de lire toutes les données
                    SqlCommand command = new SqlCommand("SELECT * FROM DONNEES;", conn);

                    //Execution de la requête
                    SqlDataReader reader3 = command.ExecuteReader();

                    //On regarde si il y a quelque chose dans le reader
                    if (reader3.HasRows)
                    {
                        //Si oui alors on regarde ligne par ligne
                        while (reader3.Read())
                        {
                            //Initialisation variable vide
                            string value = "";
                            //On parcourt le tableau avec les noms des colonnes pour pouvoir savoir qu'est-ce qu'il faut aller chercher en données
                            for (var index = 0; index < tabDatabase.Length; index++)
                            {
                                //Si c'est la première fois
                                if (value == "")
                                {
                                    //On transforme la variable qui était un double en DateTime afin d'avoir un vraiment format de date
                                    DateTime dt = DateTime.FromOADate(reader3.GetDouble(index));
                                    value = value + dt;
                                }
                                else
                                {
                                    value = value + "\t" + reader3.GetDouble(index);
                                }
                            }
                            listResult.Add(value);
                            //Affichage des résultats dans la console
                            Console.WriteLine(value);
                            //Console.WriteLine("{0}", dataValue);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Pas de ligne trouvée");
                    }

                    //Fermeture du reader et de la connexion à la base de données
                    reader3.Close();
                    conn.Close();

                }
            }
            return listResult;
        }
    }
}
