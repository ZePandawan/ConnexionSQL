using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConnexionSQL
{
    class DBUtils
    {
        public static SqlConnection GetDBConnection()
        {
            string datasource = @"pc-dlongueville\sqlexpress";
            string database = "EXH_2019_04";
            return DBSQLServerUtils.GetDBConnection(datasource, database);
        }
    }
}
