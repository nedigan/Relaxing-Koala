using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    internal class Kitchen
    {
        private static Kitchen _fInstance;
        public List<Order> fOrders {  get; private set; } = new List<Order>();
        private Kitchen() { }

        // Singleton pattern
        public static Kitchen getInstance()
        {
            if (_fInstance == null)
                _fInstance = new Kitchen();
            return _fInstance;
        }

        public void addOrder(Order aOrder)
        {
            fOrders.Add(aOrder);
        }

        public void setOrderStatus(Order aOrder, OrderStatus aStatus)
        {
            if (fOrders.Contains(aOrder))
            {
                aOrder.fStatus = aStatus;
            }
            else
            {
                Console.WriteLine("Kitchen has not recieved order with Reference Number: " +  aOrder.fReferenceNumber);
            }
        }
    }
}
