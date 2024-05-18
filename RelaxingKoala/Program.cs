namespace RelaxingKoala
{
    static class Constants
    {
        public const int MAX_TABLES = 10;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            
            // initialising menu
            Menu lMenu = new Menu();
            lMenu.InitialiseMenu(@"..\..\..\MenuItemDB.csv");

            // initialising tables
            List <Table> lTables = new List<Table>();
            // creating 10 tables
            for (int i  = 1; i <= Constants.MAX_TABLES; i++) 
            {
                Table lTable = new Table(i);
                lTables.Add(lTable);
            }   

            // maybe have a list of online customers every time the system is running and that deletes/cleans like the menu does every time ***
            List<OnlineCustomer> lOnlineCustomers = new List<OnlineCustomer>();

            // trying out an order - works
            Order order1 = new Order(4, lTables[1]);
            order1.addItemToOrder(lMenu.fMenu[1], 2);
            order1.payOrder(PaymentType.Card);

            // trying out order with an online customer
            OnlineCustomer oc1 = new OnlineCustomer(7, "Hanbin", "Kim", "347928", "khanbin@in.com");
            lOnlineCustomers.Add(oc1);
            Order order2 = new Order(5, oc1);
            order2.addItemToOrder(lMenu.fMenu[0], 1);
            order2.addItemToOrder(lMenu.fMenu[3], 2);
            order2.payOrder(PaymentType.Cash);

            // trying out reservation with an online customer
            Table lTableToReserve = lTables[2]; 
            OnlineCustomer oc2 = new OnlineCustomer(8, "Jack", "Marsh", "342948", "jackm1298@gmail.com", 4, "2024-05-20 12:00:00");
            lTableToReserve.reserveTable(oc2);
            lOnlineCustomers.Add(oc2);
            lTableToReserve.freeTable();

            // Interface loop

            while (true)
            {
                Console.WriteLine("What would you like to do?\n");

                Console.WriteLine("1. Order a take away meal.");
                Console.WriteLine("2. Make a reservation.");
                Console.WriteLine("3. Kitchen change order status.");
                Console.WriteLine("4. Quit.");

                string? input = Console.ReadLine();
                input = input?.Trim();

                bool isNumber = int.TryParse(input, out int n);
                
                if (isNumber && n > 0 && n < 5)
                {
                    switch (n)
                    {
                        case 1:
                            OrderTakeawayMeal(lMenu);
                            break;
                        case 2:
                            ReserveTable(lTables);
                            break;
                        case 3:
                            ChangeOrderStatus();
                            break;
                        case 4:
                            Console.WriteLine("Goodbye...");
                            return;
                    }
                }
                else
                {
                    Console.WriteLine("Please input a number from 1 - 4.\n");
                }
            }
        }

        static void OrderTakeawayMeal(Menu aMenu)
        {
            Console.WriteLine("Menu: \n");

            for (int i = 0; i < aMenu.fMenu.Count; i++)
            {
                Console.WriteLine($"{i+1}. {aMenu.fMenu[i].fName}");
                Console.WriteLine($"    {aMenu.fMenu[i].fDescription}");
            }

            Console.WriteLine();
            bool valid = false;
            MenuItem menuItem = new MenuItem();
            do
            {
                Console.WriteLine($"What would you like to order? 1 - {aMenu.fMenu.Count}");

                string? input = Console.ReadLine();
                input = input?.Trim();

                bool isNumber = int.TryParse(input, out int n);

                valid = isNumber && n > 0 && n < aMenu.fMenu.Count + 1;
                if (valid)
                {
                    menuItem = aMenu.fMenu[n-1];
                }
            } while (!valid);

            Console.WriteLine();
            Console.WriteLine($"You have selected {menuItem.fName}!");

            Console.WriteLine("Please enter your full name:");
            string? name = Console.ReadLine();

            Console.WriteLine("Please enter your mobile number:");
            string? phoneNumber = Console.ReadLine();

            Console.WriteLine("Please enter your email:");
            string? email = Console.ReadLine();

            

            OnlineCustomer customer = new OnlineCustomer(0, name, phoneNumber, email, DateTime.Now.ToString()); // idk how we doing ids
            Order order = new Order(0, customer); // idk how we doing ids
            order.addItemToOrder(menuItem, 1); // only one menu item for simplicity????

            Console.WriteLine("Press enter to confirm you order and pay.");
            Console.ReadLine();
            Console.WriteLine("Thank you...");

            order.payOrder(PaymentType.Online);
        }
        static void ReserveTable(List<Table> aTables)
        {

        }
        static void ChangeOrderStatus()
        {
            Kitchen kitchen = Kitchen.getInstance();
            
            foreach(Order order in kitchen.fOrders)
            {
                Console.WriteLine(order.fStatus);
            }
        }
    }
}
