using System;
using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using System.Windows;
using Prism.Mvvm;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class Rule
    {
        string Name { get; set; }
        string Describtion { get; set; }
        string Type { get; set; }
        string Source { get; set; }

        public Rule(string name, string source, string type, string describtion)
        {
            Name = name;
            Describtion = describtion;
            Type = type;
            Source = source;
        }

        public string Query()
        {
            return "temporary string";
        }
    }

    class AddRulesViewModel : BindableBase
    {
        #region Propeties

        private Rule NewRule = null;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        private string _source;
        public string Source
        {
            get { return _source; }
            set
            {
                _source = value;
                RaisePropertyChanged(nameof(Source));
            }
        }

        private string _type;
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                RaisePropertyChanged(nameof(Type));
            }
        }

        private string _describtion;
        public string Describtion
        {
            get { return _describtion; }
            set
            {
                _describtion = value;
                RaisePropertyChanged(nameof(Describtion));
            }
        }

        #endregion

        #region Commands

        public ICommand Back { get; set; }
        public ICommand Confirm { get; set; }


        #endregion

        #region Constructors

        public AddRulesViewModel()
        {
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Confirm = new DelegateCommand(ConfirmChanges);
        }

        #endregion

        #region Actions

        private void ChangeViewToEditYourArmies()
        {
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void ConfirmChanges()
        {
            NewRule = new Rule(_name, _source, _type, _describtion);
            string msg = _name + "\n" + _source + "\n" + _type + "\n" + _describtion;
            MessageBox.Show(msg);
        }

        #endregion
    }
}
