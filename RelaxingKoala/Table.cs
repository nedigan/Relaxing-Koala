using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    internal class Table
    {
        private int _fId;
        private OnlineCustomer? _fReserver;
        public bool fIsAvailable { get; set; }
        public int fID { get { return _fId; } }   

        public Table(int aId)
        {
            _fId = aId;
            fIsAvailable = true;
        }

        public void reserveTable(OnlineCustomer aReserver = null)
        {
            // if available, reserve 
            _fReserver = aReserver;
            fIsAvailable = false;

            Console.WriteLine("Table " + _fId + " has been reserved successfully.");

            // else message
        }

        public void freeTable() 
        {
            _fReserver = null;
            fIsAvailable = true;

            Console.WriteLine("Table " + _fId + " has been freed successfully.");
        }
    }
}
