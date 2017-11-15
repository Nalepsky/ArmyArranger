using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using System.Data.SQLite;
using System;

namespace ArmyArranger.ViewModels
{
    public class AddUnitsViewModel : BindableBase
    {
        
        #region Propeties

        private string _result;
        public string Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged(nameof(Result));
            }
        }


        private readonly string _defaultValue1 = "--";
        private string _value1;
        public string Value1
        {
            get
            {
                return !string.IsNullOrEmpty(_value1) ? _value1 : _defaultValue1;
            }
            set
            {
                _value1 = (value == string.Empty) ? _defaultValue1 : value;
                RaisePropertyChanged(nameof(Value1));
            }
        }


        private readonly string _defaultValue2 = "--";
        private string _value2;
        public string Value2
        {
            get
            {
                return !string.IsNullOrEmpty(_value2) ? _value2 : _defaultValue2;
            }
            set
            {
                _value2 = (value == string.Empty) ? _defaultValue2 : value;
                RaisePropertyChanged(nameof(Value2));
            }
        }

        #endregion

        #region Commands

        public ICommand ValidateCommand { get; set; }
        public ICommand SaveData { get; set; }

        #endregion

        #region Constructors

        public AddUnitsViewModel()
        {
            ValidateCommand = new DelegateCommand(ValidateAction);

            SaveData = new DelegateCommand(SaveToDB);
        }

        #endregion

        #region Actions

        private void ValidateAction()
        {
            bool validationResult = (Value1 != _defaultValue1) && (Value2 != _defaultValue2);
            if (!validationResult)
            {
                Result = "Values are empty";
                return;
            }
            Result = "You set val1=" + Value1 + " val2=" + Value2;
        }

        private void SaveToDB()
        {

            SQLiteConnection sqlconnect = new SQLiteConnection(string.Format("Data Source={0}", System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testbase.db")));
            SQLiteCommand command;
            string query = "";

            sqlconnect.Open();
                if (sqlconnect.State == System.Data.ConnectionState.Open)
                {
                    try
                    {
                        query = string.Format("CREATE TABLE test(val1 varchar(100), val2 varchar(100))");
                        command = new SQLiteCommand(query, sqlconnect);
                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return;
                    }

                    MessageBox.Show("Created");
                }
                else
                {
                    MessageBox.Show("Database connection error.");
                }
                sqlconnect.Close();
        }

        #endregion
    }
}
