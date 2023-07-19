using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class ConnectDB
    {
        SqlConnection connection = new SqlConnection(@"Data Source = LAPTOP-3UNFJENB; Initial Catalog = Tg_bot_students; Integrated Security = True; TrustServerCertificate=True");
        public void openconnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void closeconnction()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection getConnection()
        {
            return connection;
        }
    }

}

