using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    internal struct MenuItem
    {
        public int fID { get; set; }
        public string fName { get; set; }
        public string fDescription { get; set; }
        public float fPrice { get; set; }
        public string[] fAllergens { get; set; }  
        
        public MenuItem(int aID, string aName, string aDescription, float aPrice, string[] aAllergens)
        {
            fID = aID;
            fName = aName;
            fDescription = aDescription;
            fPrice = aPrice;
            fAllergens = aAllergens;
        }
    }
}
