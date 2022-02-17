﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnexionSQL
{
    class DBSQLServerUtils
    {
        public static SqlConnection GetDBConnection(string datasource, string database)
        {
            //
            // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
            //
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                       + database + ";Integrated Security =True";
            //string connString = @"Data Source=pc-dlongueville\sqlexpress;Initial Catalog=EXH_2019_04;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connString);
            return conn;
        }
    }
}

