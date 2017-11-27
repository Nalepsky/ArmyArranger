using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArmyArranger.Global
{
    class Selector
    {
        string Name;
        string Date;
        string Mandatory;
        string Headquarters;
        string Infantry;
        string ArmouredCars;
        string Artilery;
        string Tanks;
        string Transport;
        string NationId;

        public Selector(string name, string date, string mandatory, string headquarters, string infantry, string armouredCars, string artilery, string tanks, string transport, string nationId)
        {
            Name = (name != null) ? "'" + name + "'" : "null";
            Date = (date != null) ? "'" + date + "'" : "null";
            Mandatory = (mandatory != null) ? "'" + mandatory + "'" : "null";
            Headquarters = (headquarters != null) ? "'" + headquarters + "'" : "null";
            Infantry = (infantry != null) ? "'" + infantry + "'" : "null";
            ArmouredCars = (armouredCars != null) ? "'" + armouredCars + "'" : "null";
            Artilery = (artilery != null) ? "'" + artilery + "'" : "null";
            Tanks = (tanks != null) ? "'" + tanks + "'" : "null";
            Transport = (transport != null) ? "'" + transport + "'" : "null";
            NationId = (nationId != null) ? "'" + nationId + "'" : "null";
        }
    }
}
