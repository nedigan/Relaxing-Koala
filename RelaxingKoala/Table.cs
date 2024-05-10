using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    internal class Table
    {
        private int _id;
        private OnlineCustomer? _reserver;
        public int ID { get { return _id; } }   
        public bool IsAvailable { get { return _reserver != null; } }

        public Table(int aId, OnlineCustomer? reserver = null)
        {
            _id = aId;
            _reserver = reserver;
        }

        public void reserveTable(OnlineCustomer reserver)
        {
            _reserver = reserver;
            // Do something else?
        }
    }
}
