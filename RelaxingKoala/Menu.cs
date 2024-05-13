using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RelaxingKoala
{
    internal class Menu
    {
        public List<MenuItem> fMenu = new List<MenuItem>();
        public Menu() 
        {
            fMenu = new List<MenuItem>();
        }

        public void InitialiseMenu(string aFilePath)
        {
            if (File.Exists(aFilePath))
            {
                using (StreamReader lReader = new StreamReader(aFilePath))
                {
                    // skip the header row
                    lReader.ReadLine();

                    while (!lReader.EndOfStream)
                    {
                        var lLine = lReader.ReadLine();
                        // properly handle CSV fields enclosed in quotes
                        var lValues = ParseCsvLine(lLine);

                        if (lValues.Length >= 6)
                        {
                            bool isAvailable = lValues[4].Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase);
                            if (isAvailable)
                            {
                                string name = lValues[1].Trim();
                                string description = lValues[2].Trim();
                                if (float.TryParse(lValues[3].Trim('$', ' '), NumberStyles.Currency, CultureInfo.InvariantCulture, out float price))
                                {
                                    string[] allergens = string.IsNullOrEmpty(lValues[5]) ? new string[0] : lValues[5].Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    MenuItem menuItem = new MenuItem(name, description, price, allergens.Select(a => a.Trim()).ToArray());
                                    fMenu.Add(menuItem);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("File doesn't exist");
            }
        }
        private string[] ParseCsvLine(string line)
        {
            var tokens = new List<string>();
            var currentToken = "";
            bool inQuotes = false;

            foreach (char c in line)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (c == ',' && !inQuotes)
                {
                    tokens.Add(currentToken);
                    currentToken = "";
                }
                else
                {
                    currentToken += c;
                }
            }

            tokens.Add(currentToken);  // add the last token

            return tokens.ToArray();
        }

    }
}
