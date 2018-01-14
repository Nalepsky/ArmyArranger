using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    public class ArmyListOption : Option
    {

        public new static ObservableCollection<ArmyListOption> OptionsCollection = new ObservableCollection<ArmyListOption>();
        public Boolean IsChecked { get; set; }

        public ArmyListOption(int id, string description, int count, int cost, int weaponId, int ruleId, int unitID)
        {
            this.ID = id;
            this.Description = description;
            this.Count = count;
            this.Cost = cost;
            this.UnitID = unitID;
            this.WeaponID = weaponId;
            this.RuleID = ruleId;

            OptionsCollection.Add(this);
            IsChecked = false;
        }
    }
}
