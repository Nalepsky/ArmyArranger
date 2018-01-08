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

        private ObservableCollection<Entry> _headquatersentries;
        public ObservableCollection<Entry> HeadquaetersEntries
        {
            get { return _headquatersentries; }
            set
            {
                _headquatersentries = value;
                RaisePropertyChanged(nameof(HeadquaetersEntries));
            }
        }

        private ObservableCollection<Entry> _infantryentries;
        public ObservableCollection<Entry> InfantryEntries
        {
            get { return _infantryentries; }
            set
            {
                _infantryentries = value;
                RaisePropertyChanged(nameof(InfantryEntries));
            }
        }

        private ObservableCollection<Entry> _artilleryentries;
        public ObservableCollection<Entry> ArtilleryEntries
        {
            get { return _artilleryentries; }
            set
            {
                _artilleryentries = value;
                RaisePropertyChanged(nameof(ArtilleryEntries));
            }
        }

        private ObservableCollection<Entry> _armouredcarsentries;
        public ObservableCollection<Entry> ArmouredCarsEntries
        {
            get { return _armouredcarsentries; }
            set
            {
                _armouredcarsentries = value;
                RaisePropertyChanged(nameof(ArmouredCarsEntries));
            }
        }

        private ObservableCollection<Entry> _tanksentries;
        public ObservableCollection<Entry> TanksEntries
        {
            get { return _tanksentries; }
            set
            {
                _tanksentries = value;
                RaisePropertyChanged(nameof(TanksEntries));
            }
        }

        private ObservableCollection<Entry> _transportsentries;
        public ObservableCollection<Entry> TransportsEntries
        {
            get { return _transportsentries; }
            set
            {
                _transportsentries = value;
                RaisePropertyChanged(nameof(TransportsEntries));
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

        private Entry _selectedHeadquaetersentry;
        public Entry SelectedHeadquaetersEntry
        {
            get { return _selectedHeadquaetersentry; }
            set
            {
                _selectedHeadquaetersentry = value;
                RaisePropertyChanged(nameof(SelectedHeadquaetersEntry));
            }
        }

        private Entry _selectedInfantryentry;
        public Entry SelectedInfantryEntry
        {
            get { return _selectedInfantryentry; }
            set
            {
                _selectedInfantryentry = value;
                RaisePropertyChanged(nameof(SelectedInfantryEntry));
            }
        }

        private Entry _selectedArtilleryentry;
        public Entry SelectedArtilleryEntry
        {
            get { return _selectedArtilleryentry; }
            set
            {
                _selectedArtilleryentry = value;
                RaisePropertyChanged(nameof(SelectedArtilleryEntry));
            }
        }

        private Entry _selectedArmouredCarsentry;
        public Entry SelectedArmouredCarsEntry
        {
            get { return _selectedArmouredCarsentry; }
            set
            {
                _selectedArmouredCarsentry = value;
                RaisePropertyChanged(nameof(SelectedArmouredCarsEntry));
            }
        }

        private Entry _selectedTanksentry;
        public Entry SelectedTanksEntry
        {
            get { return _selectedTanksentry; }
            set
            {
                _selectedTanksentry = value;
                RaisePropertyChanged(nameof(SelectedTanksEntry));
            }
        }

        private Entry _selectedTransportsentry;
        public Entry SelectedTransportsEntry
        {
            get { return _selectedTransportsentry; }
            set
            {
                _selectedTransportsentry = value;
                RaisePropertyChanged(nameof(SelectedTransportsEntry));
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
        public ICommand MouseClick { get; set; }
        public ICommand AddNextMandatory { get; set; }
        public ICommand EditMandatory { get; set; }
        public ICommand AddNextHeadquaeters { get; set; }
        public ICommand EditHeadquaeters { get; set; }
        public ICommand AddNextInfantry { get; set; }
        public ICommand EditInfntry { get; set; }
        public ICommand AddNextArtillery { get; set; }
        public ICommand EditArtillery { get; set; }
        public ICommand AddNextArmouredCars { get; set; }
        public ICommand EditArmouredCars { get; set; }
        public ICommand AddNextTanks { get; set; }
        public ICommand EditTanks { get; set; }
        public ICommand AddNextTransports { get; set; }
        public ICommand EditTransports { get; set; }

        #endregion

        #region Constructors

        public AddSelectorsViewModel()
        {
            OnLoad = new DelegateCommand(FunctionOnLoad);
            MouseClick = new DelegateCommand(FunctionOnClick);
            Back = new DelegateCommand(ChangeViewToEditYourArmies);
            Confirm = new DelegateCommand(Save);

            AddNextMandatory = new DelegateCommand(OpenMandatoryWindow);
            EditMandatory = new DelegateCommand(OpenEditMandatoryWindow);

            AddNextHeadquaeters = new DelegateCommand(OpenHeadquaetersWindow);
            EditHeadquaeters = new DelegateCommand(OpenEditHeadquaetersWindow);

            AddNextInfantry = new DelegateCommand(OpenInfantryWindow);
            EditInfntry = new DelegateCommand(OpenEditInfantryWindow);

            AddNextArtillery = new DelegateCommand(OpenArtilleryWindow);
            EditArtillery = new DelegateCommand(OpenEditArtilleryWindow);

            AddNextArmouredCars = new DelegateCommand(OpenArmouredCarsWindow);
            EditArmouredCars = new DelegateCommand(OpenEditArmouredCarsWindow);

            AddNextTanks = new DelegateCommand(OpenTanksWindow);
            EditTanks = new DelegateCommand(OpenEditTanksWindow);

            AddNextTransports = new DelegateCommand(OpenTransportsWindow);
            EditTransports = new DelegateCommand(OpenEditTransportsWindow);
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

        private void Save()
        {
            thisModel.AddSelector(SelectorName, Year, thisModel.MandatoryString, thisModel.HeadquartersString, thisModel.InfantryString, thisModel.ArmouredCarsString, thisModel.ArtilleryString, thisModel.TanksString, thisModel.TransportsString, SelectedNation.ID);
        }

        private void OpenMandatoryWindow()
        {
            thisModel.EditedEntry = "mandatory";
            Window EntryWindow = new AddEntryWindow(service);
            service.AddSubscriber(this);
            EntryWindow.Show();            
        }
        private void OpenEditMandatoryWindow()
        {
            if(SelectedMandatoryEntry != null)
            {
                Window EntryWindow = new AddEntryWindow(service, SelectedMandatoryEntry);
                service.AddSubscriber(this);
                EntryWindow.Show();
            }            
        }

        private void OpenHeadquaetersWindow()
        {
            thisModel.EditedEntry = "headquarters";
            Window EntryWindow = new AddEntryWindow(service);
            service.AddSubscriber(this);
            EntryWindow.Show();
        }
        private void OpenEditHeadquaetersWindow()
        {
            if (SelectedMandatoryEntry != null)
            {
                Window EntryWindow = new AddEntryWindow(service, SelectedHeadquaetersEntry);
                service.AddSubscriber(this);
                EntryWindow.Show();
            }
        }

        private void OpenInfantryWindow()
        {
            thisModel.EditedEntry = "infantry";
            Window EntryWindow = new AddEntryWindow(service);
            service.AddSubscriber(this);
            EntryWindow.Show();
        }
        private void OpenEditInfantryWindow()
        {
            if (SelectedMandatoryEntry != null)
            {
                Window EntryWindow = new AddEntryWindow(service, SelectedInfantryEntry);
                service.AddSubscriber(this);
                EntryWindow.Show();
            }
        }

        private void OpenArtilleryWindow()
        {
            thisModel.EditedEntry = "artillery";
            Window EntryWindow = new AddEntryWindow(service);
            service.AddSubscriber(this);
            EntryWindow.Show();
        }
        private void OpenEditArtilleryWindow()
        {
            if (SelectedMandatoryEntry != null)
            {
                Window EntryWindow = new AddEntryWindow(service, SelectedArtilleryEntry);
                service.AddSubscriber(this);
                EntryWindow.Show();
            }
        }

        private void OpenArmouredCarsWindow()
        {
            thisModel.EditedEntry = "armouredcars";
            Window EntryWindow = new AddEntryWindow(service);
            service.AddSubscriber(this);
            EntryWindow.Show();
        }
        private void OpenEditArmouredCarsWindow()
        {
            if (SelectedMandatoryEntry != null)
            {
                Window EntryWindow = new AddEntryWindow(service, SelectedArmouredCarsEntry);
                service.AddSubscriber(this);
                EntryWindow.Show();
            }
        }

        private void OpenTanksWindow()
        {
            thisModel.EditedEntry = "transport";
            Window EntryWindow = new AddEntryWindow(service);
            service.AddSubscriber(this);
            EntryWindow.Show();
        }
        private void OpenEditTanksWindow()
        {
            if (SelectedMandatoryEntry != null)
            {
                Window EntryWindow = new AddEntryWindow(service, SelectedTanksEntry);
                service.AddSubscriber(this);
                EntryWindow.Show();
            }
        }

        private void OpenTransportsWindow()
        {
            Window EntryWindow = new AddEntryWindow(service);
            service.AddSubscriber(this);
            EntryWindow.Show();
        }
        private void OpenEditTransportsWindow()
        {
            if (SelectedMandatoryEntry != null)
            {
                Window EntryWindow = new AddEntryWindow(service, SelectedTransportsEntry);
                service.AddSubscriber(this);
                EntryWindow.Show();
            }
        }

        public void OnMessageSend(string message)
        {
            Console.WriteLine(message);
            thisModel.AddEntry(message);
            MandatoryEntries = thisModel.GetMandatoryentries();
            HeadquaetersEntries = thisModel.GetHeadquartersentries();
            InfantryEntries = thisModel.GetInfantryentries();
            ArtilleryEntries = thisModel.GetArtilleryentries();
            ArmouredCarsEntries = thisModel.GetArmouredCarsentries();
            TanksEntries = thisModel.GetTanksentries();
            TransportsEntries = thisModel.GetTransportsentries();
        }

        private void FunctionOnClick()
        {
            if (thisModel.ChosenEqualsSelected(SelectedSelector) && SelectedSelector != null)
            {
                thisModel.EmptySelector = SelectedSelector;
                SelectorName = SelectedSelector.Name;
                Year = SelectedSelector.Date;
                Console.WriteLine("id" + SelectedSelector.NationId);                
                SelectedNation = thisModel.getNation(SelectedSelector.NationId, NationsList);

                MandatoryEntries = thisModel.GetMandatoryentries();
                HeadquaetersEntries = thisModel.GetHeadquartersentries();
                InfantryEntries = thisModel.GetInfantryentries();
                ArtilleryEntries = thisModel.GetArtilleryentries();
                ArmouredCarsEntries = thisModel.GetArmouredCarsentries();
                TanksEntries = thisModel.GetTanksentries();
                TransportsEntries = thisModel.GetTransportsentries();
            }
            
        }

        #endregion
    }
}