using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using Prism.Mvvm;
using ArmyArranger.Global;
using System.Collections.ObjectModel;
using ArmyArranger.Models;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddNationsViewModel : BindableBase
    {
        #region Propeties

        AddNationsModel thisModel = new AddNationsModel();

        private ObservableCollection<Nation> _nationsList;
        public ObservableCollection<Nation> NationsList
        {
            get { return _nationsList; }
            set
            {
                _nationsList = value;
                RaisePropertyChanged(nameof(NationsList));
            }
        }

        private Nation _selectedRule;
        public Nation SelectedNation
        {
            get { return _selectedRule; }
            set
            {
                _selectedRule = value;
                RaisePropertyChanged(nameof(SelectedNation));
            }
        }

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

        private string _confirmButtonText;
        public string ConfirmButtonText
        {
            get { return _confirmButtonText; }
            set
            {
                _confirmButtonText = value;
                RaisePropertyChanged(nameof(ConfirmButtonText));
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand MouseClick { get; set; }
        public ICommand AddNew { get; set; }
        public ICommand Back { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Confirm { get; set; }

        #endregion

        #region Constructors

        public AddNationsViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            AddNew = new DelegateCommand(PrepareToAddNew);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Remove = new DelegateCommand(RemoveSelectedRule);
            Confirm = new DelegateCommand(ConfirmChanges);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            ConfirmButtonText = "Save New";
            thisModel.EmptyNation.LoadAll();
            NationsList = Nation.NationsCollecion;
        }

        private void FunctionOnClick()
        {
            if (thisModel.ChosenEqualsSelected(SelectedNation) && SelectedNation != null)
            {
                Name = SelectedNation.Name;
                ConfirmButtonText = "Update";
            }
        }

        private void PrepareToAddNew()
        {
            Name = "";
            ConfirmButtonText = "Save New";
            thisModel.ClearNations();
            SelectedNation = null;
        }

        private void ChangeViewToEditYourArmies()
        {
            thisModel.EmptyNation.ClearNationsCollecion();
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void RemoveSelectedRule()
        {
            thisModel.RemoveNation(SelectedNation);
            PrepareToAddNew();
        }

        private void ConfirmChanges()
        {
            thisModel.ConfirmChanges(Name, SelectedNation, NationsList);
            if(SelectedNation != null)
                Name = SelectedNation.Name;
            else
                PrepareToAddNew();
        }

        #endregion
    }
}
