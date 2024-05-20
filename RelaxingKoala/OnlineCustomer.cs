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
        public string fFirstName { get; private set; }
        public string fLastName { get; private set; }
        public string fPhoneNumber {  get; private set; }
        public string fEmail {  get; private set; }

        public OnlineCustomer(int aId, string aFirstName, string aLastName, string aPhoneNumber, string aEmail)
        {
            fID = aId;
            fFirstName = aFirstName;
            fLastName = aLastName;
            fPhoneNumber = aPhoneNumber;
            fEmail = aEmail;

            // updating the database
            _addCustomerToDB();
        }

        private void _addCustomerToDB()
        {
            string newRecord = $"{fID},{fFirstName},{fLastName},{fPhoneNumber},{fEmail}";

            try
            {
                using (StreamWriter lSw = new StreamWriter(@"..\..\..\OnlineCustomerDB.csv", true))
                {
                    lSw.WriteLine(newRecord);
                }
                Console.WriteLine("Online customer successfully added to the CSV file.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to add online customer to the CSV file. Error: {e.Message}");
            }
        }

        // do we need to fetch the same customer or can that be handled by the DB and we just create new entires here? ***
    }
}
