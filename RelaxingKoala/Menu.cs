using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RelaxingKoala
{
    internal class Menu
    {
        public List<MenuItem> fMenu = new List<MenuItem>();
        public Menu() 
        {
            fMenu = new List<MenuItem>();
        }

        public void initialiseMenu(string aFilePath)
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
                        var lValues = _parseCsvLine(lLine);

                        if (lValues.Length >= 6)
                        {
                            bool isAvailable = lValues[4].Trim().Equals("Yes", StringComparison.OrdinalIgnoreCase);
                            if (isAvailable)
                            {
                                if (int.TryParse(lValues[0].Trim(), out int lID) &&
                                    float.TryParse(lValues[3].Trim('$', ' '), NumberStyles.Currency, CultureInfo.InvariantCulture, out float lPrice))
                                {
                                    string lName = lValues[1].Trim();
                                    string lDescription = lValues[2].Trim();
                                    string[] lAllergens = string.IsNullOrEmpty(lValues[5]) ? new string[0] : lValues[5].Trim().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                    MenuItem lMenuItem = new MenuItem(lID, lName, lDescription, lPrice, lAllergens.Select(a => a.Trim()).ToArray());
                                    fMenu.Add(lMenuItem);
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

        private string[] _parseCsvLine(string aLine)
        {
            var lTokens = new List<string>();
            var lCurrentToken = "";
            bool lInQuotes = false;

            foreach (char c in aLine)
            {
                if (c == '"')
                {
                    lInQuotes = !lInQuotes;
                }
                else if (c == ',' && !lInQuotes)
                {
                    lTokens.Add(lCurrentToken);
                    lCurrentToken = "";
                }
                else
                {
                    lCurrentToken += c;
                }
            }

            lTokens.Add(lCurrentToken);  // add the last token

            return lTokens.ToArray();
        }

    }
}
