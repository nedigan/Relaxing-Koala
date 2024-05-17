using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    enum OrderStatus
    {   
        New,
        Paid,
        InProgress,
        Ready
    }
    internal class Order
    {
        private List<MenuItem> _fItemsOrdered = new List<MenuItem>();
        public int fReferenceNumber {  get; private set; }
        public OnlineCustomer? fOnlineCustomer { get; private set; }
        public Table? fTable { get; private set; }
        public bool fIsTakeAway { get { return fOnlineCustomer != null; } }
        public List<MenuItem> fItemsOrdered { get { return _fItemsOrdered; }}
        public OrderStatus fStatus { get; set; }

        public Order(int aReferenceNumber, OnlineCustomer aOnlineCustomer) : this(aReferenceNumber)
        {
            fOnlineCustomer = aOnlineCustomer;
        }

        public Order(int aOrderReferenceNumber, Table aTable): this(aOrderReferenceNumber)
        {
            fTable = aTable;
        }

        private Order(int aReferenceNumber)
        {
            fReferenceNumber = aReferenceNumber;
        }

        public void addItemToOrder(MenuItem aItem, int aQuantity)
        {
            _fItemsOrdered.Add(aItem);

            // updating database
            AddOrderItemToDB(fReferenceNumber, aItem.fID, aQuantity);
        }

        public void payOrder(PaymentType aType)
        {
            Payment lPayment = new Payment();
            float lAmount = 0;
            foreach (MenuItem lItem in _fItemsOrdered)
            {
                lAmount += lItem.fPrice;
            }

            switch(aType)
            {
                case PaymentType.Cash:
                    lPayment.processCashPayment(lAmount);
                    break;
                case PaymentType.Card:
                    lPayment.processEftposPayment(lAmount);
                    break;
                case PaymentType.Online:
                    lPayment.processOnlinePayment(lAmount);
                    break;
            }

            if (lPayment.fPaymentSuccessful)
            {
                Kitchen lKitchen = Kitchen.getInstance();
                lKitchen.addOrder(this);

                fStatus = OrderStatus.Paid;

                // adding to database once payment is successful
                AddOrderToDB(lAmount, aType.ToString().ToLower());
            }
        }

        private void AddOrderItemToDB(int aOrderReference, int aMenuItemID, int aQuantity)
        {
            string lNewRecord = $"{aOrderReference},{aMenuItemID},{aQuantity}";

            try
            {
                using (StreamWriter lSw = new StreamWriter(@"..\..\..\OrderItemDB.csv", true))
                {
                    lSw.WriteLine(lNewRecord);
                }
                Console.WriteLine("Order item successfully added to the CSV file.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to add order item to the CSV file. Error: {e.Message}");
            }
        }

        private void AddOrderToDB(float aTotal, string aPaymentType)
        {
            string newRecord = $"{fReferenceNumber},{aTotal},{fTable?.fID ?? -1},{fOnlineCustomer?.fID ?? -1},{aPaymentType}";

            try
            {
                using (StreamWriter sw = new StreamWriter(@"..\..\..\OrderDB.csv", true))
                {
                    sw.WriteLine(newRecord);
                }
                Console.WriteLine("Order successfully added to the CSV file.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to add order to the CSV file. Error: {e.Message}");
            }
        }

    }
}
