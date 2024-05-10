using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    internal struct MenuItem
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public MenuItem(string aName, string aDescription, int aPrice)
        {
            Name = aName;
            Description = aDescription;
            Price = aPrice;
        }
    }
}
