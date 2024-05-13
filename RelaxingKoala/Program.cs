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

            // function 4
            List <Table> lTables = new List<Table>();
            // create 10 tables
            for (int i  = 1; i <= Constants.MAX_TABLES; i++) 
            {
                Table lTable = new Table(i);
                lTables.Add(lTable);
            }

        }
    }
}
