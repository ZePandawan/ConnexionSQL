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
        public static SqlConnection GetDBConnection(string database)
        {
            string datasource = @"pc-dlongueville\sqlexpress";
            return DBSQLServerUtils.GetDBConnection(datasource, database);
        }
    }
}
