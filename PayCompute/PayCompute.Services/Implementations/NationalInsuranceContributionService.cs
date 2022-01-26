using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services.Implementations
{
    public class NationalInsuranceContributionService : INationalInsuranceContributionServie
    {
        private decimal NIRate;
        private decimal NIC;
        public decimal NIContribution(decimal totalAmount)
        {
            if (totalAmount < 719)
            {
                // Lower Earning Limit Rate & below primary threshold
                NIRate = .0m;
                NIC = 0m;
            }
            else if(totalAmount >= 719 && totalAmount < 4167)
            {
                // Between primary threshold and Upper earning limit (UEL)
                NIRate = .12m;
                NIC = ((totalAmount - 719) * NIRate);
            }
            else if (totalAmount > 4167)
            {
                // Above Upper Earnings Limit (UEL)
                NIRate = .02m;
                NIC = ((4167 - 719) * .12m) + ((totalAmount - 4167) * NIRate);
            }

            return NIC;
        }
    }
}
