using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    enum PaymentType
    {
        Cash,
        Card,
        Online
    }
    internal class Payment
    {
        // Fields???
        public int fID { get; private set; }
        public bool fPaymentSuccessful { get; private set; } = false;

        public Payment(int aID)
        {
            fID = aID;
        }
        public float processCashPayment(float aPaymentTotal)
        {
            // Do something...
            Console.WriteLine("Processing cash payment...");

            float lChange = 0f; // Could get change here if we want???

            _generateReceipt();

            fPaymentSuccessful = true; // do some checks to see if payment was successful?

            return lChange;
        }

        public void processEftposPayment(float aPaymentAmount)
        {
            // Do something...
            Console.WriteLine("Processing EFTPOS payment...");
            _generateReceipt();

            fPaymentSuccessful = true; // do some checks to see if payment was successful?
        }

        public void processOnlinePayment(float aPaymentAmount)
        {
            // Do something...
            // Third-party software??
            Console.WriteLine("Processing online payment...");
            _generateReceipt();

            fPaymentSuccessful = true; // do some checks to see if payment was successful?
        }

        public void generateInvoice(Order aOrder)
        {
            Console.WriteLine("Printing invoice...");
        }

        private void _generateReceipt()
        {
            Console.WriteLine("Printing receipt...");
        }
    }
}
