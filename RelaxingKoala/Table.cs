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
        //private OnlineCustomer? _fReserver;
        public Dictionary<DateTime, OnlineCustomer> fReservedDates {  get; private set; } = new Dictionary<DateTime, OnlineCustomer>();
        public bool fIsAvailable { get; set; }
        public int fID { get { return _fId; } }   

        public Table(int aId)
        {
            _fId = aId;
            fIsAvailable = true;
        }

        public void reserveTable(DateTime aDateTime, OnlineCustomer aReserver = null)
        {
            // if available, reserve 
            //_fReserver = aReserver;
            fIsAvailable = false;

            fReservedDates[aDateTime] = aReserver;
            _addReservationToDB(aDateTime, aReserver);

            Console.WriteLine("Table " + _fId + " has been reserved successfully.");

            // else message
        }

        public bool IsAvailable(DateTime aDate)
        {
            foreach (var key in fReservedDates.Keys)
            {
                if (key.Date == aDate.Date)
                    return false;
            }
            return true;
        }

        public void freeTable(DateTime aDate) 
        {
            fIsAvailable = true;
            Console.WriteLine("Table " + _fId + " has been freed successfully.");
        }

        private void _addReservationToDB(DateTime aDateTime, OnlineCustomer aReserver = null)
        {
            int lID = aReserver == null ? -1 : aReserver.fID;
            string newRecord = $"{5},{lID},{aDateTime}";

            try
            {
                using (StreamWriter lSw = new StreamWriter(@"..\..\..\ReservationDB.csv", true))
                {
                    lSw.WriteLine(newRecord);
                }
                Console.WriteLine("Reservation successfully added to the CSV file.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to add reservation to the CSV file. Error: {e.Message}");
            }
        }

    }
}
