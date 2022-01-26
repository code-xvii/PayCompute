using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implementations
{
    public class TaxService : ITaxService
    {
        private decimal taxRate;
        private decimal tax;

        public decimal TaxAmount(decimal totalAmount)
        {
            if (totalAmount <= TaxRule.TaxFreeBracket)
            {
                // Tax Free Rate
                taxRate = TaxRule.TaxFreeRate;
                tax = totalAmount * taxRate;
            }
            else if (totalAmount > TaxRule.TaxFreeBracket && totalAmount <= TaxRule.BasicTaxBracket)
            {
                // Basic Tax Rate
                taxRate = TaxRule.BasicTaxRate;
                // Income tax
                tax = (TaxRule.TaxFreeBracket * TaxRule.TaxFreeRate) + ((totalAmount - TaxRule.TaxFreeBracket) * taxRate);
            }
            else if (totalAmount > TaxRule.BasicTaxBracket && totalAmount <= TaxRule.HigherTaxBracket)
            {
                // Higher tax rate
                taxRate = TaxRule.HigherTaxRate;
                // Income tax
                tax = (TaxRule.TaxFreeBracket * TaxRule.TaxFreeRate) + 
                        ((TaxRule.BasicTaxBracket - TaxRule.TaxFreeBracket) * TaxRule.BasicTaxRate) + 
                        ((totalAmount - TaxRule.BasicTaxBracket) * taxRate);
            }
            else if (totalAmount > 12500)
            {
                // Additional tax rate
                taxRate = TaxRule.AdditionalTaxRate;
                // Income tax
                tax = (TaxRule.TaxFreeBracket * TaxRule.TaxFreeRate) +
                        ((TaxRule.BasicTaxBracket - TaxRule.TaxFreeBracket) * TaxRule.BasicTaxRate) + 
                        ((TaxRule.HigherTaxBracket - TaxRule.BasicTaxBracket) * TaxRule.HigherTaxRate) + 
                        ((totalAmount - TaxRule.HigherTaxBracket) * taxRate);
            }

            return tax;
        }
    }

    public static class TaxRule
    {
        public const decimal TaxFreeRate = 0.00m;
        public const decimal TaxFreeBracket = 1042m;
        public const decimal BasicTaxRate = 0.20m;
        public const decimal BasicTaxBracket = 3125m;
        public const decimal HigherTaxRate = 0.40m;
        public const decimal HigherTaxBracket = 12500m;
        public const decimal AdditionalTaxRate = 0.45m;      
    }
}
