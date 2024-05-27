using System.Diagnostics.CodeAnalysis;

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
            lMenu.initialiseMenu(@"..\..\..\MenuItemDB.csv");

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
            OnlineCustomer oc2 = new OnlineCustomer(8, "Jack", "Marsh", "342948", "jackm1298@gmail.com");
            lTableToReserve.reserveTable(DateTime.Now, oc2);
            lOnlineCustomers.Add(oc2);
            lTableToReserve.freeTable(DateTime.Now);

            // Interface loop

            while (true)
            {
                Console.WriteLine("What would you like to do?\n");

                Console.WriteLine("1. Order a take away meal.");
                Console.WriteLine("2. Make a reservation.");
                Console.WriteLine("3. Staff book table.");
                Console.WriteLine("4. Kitchen change order status.");
                Console.WriteLine("5. Quit.");

                string? input = Console.ReadLine();
                input = input?.Trim();

                bool isNumber = int.TryParse(input, out int n);
                
                if (isNumber && n > 0 && n < 6)
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
                            StaffSetTable(lTables);
                            break;
                        case 4:
                            ChangeOrderStatus();
                            break;
                        case 5:
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
        private static MenuItem _getMenuItem(Menu aMenu)
        {
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
                    menuItem = aMenu.fMenu[n - 1];
                }
            } while (!valid);
            Console.WriteLine();
            Console.WriteLine($"You have selected {menuItem.fName}!");
            return menuItem;
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
            List<MenuItem> lOrderedItems = new List<MenuItem>();
            while (true)
            {
                lOrderedItems.Add(_getMenuItem(aMenu));
                Console.WriteLine("Would you like to order again? (Y or N)");
                string? input = Console.ReadLine();
                if (input.ToUpper() == "N")
                    break;
            }

            Console.WriteLine("Please enter your first name:");
            string? firstName = Console.ReadLine();

            Console.WriteLine("Please enter you last name:");
            string? lastName = Console.ReadLine();  

            Console.WriteLine("Please enter your mobile number:");
            string? phoneNumber = Console.ReadLine();

            Console.WriteLine("Please enter your email:");
            string? email = Console.ReadLine();

            

            OnlineCustomer customer = new OnlineCustomer(0, firstName, lastName, phoneNumber, email); // idk how we doing ids
            Order order = new Order(0, customer); // idk how we doing ids
            foreach (MenuItem item in lOrderedItems)
            {
                order.addItemToOrder(item, 1); // only one menu item for simplicity????
            }

            Console.WriteLine("Press enter to confirm you order and pay.");
            Console.ReadLine();
            Console.WriteLine("Thank you...");

            order.payOrder(PaymentType.Online);
        }
        static void ReserveTable(List<Table> aTables)
        {
            bool lValid = false;
            int amountOfPeople = 0;
            do
            {
                Console.WriteLine("How many people are you booking for?");
                string? num = Console.ReadLine();
                lValid = int.TryParse(num, out amountOfPeople) && amountOfPeople > 0;
                if (!lValid)
                    Console.WriteLine("Please input a valid number greater than 0");

            } while (!lValid);

            DateTime lDateTime;
            do
            {
                Console.WriteLine("Please enter the date/time for the reservation (yyyy-mm-dd hh:mm:ss)");
                string? lDateTimeText = Console.ReadLine();

                if (DateTime.TryParse(lDateTimeText, out lDateTime))
                    lValid = true;

            } while (!lValid);

            Console.WriteLine();
            
            if (aTables.Count(t => t.IsAvailable(lDateTime)) < Math.Ceiling(amountOfPeople / 4d)){
                Console.WriteLine("Sorry, there are not enough available tables on this date.");
                return;
            }

            Console.WriteLine("Please enter your first name:");
            string? firstName = Console.ReadLine();

            Console.WriteLine("Please enter you last name:");
            string? lastName = Console.ReadLine();

            Console.WriteLine("Please enter your mobile number:");
            string? phoneNumber = Console.ReadLine();

            Console.WriteLine("Please enter your email:");
            string? email = Console.ReadLine();

            OnlineCustomer customer = new OnlineCustomer(0, firstName, lastName, phoneNumber, email);

            int amountOfTablesNeeded = (int)Math.Ceiling(amountOfPeople / 4d);
            foreach (Table lTable in aTables) // gets first available table
            {
                if (lTable.IsAvailable(lDateTime))
                {
                    if (amountOfTablesNeeded > 0)
                    {
                        lTable.reserveTable(lDateTime, customer);
                        amountOfTablesNeeded--;
                    }
                    else
                        break;
                }
            }
        }
        static void ChangeOrderStatus()
        {
            Kitchen kitchen = Kitchen.getInstance();

            if (kitchen.fOrders.Count == 0)
            {
                Console.WriteLine("There are no current orders...");
                return;
            }

            Console.WriteLine("Current Orders:");
            for (int i = 0; i < kitchen.fOrders.Count; i++)
            {
                Order lOrder = kitchen.fOrders[i];
                string lName;
                if (lOrder.fIsTakeAway)
                    lName = lOrder.fOnlineCustomer.fFirstName + " " + lOrder.fOnlineCustomer.fLastName;
                else
                    lName = "Table: " + lOrder.fTable.fID;
                Console.WriteLine($"{i + 1}. {lName}");
                foreach (MenuItem lItem in lOrder.fItemsOrdered)
                {
                    Console.WriteLine($"    {lItem.fName}");
                }
            }

            Console.WriteLine();
            bool lIsValid = false;
            Order lSelectedOrder = null;
            do
            {
                Console.WriteLine($"Which order would you like to update? (1 - {kitchen.fOrders.Count})");

                string? input = Console.ReadLine();
                input = input?.Trim();

                bool isNumber = int.TryParse(input, out int n);
                lIsValid = isNumber && n > 0 && n < kitchen.fOrders.Count + 1;
                if (lIsValid)
                {
                    lSelectedOrder = kitchen.fOrders[n - 1];
                }
            } while (!lIsValid);
            
            Console.WriteLine();
            Console.WriteLine("What status would you like to change this order to?");
            Console.WriteLine("1. In Progress");
            Console.WriteLine("2. Ready");

            lIsValid = false;
            do
            {
                string? input = Console.ReadLine();
                input = input?.Trim();

                bool isNumber = int.TryParse(input, out int n);
                lIsValid = isNumber && n > 0 && n < 3;
                if (lIsValid)
                {
                    if (n == 1)
                        kitchen.setOrderStatus(lSelectedOrder, OrderStatus.InProgress);
                    else if (n == 2)
                        kitchen.setOrderStatus(lSelectedOrder, OrderStatus.Ready);
                }
            } while (!lIsValid);
        }

        static void StaffSetTable(List<Table> aTables)
        {
            int index = 1;
            List<Table> lFreeTables = new List<Table>();
            foreach (Table table in aTables)
            {
                if (table.IsAvailable(DateTime.Now))
                {
                    Console.WriteLine($"{index}. Table {table.fID}");
                    lFreeTables.Add(table);
                    index++;
                }
            }
            if (index - 1 == 0) // no available tables
            {
                Console.WriteLine("Sorry all tables are booked for today...");
                return;
            }
            
            bool lIsValid = false;
            int lTableNum = 0;
            do
            {
                Console.WriteLine($"Which table would you like to book? (1 - {index - 1})");
                string? num = Console.ReadLine();

                lIsValid = int.TryParse(num, out int n) && n > 0 && n < index;
                if (lIsValid)
                    lTableNum = n;
            } while (!lIsValid);

            lFreeTables[lTableNum - 1].reserveTable(DateTime.Now);
        }
    }
}
