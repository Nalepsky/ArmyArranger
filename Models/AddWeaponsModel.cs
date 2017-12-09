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
        public Rule EmptyRule = new Rule();
        public Weapon lastChoosenWeapon;
        public Rule lastChoosenRule;


        #endregion

        #region Constructors

        public AddWeaponsModel()
        {

        }

        #endregion

        #region Actions   

        public void AddWeapon(string name, int range, int shots, int penetration, bool requiresLoader, int ruleID)
        {
            try
            {
                EmptyWeapon.CreateNewAndSaveToDB(name, range, shots, penetration, requiresLoader, ruleID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            MessageBox.Show("Successfully added");
        }

        public void UpdateWeapon(Weapon selectedWeapon, string name, int range, int shots, int penetration, bool requiresLoader, int ruleID)
        {
            if (selectedWeapon == null)
                return;


            try
            {
                selectedWeapon.UpdateInDB(name, range, shots, penetration, requiresLoader, ruleID);
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

        public bool ChosenRuleEqualsSelected(Rule selectedRule)
        {
            if (lastChoosenRule != selectedRule)
            {
                lastChoosenRule = selectedRule;
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
            lastChoosenRule = EmptyRule;
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

        public void ConfirmChanges(string name, int range, int shots, int penetration, bool requiresLoader, int ruleID, Weapon selectedWeapon, ObservableCollection<Weapon> weaponsList)
        {
            if (String.IsNullOrWhiteSpace(name))
                return;

            Weapon WeaponWithThisname = weaponsList.FirstOrDefault(weapon => weapon.Name == name);

            if (selectedWeapon == null)
            {
                if (WeaponWithThisname != null)
                {
                    if (PromptQuestion("Weapon '" + name + "' alredy exist, do you want to override it?"))
                        UpdateWeapon(WeaponWithThisname, name, range, shots, penetration, requiresLoader, ruleID);
                }
                else
                {
                    AddWeapon(name, range, shots, penetration, requiresLoader, ruleID);
                }
            }
            else
            {
                if (selectedWeapon.Name == name)
                {
                    UpdateWeapon(selectedWeapon, name, range, shots, penetration, requiresLoader, ruleID);
                }
                else if (WeaponWithThisname != null)
                {
                    if (PromptQuestion("Weapon '" + name + "' alredy exist, do you want to override it?"))
                        UpdateWeapon(WeaponWithThisname, name, range, shots, penetration, requiresLoader, ruleID);
                }
                else
                {
                    if (PromptQuestion("Do you wish to update name of choosen weapon? \nOtherwise new weapon with given name will by added."))
                        UpdateWeapon(selectedWeapon, name, range, shots, penetration, requiresLoader, ruleID);
                    else
                        AddWeapon(name, range, shots, penetration, requiresLoader, ruleID);
                }
            }
        }

        #endregion
    }
}
