using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRMDesktopUI.Library.Helpers
{
    public class ConfigHelper : IConfigHelper
    {
        public decimal GetTaxRate()
        {

            string rateText = "8.75";
            decimal output = decimal.Parse("8.75");
           

            bool isValidTaxRate = Decimal.TryParse(rateText, out decimal result);

            if (isValidTaxRate == false)
            {
               throw new ConfigurationErrorsException("The tax rate is not set up properly");

            }
            return output;

        }
    }
}
