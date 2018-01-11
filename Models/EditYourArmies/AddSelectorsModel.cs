using ArmyArranger.Global;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ArmyArranger.Models
{
    class AddSelectorsModel
    {
        #region Properties

        public Selector EmptySelector = new Selector();
        public GameRule EmptyRule = new GameRule();
        public Nation EmptyNation = new Nation();
        public Selector LastChosenSelector;
        public ObservableCollection<Entry> MandatoryEntriesList = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> HeadquartersEntriesList = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> InfantryEntriesList = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> ArtilleryEntriesList = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> ArmouredCarsEntriesList = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> TanksEntriesList = new ObservableCollection<Entry>();
        public ObservableCollection<Entry> TransportsEntriesList = new ObservableCollection<Entry>();

        public string MandatoryString;
        public string HeadquartersString;
        public string InfantryString;
        public string ArtilleryString;
        public string ArmouredCarsString;
        public string TanksString;
        public string TransportsString;

        public string EditedEntry;

        #endregion

        #region Constructors

        public AddSelectorsModel()
        {
            MandatoryString = EmptySelector.Mandatory;
            HeadquartersString = EmptySelector.Headquarters;
            InfantryString = EmptySelector.Infantry;
            ArtilleryString = EmptySelector.Artillery;
            ArmouredCarsString = EmptySelector.ArmouredCars;
            TanksString = EmptySelector.Tanks;
            TransportsString = EmptySelector.Transport;

            Console.WriteLine(MandatoryString);
            LastChosenSelector = null;
        }

        #endregion

        #region Actions       
        
        public Nation getNation(int NationId, ObservableCollection<Nation> NationsList)
        {
            foreach(var nation in NationsList)
                if (nation.ID == NationId)
                    return nation;
            return null;
        }

        public void AddSelector(string name, string date, string mandatory, string headquarters, string infantry, string armouredCars, string artillery, string tanks, string transport, int nationID)
        {
            try
            {
                EmptySelector.CreateNewAndSaveToDB(name, date, mandatory, headquarters, infantry, armouredCars, artillery, tanks, transport, nationID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void AddEntry(String NewEntry)
        {
            if (EditedEntry == "mandatory")
                EmptySelector.Mandatory += NewEntry;
            else if (EditedEntry == "headquarters")
                EmptySelector.Headquarters += NewEntry;
            else if (EditedEntry == "infantry")
                EmptySelector.Infantry += NewEntry;
            else if (EditedEntry == "artillery")
                EmptySelector.Artillery += NewEntry;
            else if (EditedEntry == "armouredcars")
                EmptySelector.ArmouredCars += NewEntry;
            else if (EditedEntry == "tanks")
                EmptySelector.Tanks += NewEntry;
            else if (EditedEntry == "transport")
                EmptySelector.Transport += NewEntry;
        }        

        public ObservableCollection<Entry> GetMandatoryentries()
        {            
            MandatoryEntriesList.Clear();
            MandatoryString = EmptySelector.Mandatory;

            if (MandatoryString != "" && MandatoryString != null)
            {
                String[] MandatoryEntries = MandatoryString.Split('|');

                foreach (var Entry in MandatoryEntries)
                {
                    if (Entry != "")
                    {
                        String[] Elements = Entry.Split(';');
                        Entry E = new Entry();
                        bool flag = true;
                        bool nameFlag = true;

                        foreach (var El in Elements)
                        {
                            if (flag == true)
                            {
                                flag = false;
                                String[] MinMax = El.Split('-');
                                E.Min = Int32.Parse(MinMax[0]);
                                E.Max = Int32.Parse(MinMax[1]);
                            }
                            else
                            {
                                String[] UnitFlag = El.Split(',');
                                if (nameFlag == true)
                                {
                                    E.Name = UnitFlag[0] + " Mandatory";
                                    nameFlag = false;
                                }

                                Unit newUnit = new Unit(Int32.Parse(UnitFlag[0]));
                                E.UnitList.Add(newUnit);

                                if (UnitFlag[1] == "true")
                                    E.ExcludingUnitList.Add(newUnit);
                            }
                        }
                        MandatoryEntriesList.Add(E);
                    }
                }
            }            
            return MandatoryEntriesList;
        }

        public ObservableCollection<Entry> GetHeadquartersentries()
        {            
            HeadquartersEntriesList.Clear();
            HeadquartersString = EmptySelector.Headquarters;

            if (HeadquartersString != "" && HeadquartersString != null)
            {
                String[] HeadquartersEntries = HeadquartersString.Split('|');

                foreach (var Entry in HeadquartersEntries)
                {
                    if (Entry != "")
                    {
                        String[] Elements = Entry.Split(';');
                        Entry E = new Entry();
                        bool flag = true;
                        bool nameFlag = true;

                        foreach (var El in Elements)
                        {
                            if (flag == true)
                            {
                                flag = false;
                                String[] MinMax = El.Split('-');
                                E.Min = Int32.Parse(MinMax[0]);
                                E.Max = Int32.Parse(MinMax[1]);
                            }
                            else
                            {
                                String[] UnitFlag = El.Split(',');
                                if (nameFlag == true)
                                {
                                    E.Name = UnitFlag[0] + " HQ";
                                    nameFlag = false;
                                }

                                Unit newUnit = new Unit(Int32.Parse(UnitFlag[0]));
                                E.UnitList.Add(newUnit);

                                if (UnitFlag[1] == "true")
                                    E.ExcludingUnitList.Add(newUnit);
                            }
                        }
                        HeadquartersEntriesList.Add(E);
                    }
                }
            }
            return HeadquartersEntriesList;
        }

        public ObservableCollection<Entry> GetInfantryentries()
        {
            
            InfantryEntriesList.Clear();
            InfantryString = EmptySelector.Infantry;

            if (InfantryString != "" && InfantryString != null)
            {
                String[] InfantryEntries = InfantryString.Split('|');

                foreach (var Entry in InfantryEntries)
                {
                    if (Entry != "")
                    {
                        String[] Elements = Entry.Split(';');
                        Entry E = new Entry();
                        bool flag = true;
                        bool nameFlag = true;

                        foreach (var El in Elements)
                        {
                            if (flag == true)
                            {
                                flag = false;
                                String[] MinMax = El.Split('-');
                                E.Min = Int32.Parse(MinMax[0]);
                                E.Max = Int32.Parse(MinMax[1]);
                            }
                            else
                            {
                                String[] UnitFlag = El.Split(',');
                                if (nameFlag == true)
                                {
                                    E.Name = UnitFlag[0] + " Infantry";
                                    nameFlag = false;
                                }

                                Unit newUnit = new Unit(Int32.Parse(UnitFlag[0]));
                                E.UnitList.Add(newUnit);

                                if (UnitFlag[1] == "true")
                                    E.ExcludingUnitList.Add(newUnit);
                            }
                        }
                        InfantryEntriesList.Add(E);
                    }
                }
            }
            return InfantryEntriesList;
        }

        public ObservableCollection<Entry> GetArtilleryentries()
        {            
            ArtilleryEntriesList.Clear();
            ArtilleryString = EmptySelector.Artillery;

            if (ArtilleryString != "" && ArtilleryString != null)
            {
                String[] ArtilleryEntries = ArtilleryString.Split('|');

                foreach (var Entry in ArtilleryEntries)
                {
                    if (Entry != "")
                    {
                        String[] Elements = Entry.Split(';');
                        Entry E = new Entry();
                        bool flag = true;
                        bool nameFlag = true;

                        foreach (var El in Elements)
                        {
                            if (flag == true)
                            {
                                flag = false;
                                String[] MinMax = El.Split('-');
                                E.Min = Int32.Parse(MinMax[0]);
                                E.Max = Int32.Parse(MinMax[1]);
                            }
                            else
                            {
                                String[] UnitFlag = El.Split(',');
                                if (nameFlag == true)
                                {
                                    E.Name = UnitFlag[0] + " Artillery";
                                    nameFlag = false;
                                }

                                Unit newUnit = new Unit(Int32.Parse(UnitFlag[0]));
                                E.UnitList.Add(newUnit);

                                if (UnitFlag[1] == "true")
                                    E.ExcludingUnitList.Add(newUnit);
                            }
                        }
                        ArtilleryEntriesList.Add(E);
                    }
                }
            }
            return ArtilleryEntriesList;
        }

        public ObservableCollection<Entry> GetArmouredCarsentries()
        {
            ArmouredCarsEntriesList.Clear();
            ArmouredCarsString = EmptySelector.ArmouredCars;

            if (ArmouredCarsString != "" && ArmouredCarsString != null)
            {
                String[] ArmouredCarsEntries = ArmouredCarsString.Split('|');

                foreach (var Entry in ArmouredCarsEntries)
                {
                    if (Entry != "")
                    {
                        String[] Elements = Entry.Split(';');
                        Entry E = new Entry();
                        bool flag = true;
                        bool nameFlag = true;

                        foreach (var El in Elements)
                        {
                            if (flag == true)
                            {
                                flag = false;
                                String[] MinMax = El.Split('-');
                                E.Min = Int32.Parse(MinMax[0]);
                                E.Max = Int32.Parse(MinMax[1]);
                            }
                            else
                            {
                                String[] UnitFlag = El.Split(',');
                                if (nameFlag == true)
                                {
                                    E.Name = UnitFlag[0] + " Armoured Cars";
                                    nameFlag = false;
                                }

                                Unit newUnit = new Unit(Int32.Parse(UnitFlag[0]));
                                E.UnitList.Add(newUnit);

                                if (UnitFlag[1] == "true")
                                    E.ExcludingUnitList.Add(newUnit);
                            }
                        }
                        ArmouredCarsEntriesList.Add(E);
                    }
                }
            }
            return ArmouredCarsEntriesList;
        }

        public ObservableCollection<Entry> GetTanksentries()
        {
            TanksEntriesList.Clear();
            TanksString = EmptySelector.Tanks;

            if (TanksString != "" && TanksString != null)
            {
                String[] TanksEntries = TanksString.Split('|');

                foreach (var Entry in TanksEntries)
                {
                    if (Entry != "")
                    {
                        String[] Elements = Entry.Split(';');
                        Entry E = new Entry();
                        bool flag = true;
                        bool nameFlag = true;

                        foreach (var El in Elements)
                        {
                            if (flag == true)
                            {
                                flag = false;
                                String[] MinMax = El.Split('-');
                                E.Min = Int32.Parse(MinMax[0]);
                                E.Max = Int32.Parse(MinMax[1]);
                            }
                            else
                            {
                                String[] UnitFlag = El.Split(',');
                                if (nameFlag == true)
                                {
                                    E.Name = UnitFlag[0] + " Armoured Cars";
                                    nameFlag = false;
                                }

                                Unit newUnit = new Unit(Int32.Parse(UnitFlag[0]));
                                E.UnitList.Add(newUnit);

                                if (UnitFlag[1] == "true")
                                    E.ExcludingUnitList.Add(newUnit);
                            }
                        }
                        TanksEntriesList.Add(E);
                    }
                }
            }
            return TanksEntriesList;
        }

        public ObservableCollection<Entry> GetTransportsentries()
        {
            TransportsEntriesList.Clear();
            TransportsString = EmptySelector.Transport;

            if (TransportsString != "" && TransportsString != null)
            {
                String[] TransportsEntries = TransportsString.Split('|');

                foreach (var Entry in TransportsEntries)
                {
                    if (Entry != "")
                    {
                        String[] Elements = Entry.Split(';');
                        Entry E = new Entry();
                        bool flag = true;
                        bool nameFlag = true;

                        foreach (var El in Elements)
                        {
                            if (flag == true)
                            {
                                flag = false;
                                String[] MinMax = El.Split('-');
                                E.Min = Int32.Parse(MinMax[0]);
                                E.Max = Int32.Parse(MinMax[1]);
                            }
                            else
                            {
                                String[] UnitFlag = El.Split(',');
                                if (nameFlag == true)
                                {
                                    E.Name = UnitFlag[0] + " Armoured Cars";
                                    nameFlag = false;
                                }

                                Unit newUnit = new Unit(Int32.Parse(UnitFlag[0]));
                                E.UnitList.Add(newUnit);

                                if (UnitFlag[1] == "true")
                                    E.ExcludingUnitList.Add(newUnit);
                            }
                        }
                        TransportsEntriesList.Add(E);
                    }
                }
            }
            return TransportsEntriesList;
        }

        public bool ChosenEqualsSelected(Selector SelectedSelector)
        {
            if (LastChosenSelector != SelectedSelector)
            {
                LastChosenSelector = SelectedSelector;
                return true;
            }
            return false;
        }

        #endregion
    }
}