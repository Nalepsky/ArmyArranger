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
        static SQLiteDataReader result;

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


            try
            {
                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Weapon(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE," +
                    "Name VARCHAR(50) NOT NULL," +
                    "Range INTEGER NOT NULL," +
                    "Shots INTEGER NOT NULL," +
                    "Penetration INTEGER NULL," +
                    "RequiresLoader BIT NOT NULL" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Unit(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE," +
                    "Name INTEGER NOT NULL," +
                    "Type VARCHAR(50) NOT NULL," +
                    "Composition VARCHAR(50) NOT NULL," +
                    "Experience INTEGER NOT NULL," +
                    "EquipmentDescription VARCHAR(255) NULL," +
                    "Defence INTEGER NULL," +
                    "BasePoints INTEGER NULL," +
                    "NationID INTEGER NOT NULL," +
                    "FOREIGN KEY(NationID) REFERENCES Nation(ID)" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Selector(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE," +
                    "Name VARCHAR(50) NOT NULL," +
                    "Date DATE NOT NULL," +
                    "Mandatory VARCHAR(255) NULL," +
                    "Headquarters VARCHAR(255) NULL," +
                    "Infantry VARCHAR(255) NULL," +
                    "ArmouredCars VARCHAR(255) NULL," +
                    "Artilery VARCHAR(255) NULL," +
                    "Tanks VARCHAR(255) NULL," +
                    "Transport VARCHAR(255) NULL," +
                    "NationID INTEGER NOT NULL," +
                    "FOREIGN KEY(NationID) REFERENCES Nation(ID)" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Rule(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE," +
                    "Name VARCHAR(50) NOT NULL," +
                    "Description VARCHAR(255) NOT NULL," +
                    "Type VARCHAR(50) NOT NULL," +
                    "Source VARCHAR(50) NULL" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Option(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE," +
                    "Description VARCHAR(255) NOT NULL," +
                    "Cost INTEGER NULL," +
                    "Count INTEGER NULL," +
                    "WeaponID INTEGER NULL," +
                    "UnitID INTEGER NULL," +
                    "FOREIGN KEY(WeaponID) REFERENCES Weapon(ID)," +
                    "FOREIGN KEY(UnitID) REFERENCES Unit(ID)" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Nation(" +
                    "ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE," +
                    "Name VARCHAR(50) NOT NULL" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Unit_Weapon(" +
                    "UnitID INTEGER NOT NULL," +
                    "WeaponID INTEGER NOT NULL," +
                    "FOREIGN KEY(UnitID) REFERENCES Unit(ID)" +
                    "FOREIGN KEY(WeaponID) REFERENCES Weapon(ID)" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Rule_Unit(" +
                    "RuleID INTEGER NOT NULL," +
                    "UnitID INTEGER NOT NULL," +
                    "FOREIGN KEY(RuleID) REFERENCES Rule(ID)" +
                    "FOREIGN KEY(UnitID) REFERENCES Unit(ID)" +                 
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Rule_Nation(" +
                    "RuleID INTEGER NOT NULL," +
                    "NationID INTEGER NOT NULL," +
                    "FOREIGN KEY(RuleID) REFERENCES Rule(ID)" +
                    "FOREIGN KEY(NationID) REFERENCES Nation(ID)" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Rule_Selector(" +
                    "RuleID INTEGER NOT NULL," +
                    "SelectorID INTEGER NOT NULL," +
                    "FOREIGN KEY(RuleID) REFERENCES Rule(ID)" +
                    "FOREIGN KEY(SelectorID) REFERENCES Selector(ID)" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Rule_Option(" +
                    "RuleID INTEGER NOT NULL," +
                    "OptionID INTEGER NOT NULL," +
                    "FOREIGN KEY(RuleID) REFERENCES Rule(ID)" +
                    "FOREIGN KEY(OptionID) REFERENCES Option(ID)" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();

                command = new SQLiteCommand("" +
                    "CREATE TABLE IF NOT EXISTS Rule_Weapon(" +
                    "RuleID INTEGER NOT NULL," +
                    "WeaponID INTEGER NOT NULL," +
                    "FOREIGN KEY(RuleID) REFERENCES Rule(ID)" +
                    "FOREIGN KEY(WeaponID) REFERENCES Weapon(ID)" +
                    ")", sqlconnect);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }


        static public void Close()
        {
            sqlconnect.Close();
        }


        static public SQLiteDataReader ExecuteCommand(string query)
        {
            try
            {
                command = new SQLiteCommand(query, sqlconnect);
                result = command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
