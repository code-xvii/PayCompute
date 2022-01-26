using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayCompute.Services
{
    public interface INationalInsuranceContributionServie
    {
        decimal NIContribution(decimal totalAmount);
    }
}
