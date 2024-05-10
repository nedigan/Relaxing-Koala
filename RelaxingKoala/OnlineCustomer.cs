using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    internal class OnlineCustomer
    {
        public int ID { get; private set; }
        public string FullName {  get; private set; }
        public string PhoneNumber {  get; private set; }
        public string Email {  get; private set; }
        public int TableReservedID { get; private set; }

        public OnlineCustomer(int aId, string aFullName, string aPhoneNumber, string aEmail)
        {
            ID = aId;
            FullName = aFullName;
            PhoneNumber = aPhoneNumber;
            Email = aEmail;
        }
    }
}
