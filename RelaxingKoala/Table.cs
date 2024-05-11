using System;
using System.Collections.Generic;
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

        public Table(int aId, OnlineCustomer? reserver = null)
        {
            _fId = aId;
            _fReserver = reserver;
            fIsAvailable = true;
        }

        public void reserveTable(OnlineCustomer aReserver = null)
        {
            _fReserver = aReserver;
            fIsAvailable = false;  
        }

        public void freeTable() 
        {
            _fReserver = null;
            fIsAvailable = true;
        }
    }
}
