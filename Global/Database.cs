using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyArranger.Global
{
    static class Database
    {
        static SQLiteConnection sqlconnect = new SQLiteConnection(string.Format("Data Source={0}", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testbase.db")));
        static SQLiteCommand command;

        static public void Connect()
        {
            try
            {
                sqlconnect.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        static public void Close()
        {
            sqlconnect.Close();
        }


        static public void ExecuteCommand(string query)
        {
            if (sqlconnect.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    command = new SQLiteCommand(query, sqlconnect);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                MessageBox.Show("Command Executed");
            }
            else
            {
                MessageBox.Show("Database connection error.");
            }

        }
    }
}
