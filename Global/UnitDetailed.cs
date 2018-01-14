using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.SQLite;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace ArmyArranger.Global
{
    public class UnitDetailed : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged<T>([CallerMemberName]string caller = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
        }

        public int ID { get; set; }
        private string _name;
        public string Name {
            get { return _name; }
            set { _name = value; OnPropertyChanged<Unit>(); }
        }
        public int NationID { get; set; }
        public string Type { get; set; }
        public string Composition { get; set; }
        public string WeaponDescription { get; set; }
        public int ArmourClass { get; set; }
        public int Inexperienced { get; set; }
        public int Regular { get; set; }
        public int Veteran { get; set; }
        public int PointsInexp { get; set; }
        public int PointsReg { get; set; }
        public int PointsVet { get; set; }
        public int BaseSize { get; set; }
        public int MaxSize { get; set; }

        public bool OnlyOne { get; set; }
        private string _color;
        public string Color
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged<Unit>(); }
        }

        public bool isEmpty;
        public List<int> ListOfActiveRules = new List<int>();
        public List<int> ListOfActiveWeapons = new List<int>();
        public ObservableCollection<ArmyListOption> ListOfOptions = new ObservableCollection<ArmyListOption>();


        public ICommand OnClick { get; set; }

        public UnitDetailed()
        {
            isEmpty = true;
            OnClick = new DelegateCommand(FunctionOnClick);
            
        }





        private void FunctionOnClick()
        {
            Window OptionsWindow = new SelectorUnitWindow(this);
            OptionsWindow.Show();
            this.Color = "pink";
        }







        public static ObservableCollection<UnitDetailed> AllUnitsCollection = new ObservableCollection<UnitDetailed>();

        public static ObservableCollection<UnitDetailed> LoadFromStringToCollection(string loadString)
        {
            ObservableCollection<UnitDetailed> UnitsCollection = new ObservableCollection<UnitDetailed>();
            string[] UnitsIDAndFlags = loadString.Split(new char[] { ';' });
            foreach (string unitsIDAndFlags in UnitsIDAndFlags)
            {
                string[] UnitIDAndFlag = unitsIDAndFlags.Split(new char[] { ',' });

                bool t_OnlyOne = (UnitIDAndFlag[1] == "true") ? true : false;

                UnitsCollection.Add(GetUnitDetailedInfoFromDBByID(Convert.ToInt32(UnitIDAndFlag[0]), t_OnlyOne));
            }
            return UnitsCollection;
        }

        public static UnitDetailed GetUnitDetailedInfoFromDBByID(int t_ID, bool t_OnlyOne)
        {
            string t_Name = null,
                t_Type = null,
                t_Composition = null,
                t_WeaponDescription = null;

            int t_NationID = 0,
                t_Inexperienced = 0,
                t_Regular = 0,
                t_Veteran = 0,
                t_PointsInexp = 0,
                t_PointsReg = 0,
                t_PointsVet = 0,
                t_BaseSize = 0,
                t_MaxSize = 0,
                t_ArmourClass = 0;

            List<int> t_ListOfActiveRules = new List<int>();
            List<int> t_ListOfActiveWeapons = new List<int>();
            ObservableCollection<ArmyListOption> t_ListOfOptions = new ObservableCollection<ArmyListOption>();

            SQLiteDataReader result = Database.ExecuteCommand("SELECT * FROM Unit WHERE ID = " + t_ID);
            while (result.Read())
            {
                t_ID = result.GetInt32(0);
                t_Name = result.GetString(1);
                t_NationID = (!result.IsDBNull(2)) ? result.GetInt32(2) : 0;
                t_Type = (!result.IsDBNull(3)) ? result.GetString(3) : "";
                t_WeaponDescription = (!result.IsDBNull(4)) ? result.GetString(4) : "";
                t_Composition = (!result.IsDBNull(5)) ? result.GetString(5) : "";
                t_Inexperienced = (!result.IsDBNull(6)) ? result.GetInt32(6) : 0;
                t_Regular = (!result.IsDBNull(7)) ? result.GetInt32(7) : 0;
                t_Veteran = (!result.IsDBNull(8)) ? result.GetInt32(8) : 0;
                t_PointsInexp = (!result.IsDBNull(9)) ? result.GetInt32(9) : 0;
                t_PointsReg = (!result.IsDBNull(10)) ? result.GetInt32(10) : 0;
                t_PointsVet = (!result.IsDBNull(11)) ? result.GetInt32(11) : 0;
                t_BaseSize = (!result.IsDBNull(12)) ? result.GetInt32(12) : 0;
                t_MaxSize = (!result.IsDBNull(13)) ? result.GetInt32(13) : 0;
                t_ArmourClass = (!result.IsDBNull(14)) ? result.GetInt32(14) : 0;
            }

            SQLiteDataReader ruleResult = Database.ExecuteCommand("SELECT RuleID FROM Rule_Unit WHERE UnitID = " + t_ID);
            while (ruleResult.Read())
            {
                t_ListOfActiveRules.Add(ruleResult.GetInt32(0));
            }

            SQLiteDataReader weaponResult = Database.ExecuteCommand("SELECT WeaponID FROM Unit_Weapon WHERE UnitID = " + t_ID);
            while (weaponResult.Read())
            {
                t_ListOfActiveWeapons.Add(weaponResult.GetInt32(0));
            }

            SQLiteDataReader OptionResult = Database.ExecuteCommand("SELECT * FROM Option WHERE UnitID = " + t_ID);
            while (OptionResult.Read())
            {
                int o_ID = OptionResult.GetInt32(0);
                string o_Description = OptionResult.GetString(1);
                int o_Cost = OptionResult.GetInt32(2),
                    o_Count = OptionResult.GetInt32(3),
                    o_WeaponID = (!OptionResult.IsDBNull(4)) ? OptionResult.GetInt32(4) : 0,
                    o_RuleID = (!OptionResult.IsDBNull(5)) ? OptionResult.GetInt32(5) : 0;



                t_ListOfOptions.Add(new ArmyListOption(o_ID, o_Description, o_Count, o_Cost, o_WeaponID, o_RuleID, t_ID));
            }

            UnitDetailed newUnit = new UnitDetailed
            {
                ID = t_ID,
                Name = t_Name,
                NationID = t_NationID,
                Type = t_Type,
                Composition = t_Composition,
                WeaponDescription = t_WeaponDescription,
                ArmourClass = t_ArmourClass,
                Inexperienced = t_Inexperienced,
                Regular = t_Regular,
                Veteran = t_Veteran,
                PointsInexp = t_PointsInexp,
                PointsReg = t_PointsReg,
                PointsVet = t_PointsVet,
                BaseSize = t_BaseSize,
                MaxSize = t_MaxSize,

                OnlyOne = t_OnlyOne,
                Color = "white",

                isEmpty = false,
                ListOfActiveRules = t_ListOfActiveRules,
                ListOfActiveWeapons = t_ListOfActiveWeapons,
                ListOfOptions = t_ListOfOptions
            };
            AllUnitsCollection.Add(newUnit);
            return newUnit;
        }
    }
}
