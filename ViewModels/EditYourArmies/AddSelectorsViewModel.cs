using System;
using System.Windows.Input;
using Prism.Commands;
using ArmyArranger.ViewModels.Menu;
using ArmyArranger.Models;
using System.Collections.ObjectModel;
using ArmyArranger.Global;
using Prism.Mvvm;
using ArmyArranger.Views.EditYourArmies;
using System.Windows;

namespace ArmyArranger.ViewModels.EditYourArmies
{
    class AddSelectorsViewModel : BindableBase, IShareString
    {
        #region Propeties

        AddSelectorsModel thisModel = new AddSelectorsModel();
        AddRulesModel rulesModel = new AddRulesModel();
        WindowsService service = new WindowsService();

        private ObservableCollection<Selector> _selectorsList;
        public ObservableCollection<Selector> SelectorsList
        {
            get { return _selectorsList; }
            set
            {
                _selectorsList = value;
                RaisePropertyChanged(nameof(SelectorsList));
            }
        }

        private ObservableCollection<GameRule> _rulesList;
        public ObservableCollection<GameRule> RulesList
        {
            get { return _rulesList; }
            set
            {
                _rulesList = value;
                RaisePropertyChanged(nameof(RulesList));
            }
        }

        private ObservableCollection<Entry> _mandatoryentries;
        public ObservableCollection<Entry> MandatoryEntries
        {
            get { return _mandatoryentries; }
            set
            {
                _mandatoryentries = value;
                RaisePropertyChanged(nameof(MandatoryEntries));
            }
        }

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

        private ObservableCollection<Nation> _nationsComboBox;
        public ObservableCollection<Nation> NationsComboBox
        {
            get { return _nationsComboBox; }
            set
            {
                _nationsComboBox = value;
                RaisePropertyChanged(nameof(NationsComboBox));
            }
        }

        private Selector _selectedselector;
        public Selector SelectedSelector
        {
            get { return _selectedselector; }
            set
            {
                _selectedselector = value;
                RaisePropertyChanged(nameof(SelectedSelector));
            }
        }

        private GameRule _selectedrule;
        public GameRule SelectedRule
        {
            get { return _selectedrule; }
            set
            {
                _selectedrule = value;
                RaisePropertyChanged(nameof(SelectedRule));
            }
        }

        private Entry _selectedMandatoryentry;
        public Entry SelectedMandatoryEntry
        {
            get { return _selectedMandatoryentry; }
            set
            {
                _selectedMandatoryentry = value;
                RaisePropertyChanged(nameof(SelectedMandatoryEntry));
            }
        }

        private Nation _selectednation;
        public Nation SelectedNation
        {
            get { return _selectednation; }
            set
            {
                _selectednation = value;
                RaisePropertyChanged(nameof(SelectedNation));
            }
        }

        private string _ruleName;
        public string RuleName
        {
            get { return _ruleName; }
            set
            {
                _ruleName = value;
                RaisePropertyChanged(nameof(RuleName));
            }
        }

        private string _selectorname;
        public string SelectorName
        {
            get { return _selectorname; }
            set
            {
                _selectorname = value;
                RaisePropertyChanged(nameof(SelectorName));
            }
        }

        private string _year;
        public string Year
        {
            get { return _year; }
            set
            {
                _year = value;
                RaisePropertyChanged(nameof(Year));
            }
        }



        #endregion

        #region Commands

        public ICommand OnLoad { get; set; }
        public ICommand Back { get; set; }
        public ICommand Confirm { get; set; }
        public ICommand AddNextMandatory { get; set; }
        public ICommand EditMandatory { get; set; }
        public ICommand MouseClick { get; set; }

        #endregion

        #region Constructors

        public AddSelectorsViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Confirm = new DelegateCommand(ChangeViewToEditYourArmies);
            AddNextMandatory = new DelegateCommand(OpenEntryWindow);
            EditMandatory = new DelegateCommand(OpenEditEntryWindow);
        }

        #endregion

        #region Actions

        private void FunctionOnLoad()
        {
            //ConfirmButtonText = "Save New";
            thisModel.EmptyNation.LoadAll();
            NationsList = Nation.NationsCollecion;
            NationsComboBox = NationsList;
            thisModel.EmptySelector.LoadAll();            
            SelectorsList = Selector.SelectorsCollection;
            thisModel.EmptyRule.LoadAll("Selector");
            RulesList = GameRule.RulesCollection;
        }

        private void ChangeViewToEditYourArmies()
        {
            thisModel.EmptyNation.ClearNationsCollecion();
            thisModel.EmptyRule.ClearRulesCollection();
            thisModel.EmptySelector.ClearSelectorsCollection();
            
            App.Current.MainWindow.DataContext = new EditYourArmiesViewModel();
        }

        private void OpenEntryWindow()
        {            
            Window EntryWindow = new AddEntryWindow(service);
            service.AddSubscriber(this);
            EntryWindow.Show();            
        }
        private void OpenEditEntryWindow()
        {
            if(SelectedMandatoryEntry != null)
            {
                Window EntryWindow = new AddEntryWindow(service, SelectedMandatoryEntry);
                service.AddSubscriber(this);
                EntryWindow.Show();
            }
            
        }

        public void OnMessageSend(string message)
        {
            Console.WriteLine("wiadomosc"+message);
            thisModel.AddMandatoryEntry(message);
            MandatoryEntries = thisModel.GetMandatoryentries();
        }

        private void FunctionOnClick()
        {
            if (thisModel.ChosenEqualsSelected(SelectedSelector) && SelectedSelector != null)
            {
                thisModel.EmptySelector = SelectedSelector;
                SelectorName = SelectedSelector.Name;
                Year = SelectedSelector.Date;
                //add nation later
                MandatoryEntries = thisModel.GetMandatoryentries();
            }
            
        }

        #endregion
    }
}
