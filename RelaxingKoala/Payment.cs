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
        public int fID { get; private set; }
        public bool fPaymentSuccessful { get; private set; } = false;

        public Payment()
        {
            fID = _generateRandomID();
        }
        public void processCashPayment(float aPaymentTotal)
        {
            // Do something...
            Console.WriteLine("Processing cash payment...");

            _generateReceipt();

            fPaymentSuccessful = true; // do some checks to see if payment was successful?
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

        private int _generateRandomID()
        {
            Random generator = new Random();
            return generator.Next(0, 1000000);
        }
    }
}
