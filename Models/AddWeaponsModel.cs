using ArmyArranger.Global;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;



namespace ArmyArranger.Models
{
    class AddWeaponsModel
    {
        #region Properties

        public Weapon EmptyWeapon = new Weapon();
        public GameRule EmptyRule = new GameRule();
        public Weapon lastChoosenWeapon;

        #endregion

        #region Constructors

        public AddWeaponsModel()
        {

        }

        #endregion

        #region Actions   

        public void AddWeapon(string name, int range, int shots, int penetration, bool requiresLoader, ObservableCollection<GameRule> rulesList)
        {
            try
            {
                EmptyWeapon.CreateNewAndSaveToDB(name, range, shots, penetration, requiresLoader, rulesList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void UpdateWeapon(Weapon selectedWeapon, string name, int range, int shots, int penetration, bool requiresLoader, ObservableCollection<GameRule> rulesList)
        {
            if (selectedWeapon == null)
                return;


            try
            {
                selectedWeapon.UpdateInDB(name, range, shots, penetration, requiresLoader, rulesList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully updated");
        }

        public void RemoveWeapon(Weapon selectedWeapon)
        {
            if (selectedWeapon == null)
                return;

            try
            {
                selectedWeapon.Remove();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully removed");
        }


        public bool ChosenWeaponEqualsSelected(Weapon selectedWeapon)
        {
            if (lastChoosenWeapon != selectedWeapon)
            {
                lastChoosenWeapon = selectedWeapon;
                return true;
            }
            return false;
        }


        public void ClearWeapons()
        {
            lastChoosenWeapon = EmptyWeapon;
        }

        public void ClearRules()
        {
            foreach (GameRule tempRule in GameRule.RulesCollection)
            {
                tempRule.IsSelected = false;
            }
        }

        public void CheckActiveRules(Weapon selectedWeapon)
        {
            Console.WriteLine(GameRule.RulesCollection.Count);
            foreach (GameRule tempRule in GameRule.RulesCollection)
            {
                foreach ( int dupa in selectedWeapon.ListOfActiveRules)
                {
                    Console.WriteLine(dupa);
                }
                if (selectedWeapon.ListOfActiveRules.Contains(tempRule.ID))
                {
                    tempRule.IsSelected = true;
                    Console.WriteLine("jj");
                }
                    
                else{
                    tempRule.IsSelected = false;
                    Console.WriteLine("kk");
                }
                    
            }
        }

        public bool PromptQuestion(string question)
        {
            switch (MessageBox.Show(question, "Save", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    return true;
                case MessageBoxResult.No:
                    return false;
                default:
                    return false;
            }

        }

        public void ConfirmChanges(string name, int range, int shots, int penetration, bool requiresLoader, Weapon selectedWeapon, ObservableCollection<Weapon> weaponsList, ObservableCollection<GameRule> selectedRulesList)
        {
            if (String.IsNullOrWhiteSpace(name))
                return;

            Weapon WeaponWithThisname = weaponsList.FirstOrDefault(weapon => weapon.Name == name);

            if (selectedWeapon == null)
            {
                if (WeaponWithThisname != null)
                {
                    if (PromptQuestion("Weapon '" + name + "' alredy exist, do you want to override it?"))
                        UpdateWeapon(WeaponWithThisname, name, range, shots, penetration, requiresLoader, selectedRulesList);
                }
                else
                {
                    AddWeapon(name, range, shots, penetration, requiresLoader, selectedRulesList);
                }
            }
            else
            {
                if (selectedWeapon.Name == name)
                {
                    UpdateWeapon(selectedWeapon, name, range, shots, penetration, requiresLoader, selectedRulesList);
                }
                else if (WeaponWithThisname != null)
                {
                    if (PromptQuestion("Weapon '" + name + "' alredy exist, do you want to override it?"))
                        UpdateWeapon(WeaponWithThisname, name, range, shots, penetration, requiresLoader, selectedRulesList);
                }
                else
                {
                    if (PromptQuestion("Do you wish to update name of choosen weapon? \nOtherwise new weapon with given name will by added."))
                        UpdateWeapon(selectedWeapon, name, range, shots, penetration, requiresLoader, selectedRulesList);
                    else
                        AddWeapon(name, range, shots, penetration, requiresLoader, selectedRulesList);
                }
            }
        }

        #endregion
    }
}
