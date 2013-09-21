using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyKitchen.Database;

namespace MyKitchen.Controllers
{
    public class DatabaseController : Controller
    {
        protected readonly KitchenDatabase Database;

        protected DatabaseController()
        {
            Database = new KitchenDatabase(GetConnection());
        }

        private static DbConnection GetConnection()
        {
            DbConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["MyKitchenDatabaseConnectionString"].ToString());

            try
            {
                connection.Open();
                return connection;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
