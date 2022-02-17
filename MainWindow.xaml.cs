using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
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


namespace ConnexionSQL
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Label1.Content = "Getting Connection ...";
            SqlConnection conn = DBUtils.GetDBConnection();
            try
            {
                Label2.Content = "Openning Connection ...";
                conn.Open();
                Label3.Content = "Connection successful!";
            }
            catch (Exception a)
            {
                Label3.Content = "Error: " + a.Message;
            }
            Console.Read();
            conn.Close();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection conn = DBUtils.GetDBConnection();
            conn.Open();

        }

        private void QuerySQL(SqlConnection conn)
        {
            string requete = "Select * From DONNEES Where Heure = '43565,4625';";
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = conn;
            cmd.CommandText = requete;

            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        int 
                    }
                }
            }
        }
    }
}
