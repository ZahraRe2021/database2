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
        //public static List<imgs> ProductView()
        //{
        //    using (IDbConnection cnn = new SQLiteConnection(connString("Default")))
        //    {
        //        var output = cnn.Query<imgs>("select * from Products", new DynamicParameters()).ToList();
        //            return output;
        //    }
        //}



    }
}
