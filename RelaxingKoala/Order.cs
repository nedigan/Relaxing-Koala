using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    enum OrderStatus
    {
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

        public void addItemToOrder(MenuItem aItem)
        {
            _fItemsOrdered.Add(aItem);
            // Do something else?
        }

        public void payOrder(PaymentType aType)
        {
            Payment lPayment = new Payment(0); // create id somehow, currently just setting to 0 --- Maybe store a collection of payments somewhere?
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
            }
        }
    }
}
