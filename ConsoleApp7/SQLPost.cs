using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp7
{
    class SQLPost
    {
        ConnectDB db = new ConnectDB();
        public void commandUpdate(string text, Dictionary<string, object> parametr)
        {
            DataTable table = new DataTable();
            SqlCommand commandUpdate = new SqlCommand(text, db.getConnection());
            foreach (KeyValuePair<string, object> item in parametr)
            {
                if (item.Value != null)
                {
                    //commandUpdate.Parameters.Add(item.Key, typeParam).Value = item.Value;
                    commandUpdate.Parameters.AddWithValue(item.Key, item.Value);
                }
                else
                {
                    commandUpdate.Parameters.AddWithValue(item.Key, DBNull.Value);
                }
            }
            db.openconnection();
            try
            {
                if (commandUpdate.ExecuteNonQuery() == 1)
                {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.UpdateCommand = commandUpdate;
                    adapter.Update(table);
                }
            }
            catch
            {
                return;
            }
            db.closeconnction();
        }
        public void commandInsertDelete(string text, Dictionary<string, object> parametr)
        {
            SqlCommand command = new SqlCommand(text, db.getConnection());
            foreach (KeyValuePair<string, object> item in parametr)
            {
                if (item.Value != null)
                {
                    command.Parameters.AddWithValue(item.Key, item.Value);
                }
                else
                {
                    command.Parameters.AddWithValue(item.Key, DBNull.Value);
                }
            }
            db.openconnection();
            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                return;
            }
            db.closeconnction();
        }
        public DataTable commandSelect(string text, Dictionary<string, object> parametr)
        {
            DataTable table = new DataTable();
            SqlCommand commandSelect = new SqlCommand(text, db.getConnection());
            foreach (KeyValuePair<string, object> item in parametr)
            {
                if (item.Value != null)
                {
                    commandSelect.Parameters.AddWithValue(item.Key, item.Value);
                }
                else
                {
                    commandSelect.Parameters.AddWithValue(item.Key, DBNull.Value);
                }
            }
            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = commandSelect;
            adapter.Fill(table);
            return table;
        }
      
    }
}