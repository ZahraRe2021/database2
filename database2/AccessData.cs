using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace database2
{

    public class AccessData
    {
        public static string connString(string name  )
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }
      

        public static string connSQLString(string name= "UserItem")
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        //public static string connSQLString(string name = "UserItem")
        //{
        //    SqlConnection con = new SqlConnection();
        //    con.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";
        //    con.Open();
        //}

      public string sql = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\114\source\repos\database2\database2\Mock_Data.mdf;Integrated Security=True";

    }
}
