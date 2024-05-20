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
        public int fReferenceNumber {  get; private set; }
        public OnlineCustomer? fOnlineCustomer { get; private set; }
        public Table? fTable { get; private set; }
        public bool fIsTakeAway { get { return fOnlineCustomer != null; } }
        public List<MenuItem> fItemsOrdered { get; private set; } = new List<MenuItem>();
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
            fItemsOrdered.Add(aItem);

            // updating database
            _addOrderItemToDB(aItem.fID, aQuantity);
        }

        public void payOrder(PaymentType aType)
        {
            Payment lPayment = new Payment();
            float lAmount = 0;
            foreach (MenuItem lItem in fItemsOrdered)
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
                _addOrderToDB(lAmount, aType.ToString().ToLower());
            }
        }

        private void _addOrderItemToDB(int aMenuItemID, int aQuantity)
        {
            string lNewRecord = $"{fReferenceNumber},{aMenuItemID},{aQuantity}";

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

        private void _addOrderToDB(float aTotal, string aPaymentType)
        {
            string newRecord = $"{fReferenceNumber},{aTotal},{fTable?.fID ?? -1},{fOnlineCustomer?.fID ?? -1},{aPaymentType}";

            try
            {
                using (StreamWriter lSw = new StreamWriter(@"..\..\..\OrderDB.csv", true))
                {
                    lSw.WriteLine(newRecord);
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
