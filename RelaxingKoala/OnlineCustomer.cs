using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    internal class OnlineCustomer
    {
        public int fID { get; private set; }
        public string fFullName {  get; private set; }
        public string fPhoneNumber {  get; private set; }
        public string fEmail {  get; private set; }
        public int fTableReservedID { get; private set; }
        public int fOrderID { get; private set; }

        public OnlineCustomer(int aId, string aFullName, string aPhoneNumber, string aEmail)
        {
            fID = aId;
            fFullName = aFullName;
            fPhoneNumber = aPhoneNumber;
            fEmail = aEmail;
        }

        // another constructor with just the table reserved ID -- if not null maybe -1 

        // another constructor with just online customer -- if not null maybe -1

        // function 2 - update database

        // function 3 - self destruct (can be in main or here, need to check)
    }
}
